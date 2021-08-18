using BetterMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BetterMe.Controllers
{
    public class TeachersController : ApiController
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

        [HttpGet]
        [Route("api/Teachers/loginFunc")]
        public HttpResponseMessage loginFunc(string email, string pass)
        {
            Teacher t = new Teacher();
            t.Email = email;
            t.Pass = pass;
            try
            {
                t = t.Read(email, pass);
                if (t == null)                
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שם משתמש או סיסמא שגויים");
                else  
                    return Request.CreateResponse(HttpStatusCode.OK, t);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שם משתמש או סיסמא שגויים");
            }
        }

        [HttpPut]
        [Route("api/Teachers/updateTeacherPass")]
        public void updateTeacherPass(string email, string newPass)
        {
            Teacher t = new Teacher();

            try
            {
                t.updateTeacherPass(email, newPass);
            }
            catch
            { }
        }
        
    }
}