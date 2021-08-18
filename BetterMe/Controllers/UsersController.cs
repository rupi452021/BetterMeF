using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BetterMe.Models;

namespace BetterMe.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        //שליחת מייל עם הסיסמא
        [HttpGet]
        [Route("api/Users/resetPass")]
        public HttpResponseMessage resetPass(string email)
        {

            try
            {
                User u = new User();
                u = u.Forgot(email);
                return Request.CreateResponse(HttpStatusCode.OK, u);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה בשליחת המייל");
            }
        }

    }
}