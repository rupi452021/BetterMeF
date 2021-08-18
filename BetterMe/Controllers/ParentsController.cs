using BetterMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BetterMe.Controllers
{
    public class ParentsController : ApiController
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
        [Route("api/Parents/loginFunc")]
        public HttpResponseMessage loginFunc(string email, string pass)
        {
            Parent p = new Parent();
            p.Email = email;
            p.Pass = pass;
            try
            {
                p = p.Read(email, pass);
                if (p == null)
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שם משתמש או סיסמא שגויים");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, p);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שם משתמש או סיסמא שגויים");
            }
        }


        [HttpPost]
        [Route("api/Parents/addParents")]
        public HttpResponseMessage addParents(List<Parent> Parents)
        {
            Parent p = new Parent();
            
            try
            {
                p.InsertParents(Parents);
                return Request.CreateResponse(HttpStatusCode.OK, Parents);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, Parents);
            }
        }

        [HttpGet]
        [Route("api/Parents/GetParDetails")]
        public HttpResponseMessage GetParDetails(int idClass)
        {
            try
            {
                Parent p = new Parent();
                List<Parent> ParentDetList = new List<Parent>();

                ParentDetList = p.ReadParentDetails(idClass);
                return Request.CreateResponse(HttpStatusCode.OK, ParentDetList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך מבחנים");
            }
        }

        [HttpPut]
        [Route("api/Parents/updatePar")]
        public void updatePar(int idClass, [FromBody]List<Parent> AfterEditParArr)
        {
            Parent p = new Parent();
            try
            {
                p.updatePar(idClass, AfterEditParArr);
            }
            catch
            { }
        }

        [HttpPut]
        [Route("api/Parents/updateParentPass")]
        public void updateParentPass(string email, string newPass)
        {
            Parent p = new Parent();

            try
            {
                p.updateParentPass(email, newPass);
            }
            catch
            { }
        }
    }
}