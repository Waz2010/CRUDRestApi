using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleCall.Models;

namespace SimpleCall.Controllers
{
    public class PersonController : ApiController
    {


        /// <summary>
        /// Get All Person
        /// </summary>
        /// <returns></returns>
        public ArrayList Get()
        {
            PersonTable personTable = new PersonTable();
            return personTable.GetPersons();
        }


        /// <summary>
        /// Get Specific Person, and it requirs an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Person/5
        public Person Get(long id)
        {
            PersonTable personTable = new PersonTable();
            Person person = personTable.GetPerson(id);
            return person; 
        }

        /// <summary>
        /// Delet Person
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Person
        public HttpResponseMessage Post([FromBody]Person value)
        {
            PersonTable personTable = new PersonTable();
            long id;
            id = personTable.SavePerson(value);
            value.ID = id;
            //create a response and call it a Post-response object
            HttpResponseMessage Response = Request.CreateResponse(System.Net.HttpStatusCode.Created);
            //set header up for the resouce just created
            Response.Headers.Location = new Uri(Request.RequestUri, string.Format("person/{0}", id));
            return Response;
        }


        /// <summary>
        /// To Update person by is
        /// </summary>
        /// <param name="id"> integer</param>
        /// <param name="value"> person object</param>
        /// <returns> 404 or 202</returns>
        // PUT: api/Person/5
        public HttpResponseMessage Put(long id, [FromBody]Person value)
        {
            PersonTable p = new PersonTable();
            bool recoredExisted = false;
            recoredExisted = p.UpdatePerson(id, value);
            HttpResponseMessage Response; // creating a response object
            if (recoredExisted)
            {
                Response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                Response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Response;
        }

        /// <summary>
        /// Deletes a Person and it requirs an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Person/5
        public HttpResponseMessage Delete(long id)
        {
            PersonTable  P = new PersonTable();

            bool recoredExisted = false;

            recoredExisted = P.DeletePerson(id);

            HttpResponseMessage Response; // creating a response object
            if (recoredExisted)
            {
                Response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                Response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Response;
        }
    }
}




// GET: api/Person
//returns array of string 
//public IEnumerable<string> Get()
//{
//    return new string[] { "person1", "Person2" };
//}