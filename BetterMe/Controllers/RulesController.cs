using BetterMe.Models;
using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BetterMe.Controllers
{
    public class RulesController : ApiController
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

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        public void Put(int id, [FromBody]Exam e)
        {
            try
            {
                Exam ex = new Exam();
                ex.UpdateGrade(id, e);

            }
            catch
            {

            }
        }


        [HttpPost]
        [Route("api/Rules/RandomAddPoints")]
        public HttpResponseMessage RandomAddPoints(RandomAddPoints[] RandomAddPointsArr)
        {
            try
            {
                RandomAddPoints RAP = new RandomAddPoints();
                RAP.InsertRandomPoints(RandomAddPointsArr);
                return Request.CreateResponse(HttpStatusCode.OK, RandomAddPointsArr);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה בשמירת החוק");
            }
        }


        [HttpPost]
        [Route("api/Rules/subPoints")]
        public HttpResponseMessage subPoints(Remark[] subPointsArr)
        {
            try
            {
                Remark r = new Remark();
                r.InsertRemark(subPointsArr);
                return Request.CreateResponse(HttpStatusCode.OK, subPointsArr);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה בשמירת החוק");
            }
        }


        [HttpPost]
        [Route("api/Rules/InsertPrizesWithout")]
        public HttpResponseMessage InsertPrizesWithout(PrizeWithout[] prizewithout)
        {
            try
            {
                PrizeWithout pwo = new PrizeWithout();
                pwo.InsertPrizesWithout(prizewithout);
                return Request.CreateResponse(HttpStatusCode.OK, prizewithout);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה בשמירת החוק");
            }
        }

        [HttpPost]
        [Route("api/Rules/InsertPrizesWith")]
        public HttpResponseMessage InsertPrizesWith(PrizeWith[] prizewith)
        {
            try
            {
                PrizeWith pw = new PrizeWith();
                pw.InsertPrizesWith(prizewith);
                return Request.CreateResponse(HttpStatusCode.OK, prizewith);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה בשמירת החוק");
            }
        }



        [HttpDelete]
        [Route("api/Rules/DeleteRandomAddPoints")]
        public HttpResponseMessage DeleteRandomAddPoints(RandomAddPoints RAP)
        {
            try
            {
                RandomAddPoints rap = new RandomAddPoints();
                rap.DeleteRAP(RAP);
                return Request.CreateResponse(HttpStatusCode.OK, RAP);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במחיקה");
            }
        }


        [HttpDelete]
        [Route("api/Rules/DeleteRemark")]
        public HttpResponseMessage DeleteRemark(Remark R)
        {
            try
            {
                Remark r = new Remark();
                r.DeleteR(R);
                return Request.CreateResponse(HttpStatusCode.OK, R);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במחיקה");
            }
        }

        [HttpDelete]
        [Route("api/Rules/DeletePrizeWith")]
        public HttpResponseMessage DeletePrizeWith(PrizeWith PW)
        {
            try
            {
                PrizeWith pw = new PrizeWith();
                pw.DeletePW(PW);
                return Request.CreateResponse(HttpStatusCode.OK, PW);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במחיקה");
            }
        }

        [HttpDelete]
        [Route("api/Rules/DeletePrizeWithOut")]
        public HttpResponseMessage DeletePrizeWithOut(PrizeWithout PWO)
        {
            try
            {
                PrizeWithout pwo = new PrizeWithout();
                pwo.DeletePWO(PWO);
                return Request.CreateResponse(HttpStatusCode.OK, PWO);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במחיקה");
            }
        }

        [HttpPost]
        [Route("api/Rules/SetRemark")]
        public HttpResponseMessage SetRemark(StudentRemark[] SetRemarkArr)
        {
            try
            {
                StudentRemark sr = new StudentRemark();
                sr.SetRemark(SetRemarkArr);
                return Request.CreateResponse(HttpStatusCode.OK, sr);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה בשמירת הערה");
            }
        }
    }
}