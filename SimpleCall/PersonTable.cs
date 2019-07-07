using System;
using SimpleCall.Models;
using System.Collections;

namespace SimpleCall
{
    public class PersonTable
    {
        //creating a privage date member
        private  MySql.Data.MySqlClient.MySqlConnection conn; // one connection that whole class will use it (MySqlConnection)

        //create a constructor that will create a connection to the database. 
        //Benefit of that would be that when ever we will create an instance of this class it connection to the database will be ready to use.
        public PersonTable()
        {
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=admin;pwd=password;database=employeedb";
            try
            {
                //set our private data member to new connection
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                //connection string is on that class is my string 
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch(MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                
            }
        }

        public long SavePerson(Person PersonToSave)
        {
            //String to execute
            string sqlString = "INSERT INTO tblpersonnel (FirstName, LastName, PayRate, StartDate, EndDate)VALUES('" + PersonToSave.FirstName + "','" + PersonToSave.LastName + "'," + PersonToSave.PayRate + ",'" + PersonToSave.StartDate.ToString("yyyy-MM-dd HH:mmss") + "','" + PersonToSave.EndDate.ToString("yyyy-MM-dd HH:mmss") + "')";
            //Create a new connection and pass the sql query and connction string 
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            //Send command to execute the command
            cmd.ExecuteNonQuery();//non query means we dont need the results baack
            //command object, once executed, will have the auto number ID filed whcih we want 
            long id = cmd.LastInsertedId;
            CloseConnection();
            return id;
        }

        public Person GetPerson(long ID)
        {

            Person person = new Person(); //Get a new person object
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;// Using MySql Reader call to go and read the data form the db
            string sqlString = "SELECT * FROM tblpersonnel WHERE ID =" + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn); //Create a new connection and pass the sql query and connction string 
            mySQLReader = cmd.ExecuteReader(); //Get whatever query is brings back
            if (mySQLReader.Read())  //if we got back the data then 
            {
                person.ID = mySQLReader.GetInt32(0); // get what came back for the First column
                person.FirstName = mySQLReader.GetString(1); // get what came back for the Second column
                person.LastName = mySQLReader.GetString(2); // get what came back for the Third column
                person.PayRate = mySQLReader.GetFloat(3); // get what came back for the Forth column
                person.StartDate = mySQLReader.GetDateTime(4); // get what came back for the Fifth column
                person.EndDate = mySQLReader.GetDateTime(5); // get what came back for the Sixth column
                return person;
            }
            else
            {
                return null;
            }
        }

        public bool DeletePerson(long ID)
        {
            
            Person person = new Person(); //Get a new person object
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;// Using MySql Reader call to go and read the data form the db
            string sqlString = "SELECT * FROM tblpersonnel WHERE ID =" + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn); //Create a new connection and pass the sql query and connction string 

            mySQLReader = cmd.ExecuteReader(); //Get whatever query is brings back
            if (mySQLReader.Read())  //if we got back the data then 
            {
                mySQLReader.Close();// Close the reader so won't throw things away.

                sqlString = "DELETE FROM tblpersonnel WHERE ID =" + ID.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn); //Create a new connection and pass the sql query and connction string
                
                cmd.ExecuteNonQuery();// 
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdatePerson(long ID, Person PersonToUpdate)
        {

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;// Using MySql Reader call to go and read the data form the db
            string sqlString = "SELECT * FROM tblpersonnel WHERE ID =" + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn); //Create a new connection and pass the sql query and connction string 

            mySQLReader = cmd.ExecuteReader(); //Get whatever query is brings back
            if (mySQLReader.Read())  //if we got back the data then 
            {
                mySQLReader.Close();// Close the reader so won't throw things away.

                sqlString = "UPDATE tblpersonnel SET FirstName= '" + PersonToUpdate.FirstName + "', LastName='" + PersonToUpdate.LastName + "', PayRate = " + PersonToUpdate.PayRate + ", StartDate='" + PersonToUpdate.StartDate.ToString("yyyy-MM-dd HH:mmss") + "', EndDate='" + PersonToUpdate.EndDate.ToString("yyyy-MM-dd HH:mmss") + "' WHERE ID = " + ID.ToString();

                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn); //Create a new connection and pass the sql query and connction string

                cmd.ExecuteNonQuery();// 
                return true;
            }
            else
            {
                return false;
            }
        }

        public ArrayList GetPersons()
        {
            //Create an ArrayList
            ArrayList PersonArray = new ArrayList();
            //Using MySql Reader call to go and read the data form the db
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            string sqlString = "SELECT * FROM tblpersonnel";

            //Create a new connection and pass the sql query and connction string 
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            //Get whatever quesry is brings back
            mySQLReader = cmd.ExecuteReader();

            //if we got back the data then 
           while (mySQLReader.Read())
            {
                Person p = new Person();
                p.ID = mySQLReader.GetInt32(0); // get what came back for the First column
                p.FirstName = mySQLReader.GetString(1); // get what came back for the Second column
                p.LastName = mySQLReader.GetString(2); // get what came back for the Third column
                p.PayRate = mySQLReader.GetFloat(3); // get what came back for the Forth column
                p.StartDate = mySQLReader.GetDateTime(4); // get what came back for the Fifth column
                p.EndDate = mySQLReader.GetDateTime(5); // get what came back for the Sixth column
                PersonArray.Add(p);
            }
            return PersonArray;
        }

        public void CloseConnection()
        {
            conn.Close();
        }

    }
}