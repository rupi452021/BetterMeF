using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BetterMe.Models;

namespace BetterMe.Controllers
{
    public class StudentsController : ApiController
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
        public void Post([FromBody] List<Student> studList)
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
        [Route("api/Students/loginFunc")]
        public HttpResponseMessage loginFunc(string email, string pass)
        {
            Student s = new Student();
            s.Email = email;
            s.Pass = pass;
            try
            {
                s = s.Read(email, pass);
                if (s == null)
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שם משתמש או סיסמא שגויים");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, s);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שם משתמש או סיסמא שגויים");
            }
        }



        [HttpPost]
        [Route("api/Students/addStudents")]
        public HttpResponseMessage addStudents(List<Student> Students)
        {
            Student s = new Student();

            try
            {
                s.InsertStudents(Students);
                return Request.CreateResponse(HttpStatusCode.OK, s);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, s);
            }
        }



        [HttpPost]
        [Route("api/Students/addItem")]
        public HttpResponseMessage addItem(Item item)
        {
            Item i = new Item();
            try
            {
                i.addItem(item);
                return Request.CreateResponse(HttpStatusCode.OK, i);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה");
            }
        }

        //buy item
        [HttpPost]
        [Route("api/Students/buyItem")]
        public HttpResponseMessage buyItem(int itemId, string studentEmail)
        {
            Item i = new Item();
            
            try
            {
                i.BuyItem(itemId, studentEmail);
                if (i == null)
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה ברכישת הפריט");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, i);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שם משתמש או סיסמא שגויים");
            }
        }


        //update points
        [HttpPut]
        [Route("api/Students/updatePoints")]
        public HttpResponseMessage updatePoints(int StudentPoints, int ItemPoints, string studentEmail)
        {
            Student s = new Student();

            try
            {
                s.updatePoints(StudentPoints, ItemPoints, studentEmail);
                if (s == null)
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה ברכישת הפריט");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, s);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שם משתמש או סיסמא שגויים");
            }
        }

        //get items
        [HttpGet]
        [Route("api/Students/GetStudentItems")]
        public HttpResponseMessage GetStudentItems(string studentEmail)
        {
            List<StudentItem> ItemsList = new List<StudentItem>();
            StudentItem si = new StudentItem();
            
            try
            {
                ItemsList = si.GetStudentItems(studentEmail);
                if (ItemsList == null)
                    return Request.CreateResponse(HttpStatusCode.OK, ItemsList);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, ItemsList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במציאת פריטים");
            }
        }


        //save item place
        [HttpPost]
        [Route("api/Students/placeItems")]
        public HttpResponseMessage placeItems(List<StudentItem> studentItems)
        {           
            try
            {
                StudentItem si = new StudentItem();
                si.placeItems(studentItems);
                return Request.CreateResponse(HttpStatusCode.OK, studentItems);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במיקום הפריטים");
            }
        }

        //delete item 
        [HttpDelete]
        [Route("api/Students/DeleteItem")]
        public HttpResponseMessage DeleteItem(StudentItem StuItem)
        {           
            try
            {
                StuItem.DeleteItem(StuItem);
                return Request.CreateResponse(HttpStatusCode.OK, StuItem);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במיקום הפריטים");
            }
        }

        
        //Get Student Points
        [HttpGet]
        [Route("api/Students/GetStudentPoints")]
        public HttpResponseMessage GetStudentPoints(string studentEmail)
        {
            Student s = new Student();
            
            try
            {
                int points = s.GetStudentPoints(studentEmail);
                return Request.CreateResponse(HttpStatusCode.OK, points);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במציאת הניקוד");
            }
        }




        //Get Student Points
        [HttpGet]
        [Route("api/Students/getGrades")]
        public HttpResponseMessage getGrades(int idClass, int examNum)
        {
            Student s = new Student();
            
            try
            {
                List<int> stuGrades;
                stuGrades = s.getGrades(idClass, examNum);
                return Request.CreateResponse(HttpStatusCode.OK, stuGrades);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במציאת הניקוד");
            }
        }

        [HttpGet]
        [Route("api/Students/GetParentStudents")]
        public HttpResponseMessage GetParentStudents(string email)
        {
            try
            {
                ClassOfTeacher cot = new ClassOfTeacher();
                List<ClassOfTeacher> ClassesList = new List<ClassOfTeacher>();
                ClassesList = cot.GetParentStudents(email);
                if (ClassesList == null)
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך ילדים במערכת");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, ClassesList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך ילדים במערכת");
            }
        }



        //get student deatails for data table
        [HttpGet]
        [Route("api/Students/GetStudDetails")]
        public HttpResponseMessage GetStudDetails(int idClass)
        {
            Student s = new Student();

            try
            {

                List<Student> StudentdetList = s.GetStudDetails(idClass);
                return Request.CreateResponse(HttpStatusCode.OK, StudentdetList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך מבחנים");
            }
        }


        [HttpPut]
        [Route("api/Students/updateStud")]
        public void updateStud(int idClass, [FromBody]List<Student> AfterEditStudArr)
        {
            Student s = new Student();
            try
            {
                s.updateStud(idClass, AfterEditStudArr);
            }
            catch
            {  }
        }

        [HttpPut]
        [Route("api/Students/updateExams")]
        public void updateExams(string email, [FromBody] List<Exam> examAfterEdit)
        {
            Exam e = new Exam();

            try
            {
                e.updateExams(email, examAfterEdit);
            }
            catch
            { }
        }

        [HttpPut]
        [Route("api/Students/updateStudentPass")]
        public void updateStudentPass(string email, string newPass)
        {
            Student s = new Student();

            try
            {
                s.updateStudentPass(email, newPass);
            }
            catch
            { }
        }



        //get student deatails for data table
        [HttpGet]
        [Route("api/Students/GetStudentPurchases")]
        public HttpResponseMessage GetStudentPurchases(string studentEmail)
        {
            PurchasePrize pp = new PurchasePrize();

            try
            {
                List<PurchasePrize> PurchasesList = pp.GetStudentPurchases(studentEmail);
                return Request.CreateResponse(HttpStatusCode.OK, PurchasesList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך רכישות");
            }
        }


        //get student remarks for data table
        [HttpGet]
        [Route("api/Students/GetAllStudentRemarks")]
        public HttpResponseMessage GetAllStudentRemarks(string studentEmail)
        {
            AllStudentRemarks asr = new AllStudentRemarks();

            try
            {
                List<AllStudentRemarks> RemarksList = asr.GetAllStudentRemarks(studentEmail);
                return Request.CreateResponse(HttpStatusCode.OK, RemarksList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך רכישות");
            }
        }


        //get student grades from all classes
        [HttpGet]
        [Route("api/Students/GetStudentGrades")]
        public HttpResponseMessage GetStudentGrades(string studentEmail)
        {
            StudentExam se = new StudentExam();

            try
            {
                List<StudentExam> GradesList = se.GetStudentGrades(studentEmail);
                return Request.CreateResponse(HttpStatusCode.OK, GradesList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך רכישות");
            }
        }

        //get student details
        [HttpGet]
        [Route("api/Students/GetStuDetails")]
        public HttpResponseMessage GetStuDetails(string emailStu)
        {
            Student s = new Student();

            try
            {
                s = s.GetStuDetails(emailStu);
                return Request.CreateResponse(HttpStatusCode.OK, s);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!אין לך פרטים");
            }
        }


        //Get Student Grades for card
        [HttpGet]
        [Route("api/Students/getGradesForGraph")]
        public HttpResponseMessage getGradesForGraph(string email, string subjName)
        {
            Exam e = new Exam();

            try
            {
                List<Exam> grades = e.getGradesForGraph(email, subjName);
                return Request.CreateResponse(HttpStatusCode.OK, grades);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "!שגיאה במציאת הניקוד");
            }
        }

        //הוסף מבחנים
        [HttpPost]
        [Route("api/Students/FromExcel")]
        public HttpResponseMessage FromExcel(List<Student> exams)
        {
            Student ex = new Student();

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
    }
}