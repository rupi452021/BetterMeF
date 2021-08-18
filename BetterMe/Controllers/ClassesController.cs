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
    public class ClassesController : ApiController
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

        //בדוק האם כיתת אם קיימת
        [HttpGet]
        [Route("api/Classes/CheckMotherClass")]
        public HttpResponseMessage CheckMotherClass(string Layer, string Subjname, int Classnum)
        {
            try
            {
                ClassOfTeacher cd = new ClassOfTeacher();
                cd.Layer = Layer;
                cd.Subjname = Subjname;
                cd.Classnum = Classnum;
                cd = cd.CheckMotherClasss(Layer, Subjname, Classnum);
                if(cd == null)
                    return Request.CreateResponse(HttpStatusCode.OK, cd);

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!הכיתה כבר קיימת");
           
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה");
            }
        }

        //בדוק האם כיתה מקצועית קיימת
        [HttpGet]
        [Route("api/Classes/CheckProClass")]
        public HttpResponseMessage CheckProClass(string Layer, string Subjname, string Levelgroup, int Groupnum)
        {
            try
            {
                ClassOfTeacher cd = new ClassOfTeacher();
                cd.Layer = Layer;
                cd.Subjname = Subjname;
                cd.Levelgroup = Levelgroup;
                cd.Groupnum = Groupnum;
                cd = cd.CheckProClasss(Layer, Subjname, Levelgroup, Groupnum);
                if (cd == null)
                    return Request.CreateResponse(HttpStatusCode.OK, cd);

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!הכיתה כבר קיימת");

            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה");
            }
        }



        //הבא כיתות
        [HttpGet]
        [Route("api/Classes/GetClasses")]
        public HttpResponseMessage GetClasses(int teachId)
        {            
            try
            {
                ClassOfTeacher cot = new ClassOfTeacher();
                List<ClassOfTeacher> ClassesList = new List<ClassOfTeacher>();
                ClassesList = cot.ReadClassses(teachId);
                if (ClassesList == null)
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך כיתות");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, ClassesList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך כיתות");
            }
        }


        //הוסף מקצוע
        [HttpGet]
        [Route("api/Classes/ReadSubj")]
        public HttpResponseMessage ReadSubj(string SubjName)
        {
            try
            {
                Subj sub = new Subj();
                int num = sub.ReadSubj(SubjName);
                return Request.CreateResponse(HttpStatusCode.OK, num);

            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה בהוספת המקצוע");
            }
        }

        //הוסף כיתה
        [HttpPost]
        [Route("api/Classes/addClass")]
        public HttpResponseMessage addClass(Class ClassData)
        {
            try
            {
                int num = ClassData.InsertClass(ClassData); 

                return Request.CreateResponse(HttpStatusCode.OK, num);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה בהוספת הכיתה");
            }
        }
        
        
        //הוסף כיתת אם
        [HttpPost]
        [Route("api/Classes/addMotherClass")]
        public HttpResponseMessage addMotherClass(Motherclass MotherData)
        {
            try
            {
                MotherData.InsertMotherClass(MotherData);

                return Request.CreateResponse(HttpStatusCode.OK, MotherData);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה בהוספת כיתת אם");
            }
        }

        
        //הוסף כיתה מקצועית
        [HttpPost]
        [Route("api/Classes/addProClass")]
        public HttpResponseMessage addProClass(ProfessionalClass ProData)
        {
            try
            {
                ProData.InsertProClass(ProData);
                return Request.CreateResponse(HttpStatusCode.OK, ProData);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה בהוספת כיתה מקצועית");
            }
        }



        //הבא חוקי הוספת ניקוד ידני
        [HttpGet]
        [Route("api/Classes/getRandomAddPoints")]
        public HttpResponseMessage getRandomAddPoints(int idClass)
        {

            try
            {
                RandomAddPoints RaP = new RandomAddPoints();
                List<RandomAddPoints> RaPList = RaP.ReadRandomAddPoints(idClass);
                return Request.CreateResponse(HttpStatusCode.OK, RaPList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך כיתות");
            }
        }


        //הבא חוקי הורדת ניקוד
        [HttpGet]
        [Route("api/Classes/getRemark")]
        public HttpResponseMessage getRemark(int idClass)
        {

            try
            {
                Remark cp = new Remark();
                List<Remark> cpList = cp.ReadRemark(idClass);
                return Request.CreateResponse(HttpStatusCode.OK, cpList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך כיתות");
            }
        }

        //הבא פרסים עם תנאים
        [HttpGet]
        [Route("api/Classes/getPrizeWith")]
        public HttpResponseMessage getPrizeWith(int idClass)
        {
            try
            {
                PrizeWith pw = new PrizeWith();
                List<PrizeWith> pwList = pw.ReadPrizeWith(idClass);
                return Request.CreateResponse(HttpStatusCode.OK, pwList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך כיתות");
            }
        }


        //get prizeswith for student in class
        [HttpGet]
        [Route("api/Classes/getPrizeWithForStudent")]
        public HttpResponseMessage getPrizeWithForStudent(int idClass)
        {
            try
            {
                PrizeWith pw = new PrizeWith();
                List<PrizeWith> pwList = pw.getPrizeWithForStudent(idClass);
                return Request.CreateResponse(HttpStatusCode.OK, pwList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך כיתות");
            }
        }

        //הבא פרסים בלי תנאים
        [HttpGet]
        [Route("api/Classes/getPrizeWithout")]
        public HttpResponseMessage getPrizeWithout(int idClass)
        {

            try
            {
                PrizeWithout pwo = new PrizeWithout();
                List<PrizeWithout> pwoList = pwo.ReadPrizeWithout(idClass);
                return Request.CreateResponse(HttpStatusCode.OK, pwoList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך כיתות");
            }
        }

        //get prizeswithout for student in class
        [HttpGet]
        [Route("api/Classes/getPrizeWithoutForStudent")]
        public HttpResponseMessage getPrizeWithoutForStudent(int idClass)
        {

            try
            {
                PrizeWithout pwo = new PrizeWithout();
                List<PrizeWithout> pwoList = pwo.getPrizeWithoutForStudent(idClass);
                return Request.CreateResponse(HttpStatusCode.OK, pwoList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך כיתות");
            }
        }

        

        [HttpGet]
        [Route("api/Classes/GetClassesStudent")]
        public HttpResponseMessage GetClassesStudent(string email)
        {
            try
            {
                ClassOfTeacher cot = new ClassOfTeacher();
                List<ClassOfTeacher> ClassesList = new List<ClassOfTeacher>();
                ClassesList = cot.ReadClasssesStudent(email);
                if (ClassesList == null)
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך כיתות");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, ClassesList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך כיתות");
            }
        }


        //Get students prizes 
        [HttpGet]
        [Route("api/Classes/getStudendExams")]
        public HttpResponseMessage getStudendExams(int idClass, string studentEmail)
        {

            try
            {
                Exam ex = new Exam();
                List<Exam> examsList = ex.ReadStudentExams(idClass, studentEmail);
                return Request.CreateResponse(HttpStatusCode.OK, examsList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך מבחנים");
            }          
        }

        [HttpDelete]
        [Route("api/Classes/DeleteOneExam")]
        public HttpResponseMessage DeleteExam(int idclass, string studentEmail)
        {
            try
            {
                Exam e = new Exam();
                e.IdClass = idclass;
                e.StudentEmail = studentEmail;
                e.DExam(idclass, studentEmail);
                return Request.CreateResponse(HttpStatusCode.OK, e);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במחיקת מבחן");
            }
        }



        //הוסף פרס
        [HttpPost]
        [Route("api/Classes/addPurchPrize")]
        public HttpResponseMessage addPurchPrize(PurchasePrize purchPrize)
        {
            try
            {
                purchPrize.addPurchPrize(purchPrize);
                return Request.CreateResponse(HttpStatusCode.OK, purchPrize);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה בהוספת כיתה מקצועית");
            }
        }

        //get the details selected class
        [HttpGet]
        [Route("api/Classes/GetDetails")]
        public HttpResponseMessage GetDetails(int idClass)
        {

            try
            {
                ClassOfTeacher cot = new ClassOfTeacher();
                cot = cot.ReadClassDetails(idClass);
                return Request.CreateResponse(HttpStatusCode.OK, cot);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה בטעינת כיתה");
            }
        }

        //הוסף מבחנים
        [HttpPost]
        [Route("api/Classes/addExam")]
        public HttpResponseMessage addExam(List<Exam> exams)
        {
            Exam ex = new Exam();

            try
            {
                ex.InsertExams(exams);
                return Request.CreateResponse(HttpStatusCode.OK, exams);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, exams);
            }
        }

        //הבא מבחנים
        [HttpGet]
        [Route("api/Classes/getExams")]
        public HttpResponseMessage getExams(int idClass)
        {

            try
            {
                Student stud = new Student();
                List<Student> StudList = stud.ReadExams(idClass);
                return Request.CreateResponse(HttpStatusCode.OK, StudList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך מבחנים");
            }
        }


        //get remarks for class
        [HttpGet]
        [Route("api/Classes/getRemarkForClass")]
        public HttpResponseMessage getRemarkForClass(int idClass)
        {

            try
            {
                StudentRemark sr = new StudentRemark();
                List<StudentRemark> remarksList = sr.getRemarkForClass(idClass);
                return Request.CreateResponse(HttpStatusCode.OK, remarksList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך הערות");
            }
        }

        [HttpDelete]
        [Route("api/Classes/DeleteOneRemark")]
        public HttpResponseMessage DeleteOneRemark(int id, string emailStudent)
        {
            try
            {
                StudentRemark sr = new StudentRemark();
                sr.delRemark(id, emailStudent);
                return Request.CreateResponse(HttpStatusCode.OK, sr);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במחיקת הערה");
            }
        }

        [HttpDelete]
        [Route("api/Classes/DeleteClass")]
        public HttpResponseMessage DeleteClass(int classId)
        {
            try
            {
                Class c = new Class();
                c.DeleteClass(classId);
                return Request.CreateResponse(HttpStatusCode.OK, c);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במחיקת כיתה");
            }
        }





    }
}