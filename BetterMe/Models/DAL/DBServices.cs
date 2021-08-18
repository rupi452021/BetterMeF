using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using System.Net.Mail;

namespace BetterMe.Models.DAL
{
    public class DBServices
    {
        //delete all class
        public void DeleteClass(int classId)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "delete from Motherclass where idClass=" + classId;
                selectSTR += " delete from ProfessionalClass where idClass=" + classId;
                selectSTR += " delete from StudentInClass where idClass=" + classId;
                selectSTR += " delete from Exam where idclass=" + classId;

                cmd = CreateCommand(selectSTR, con);
                cmd.ExecuteNonQuery();
                con.Close();
                con = connect("DBConnectionString");

                selectSTR = "delete from Class where id=" + classId;

                cmd = CreateCommand(selectSTR, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //delete the worst remark 
        public void DeleteRemrk(int idclass, string studentEmail)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "DELETE FROM studentRemark  where studentRemark.remarkName = (select top 1 sr.remarkName FROM studentRemark sr inner join Remarks r on sr.remarkName = r.remarkname  where sr.emailStudent = '" + studentEmail + "' and sr.idClass = " + idclass + " order by takenpoints asc)";
                cmd = CreateCommand(selectSTR, con);
                cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //Get student remakrs for table 
        public List<AllStudentRemarks> GetAllStudentRemarks(string studentEmail)
        {
            SqlConnection con = null;
            List<AllStudentRemarks> StudentRemarkList = new List<AllStudentRemarks>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select t.firstName, t.lastName, s.subjname, sr.remarkName, r.takenpoints ";
                selectSTR += "from studentRemark sr inner join class c on sr.idClass=c.id inner join Subj s on c.subjId=s.id ";
                selectSTR += "inner join Remarks r on sr.remarkName=r.remarkname inner join Teacher t on c.idTeacher=t.id ";
                selectSTR += "where emailStudent='" + studentEmail + "'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    AllStudentRemarks asr = new AllStudentRemarks();

                    asr.FirstName = (string)dr["firstName"];
                    asr.LastName = (string)dr["lastName"];
                    asr.Subjname = (string)dr["subjname"];
                    asr.RemarkName = (string)dr["remarkName"];
                    asr.Takenpoints = Convert.ToInt32(dr["takenpoints"]);

                    StudentRemarkList.Add(asr);
                }

                return StudentRemarkList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //get student grades for card
        public List<Exam> getGradesForGraph(string email, string subjName)
        {
            SqlConnection con = null;
            List<Exam> grades = new List<Exam>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select e.grade, e.examNum, su.subjname ";
                selectSTR += "from Student s inner join Exam e on s.email=e.studentEmail inner join class c on e.idclass=c.id ";
                selectSTR += "inner join Subj su on c.subjId=su.id ";
                selectSTR += "where s.email='" + email + "' and su.subjname='" + subjName + "'";
                selectSTR += "order by examNum";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Exam ex = new Exam();
                    ex.ExamNum = Convert.ToInt32(dr["ExamNum"]);
                    ex.Grade = Convert.ToInt32(dr["grade"]);
                    grades.Add(ex);
                }
                return grades;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //get student details for card
        public Student GetStuDetails(string emailStu)
        {
            SqlConnection con = null;
            Student s = new Student();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select s.firstname, s.lastname, s.birthday, s.phone as phoneStu, p.fullname, p.phone as phonePar, s.numofpoints ";
                selectSTR += "from Student s inner join Parent p on s.emailParent=p.email ";
                selectSTR += "where s.email='" + emailStu + "'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    s.Pass = (string)dr["phonePar"];
                    s.First_name = (string)dr["firstname"];
                    s.Last_name = (string)dr["lastname"];
                    s.Birthday = (string)dr["birthday"];
                    s.Phone = (string)dr["phoneStu"];
                    s.NumOfPoints = Convert.ToInt32(dr["numofpoints"]); 
                    s.EmailParent = (string)dr["fullname"];
                    return s;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //Get student purchases 
        public List<StudentRemark> getRemarkForClass(int idClass)
        {
            SqlConnection con = null;
            List<StudentRemark> StudentRemarkList = new List<StudentRemark>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select r.remarkname as comments, r.takenpoints as points";
                selectSTR += " from remarks r where r.idClass=" +idClass+" union ";
                selectSTR += " select rap.comment as comments, rap.points as points";
                selectSTR += " from randomaddPoints rap where rap.idClass=" + idClass;

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    StudentRemark sr = new StudentRemark();

                    sr.Remarkname = (string)dr["comments"];
                    sr.Points = Convert.ToInt32(dr["points"]);

                    StudentRemarkList.Add(sr);
                }

                return StudentRemarkList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //new 1
        public int SetRemark(StudentRemark[] SetRemarkArr)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandStudRemark(SetRemarkArr);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommandStudRemark(StudentRemark[] SetRemarkArr)
        {
            String command = "";
            foreach (StudentRemark sr in SetRemarkArr)
            {
                StringBuilder sb = new StringBuilder();
                String prefix = " insert into studentRemark (emailStudent, idClass, remarkName) ";
                sb.AppendFormat("values('{0}', {1}, '{2}')", sr.Studentemail, sr.IdClass, sr.Remarkname);
                command += prefix + sb.ToString();
                command += " UPDATE Student SET numofpoints = numofpoints + " + sr.Points + " WHERE email='" + sr.Studentemail + "' ";
            }
            return command;
        }
        //end new 1

        //get student grades from all subj
        public List<StudentExam> GetStudentGrades(string studentEmail)
        {
            SqlConnection con = null;
            List<StudentExam> GradesList = new List<StudentExam>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select su.subjname, t.firstName, t.lastName,e.examNum, e.grade";
                selectSTR += " from student s inner join exam e on s.email=e.studentEmail inner join class c on e.idclass=c.id";
                selectSTR += " inner join subj su on c.subjId=su.id inner join teacher t on c.idTeacher=t.id";
                selectSTR += " where s.email='" + studentEmail + "'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    StudentExam se = new StudentExam();
                    se.Subj = (string)dr["subjname"];
                    se.Firstname = (string)dr["firstName"];
                    se.Lastname = (string)dr["lastName"];
                    se.Examnum = Convert.ToInt32(dr["examNum"]);
                    se.Grade = Convert.ToInt32(dr["grade"]);

                    GradesList.Add(se);
                }
                return GradesList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //Get student purchases 
        public List<PurchasePrize> GetStudentPurchases(string studentEmail)
        {
            SqlConnection con = null;
            List<PurchasePrize> PurchasesList = new List<PurchasePrize>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select s.subjname, pp.prizeType";
                selectSTR += " from PurchasesPrizes pp inner join class c on pp.idClass=c.id";
                selectSTR += " inner join subj s on c.subjId=s.id inner join teacher t on c.idTeacher=t.id";
                selectSTR += " where emailStudent='" + studentEmail + "'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    PurchasePrize pp = new PurchasePrize();

                    pp.EmailStudent = (string)dr["subjname"];
                    pp.PrizeType = (string)dr["prizeType"];

                    PurchasesList.Add(pp);
                }

                return PurchasesList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //update parent pass
        public int updateParentPass(string email, string newPass)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildupdateParentPass(email, newPass);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildupdateParentPass(string email, string newPass)
        {

            String command = " update parent set pass='" + newPass + "'";
            command += " where email='" + email + "'";
            return command;
        }


        //update student pass
        public int updateStudentPass(string email, string newPass)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildupdateStudentPass(email, newPass);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildupdateStudentPass(string email, string newPass)
        {

            String command = " update student set pass='" + newPass + "'";
            command += " where email='" + email + "'";
            return command;
        }



        //update teacher pass
        public int updateTeacherPass(string email, string newPass)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildupdateTeacherPass(email, newPass);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildupdateTeacherPass(string email, string newPass)
        {
            
            String command = " update teacher set pass='" + newPass + "'";
            command += " where email='" + email + "'";            
            return command;
        }

        //שליפת מבחנים
        public List<Student> GetExam(int classId)
        {
            SqlConnection con = null;
            List<Exam> examList = new List<Exam>();
            List<Student> SdutList = new List<Student>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT distinct s.*, sc.idClass FROM Student s inner join StudentInClass sc on s.email=sc.email where sc.idclass=" + classId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Student stud = new Student();
                    stud.IdClass = Convert.ToInt32(dr["idclass"]);
                    stud.Email = (string)dr["email"];
                    stud.First_name = (string)dr["firstname"];
                    stud.Last_name = (string)dr["lastname"];
                    examList = getAllExam(stud.Email, stud.IdClass);
                    stud.AllExam = examList;
                    SdutList.Add(stud);
                }
                return SdutList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public List<Exam> getAllExam(string email, int idclass)
        {

            SqlConnection con = null;
            List<Exam> examList = new List<Exam>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT distinct e.grade, e.examNum FROM Exam e inner join Student s on e.studentEmail=s.email where e.idclass=" + idclass + " and e.studentEmail='" + email + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Exam ex = new Exam();
                    //ex.IdClass = Convert.ToInt32(dr["idclass"]);
                    //ex.StudentEmail = (string)dr["studentEmail"];
                    ex.Grade = Convert.ToInt32(dr["grade"]);
                    ex.ExamNum = Convert.ToInt32(dr["examNum"]);
                    examList.Add(ex);
                }
                return examList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //update student exams
        public int updateExams(string email, List<Exam> examAfterEdit)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildUpdateCommandStudentExams(email, examAfterEdit);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildUpdateCommandStudentExams(string email, List<Exam> examAfterEdit)
        {

            //            Update Exam Set grade = 50
            //where studentEmail = 'daniela@gmail.com' and idclass = 35 and examNum = 3
            String command = "";
            foreach (Exam e in examAfterEdit)
            {
                String prefix = " UPDATE Exam SET grade=" + e.Grade;
                prefix += " where studentEmail='" + e.StudentEmail + "' and idclass=" + e.IdClass + " and examNum=" + e.ExamNum;
                command += prefix;
            }
            return command;
        }


        public ClassOfTeacher ReadClassDetails(int idClass)
        {
            SqlConnection con = null;
            ClassOfTeacher ct = new ClassOfTeacher();

            try
            {
                con = connect("DBConnectionString");

                //כיתת אם
                String selectSTR = " select c.id, s.subjname, c.layer, mc.classnum, pc.groupnum, pc.levelgroup ";
                selectSTR += "from class c inner join Subj s on c.subjId=s.id ";
                selectSTR += "left join ProfessionalClass pc on c.id= pc.idClass left join Motherclass mc on c.id= mc.idClass ";
                selectSTR += "where c.id ='" + idClass + "' and pc.levelgroup IS NULL";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    //ClassOfTeacher cot = new ClassOfTeacher();
                    //ct.FirstName = (string)dr["firstName"];
                    ct.IdClass = Convert.ToInt32(dr["id"]);
                    ct.Layer = (string)dr["layer"];
                    ct.Subjname = (string)dr["subjname"];
                    ct.Classnum = Convert.ToInt32(dr["classnum"]);
                    //ct.Levelgroup= (string)dr["levelgroup"];
                    //ct.Groupnum = Convert.ToInt32(dr["groupnum"]);
                }

                //כיתות מקצועיות
                selectSTR = " select c.id, s.subjname, c.layer, mc.classnum, pc.groupnum, pc.levelgroup ";
                selectSTR += "from class c inner join Subj s on c.subjId=s.id ";
                selectSTR += "left join ProfessionalClass pc on c.id= pc.idClass left join Motherclass mc on c.id= mc.idClass ";
                selectSTR += "where c.id ='" + idClass + "' and mc.classnum IS NULL";

                con.Close();
                con = connect("DBConnectionString");

                SqlCommand cmd2 = new SqlCommand(selectSTR, con);
                dr = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    //ClassOfTeacher cot = new ClassOfTeacher();
                    //cot.FirstName = (string)dr["firstName"];
                    ct.IdClass = Convert.ToInt32(dr["id"]);
                    ct.Layer = (string)dr["layer"];
                    ct.Subjname = (string)dr["subjname"];
                    ct.Levelgroup = (string)dr["levelgroup"];
                    ct.Groupnum = Convert.ToInt32(dr["groupnum"]);

                }
                return ct;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //update parent table
        public int updatePar(int idClass, List<Parent> AfterEditStudArr)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildUpdateCommandPar(idClass, AfterEditStudArr);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildUpdateCommandPar(int idClass, List<Parent> AfterEditStudArr)
        {
            String command = "";
            foreach (Parent p in AfterEditStudArr)
            {
                String prefix = " Update Parent Set email='" + p.Email + "', fullname='" + p.Fullname + "', phone='" + p.Phone + "'";
                prefix += " where email ='" + p.Email + "'";
                command += prefix;
            }
            return command;
        }

        //get parent details
        public List<Parent> ReadParentDetails(int idClass)
        {
            SqlConnection con = null;
            List<Parent> ParentDetList = new List<Parent>();
            List<Student> StudentsData = GetStudDetails(idClass);

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = " select p.* ";
                selectSTR += "from StudentInClass sic inner join student s on sic.email=s.email inner join Parent p on s.emailParent=p.email ";
                selectSTR += "where sic.idClass=" + idClass;

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    Parent p = new Parent();
                    p.Email = (string)dr["email"];
                    p.Pass = (string)dr["pass"];
                    p.Fullname = (string)dr["fullname"];
                    p.Phone = (string)dr["phone"];
                    ParentDetList.Add(p);
                }

                return ParentDetList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        //upadate student table
        public int updateStud(int idClass, List<Student> AfterEditStudArr)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildUpdateCommandStud(idClass, AfterEditStudArr);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildUpdateCommandStud(int idClass, List<Student> AfterEditStudArr)
        {
            String command = "";
            foreach (Student s in AfterEditStudArr)
            {
                String prefix = " Update Student Set email='" + s.Email + "', firstname='" + s.First_name + "', lastname='" + s.Last_name + "', birthday='" + s.Birthday + "', phone='" + s.Phone + "', emailParent='" + s.EmailParent + "'";
                prefix += " where email ='" + s.Email + "'";
                command += prefix;
            }
            return command;

        }



        //get Student selected Details
        public List<Student> GetStudDetails(int idClass)
        {
            SqlConnection con = null;
            List<Student> StudentdetList = new List<Student>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT s.birthday, sic.idClass, s.firstname, s.lastname, s.email, s.phone, s.emailParent, s.numofpoints ";
                selectSTR += "from student s inner join StudentInClass sic on s.email = sic.email ";
                selectSTR += "where sic.idClass =" + idClass;

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Student s = new Student();
                    s.Email = (string)dr["email"];
                    s.First_name = (string)dr["firstname"];
                    s.Last_name = (string)dr["lastname"];
                    s.Birthday = (string)dr["birthday"];
                    s.Phone = (string)dr["phone"];
                    s.NumOfPoints = Convert.ToInt32(dr["numofpoints"]);
                    s.IdClass = Convert.ToInt32(dr["idClass"]);
                    s.EmailParent = (string)dr["emailParent"];
                    StudentdetList.Add(s);
                }
                return StudentdetList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        //21.05.2021
        public int addPurchPrize(PurchasePrize purchPrize)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandaddPurchPrize(purchPrize);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommandaddPurchPrize(PurchasePrize purchPrize)
        {
            StringBuilder sb = new StringBuilder();
                
            String prefix = " insert into PurchasesPrizes (idClass, emailStudent, prizeType , purchaseIndex, improvmentBy) ";
            sb.AppendFormat("values({0}, '{1}', '{2}', {3}, '{4}')", purchPrize.IdClass, purchPrize.EmailStudent, purchPrize.PrizeType, purchPrize.PurchaseIndex, purchPrize.ImprovmentBy);
            String command = prefix + sb.ToString();
            
            return command;
        }

        //Get students of parents
        public List<ClassOfTeacher> GetParentStudents(string email)
        {
            SqlConnection con = null;
            List<ClassOfTeacher> StudentsList = new List<ClassOfTeacher>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT  * FROM (SELECT s.firstname, s.lastname, c.layer, mc.classnum, s.email, ";
                selectSTR += "ROW_NUMBER() OVER (PARTITION BY s.email ORDER BY firstname) AS RowNumber  ";
                selectSTR += "FROM Student s inner join StudentInClass sic on s.email=sic.email ";
                selectSTR += "left join Motherclass mc on sic.idClass=mc.idClass ";
                selectSTR += "left join Class c on sic.idClass=c.id ";
                selectSTR += "WHERE s.emailParent='" + email + "' and classnum is not null) a ";
                selectSTR += "WHERE RowNumber = 1";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    ClassOfTeacher cot = new ClassOfTeacher();
                    cot.FirstName = (string)dr["firstName"];
                    cot.LastName = (string)dr["lastname"];
                    cot.Layer = (string)dr["layer"];
                    cot.Subjname = (string)dr["email"];
                    cot.Classnum = Convert.ToInt32(dr["classnum"]);
                    StudentsList.Add(cot);
                }
                
                return StudentsList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //get students grades
        public List<int> getGrades(int idClass, int examNum)
        {
            SqlConnection con = null;
            List<int> grades = new List<int>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select grade ";
                selectSTR += "from class c inner join exam e on c.id=e.idclass ";
                selectSTR += "where c.id=" + idClass + " and examNum=" + examNum + " order by grade";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    int grade = Convert.ToInt32(dr["grade"]);
                    grades.Add(grade);

                }
                return grades;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //forgot password
        public User Forgot(string email)//  אימות האם המשתמש אגן רשום למערכת
        {

            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT distinct * FROM AllUsers WHERE email= '" + email + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                User u = new User();
                while (dr.Read()) //הבאת רשומה רשומה
                {   // Read till the end of the data into a row

                    u.Email = (string)dr["email"];
                    u.Pass = (string)dr["pass"];
                }

                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("betterme.sup@gmail.com");
                    mail.To.Add(u.Email);
                    mail.Subject = "Better Me Support";
                    mail.Body = "!שלום משתמש יקר" + Environment.NewLine + "איתרנו את הסיסמא שלך במערכת" + Environment.NewLine +
                    ":סיסמתך היא  " + u.Pass;

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("betterme.sup@gmail.com", "maorking123");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    //MessageBox.Show("mail Send");
                }
                catch (Exception ex)
                {
                    throw (ex);
                    //MessageBox.Show(ex.ToString());
                }
                return u;

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }



        //Get students classes
        public List<ClassOfTeacher> FindClassesStudent(string email)
        {
            SqlConnection con = null;
            List<ClassOfTeacher> ClassesList = new List<ClassOfTeacher>();

            try
            {
                con = connect("DBConnectionString");

                //כיתות אם
                String selectSTR = " select st.firstName, c.id,c.layer, s.subjname, mc.classnum ";
                selectSTR += "from student st inner join studentinclass sin on st.email = sin.email inner join class c on sin.idClass = c.id inner join Subj s on c.subjId = s.id ";
                selectSTR += "left join ProfessionalClass pc on c.id = pc.idClass left join Motherclass mc on c.id = mc.idClass ";
                selectSTR += "where sin.email ='" + email + "' and pc.levelgroup IS NULL";
                selectSTR += " order by layer, mc.classnum";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    ClassOfTeacher cot = new ClassOfTeacher();
                    cot.FirstName = (string)dr["firstName"];
                    cot.IdClass = Convert.ToInt32(dr["id"]);
                    cot.Layer = (string)dr["layer"];
                    cot.Subjname = (string)dr["subjname"];
                    cot.Classnum = Convert.ToInt32(dr["classnum"]);
                    ClassesList.Add(cot);
                }

                //כיתות מקצועיות
                selectSTR = " select st.firstName, c.id,c.layer, s.subjname, pc.levelgroup, pc.groupnum ";
                selectSTR += "from student st inner join studentinclass sin on st.email = sin.email inner join class c on sin.idClass = c.id inner join Subj s on c.subjId = s.id ";
                selectSTR += "left join ProfessionalClass pc on c.id = pc.idClass left join Motherclass mc on c.id = mc.idClass ";
                selectSTR += "where sin.email ='" + email + "' and mc.classnum IS NULL";
                selectSTR += " order by layer, pc.levelgroup, pc.groupnum ";

                con.Close();
                con = connect("DBConnectionString");


                SqlCommand cmd2 = new SqlCommand(selectSTR, con);
                dr = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    ClassOfTeacher cot = new ClassOfTeacher();
                    cot.FirstName = (string)dr["firstName"];
                    cot.IdClass = Convert.ToInt32(dr["id"]);
                    cot.Layer = (string)dr["layer"];
                    cot.Subjname = (string)dr["subjname"];
                    cot.Levelgroup = (string)dr["levelgroup"];
                    cot.Groupnum = Convert.ToInt32(dr["groupnum"]);
                    ClassesList.Add(cot);
                }
                return ClassesList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //get student points
        public int GetStudentPoints(string studentEmail)
        {
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT numofpoints FROM Student where email='" + studentEmail + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    int points = Convert.ToInt32(dr["numofpoints"]);                    
                    return points;
                }
                return -1;
            }

            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //delete Item 
        public void DeleteItem(StudentItem StuItem)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                int points = CheckItemPrice(StuItem);
                con = connect("DBConnectionString");
                String selectSTR = "DELETE FROM StudentItem where emailStudent='" + StuItem.EmailStudent + "' and topCss='" + StuItem.TopCss + "' and leftCss='" + StuItem.LeftCss + "' and idItem=" + StuItem.IdItem;
                selectSTR += " UPDATE Student SET numofpoints+=" + points + " where email='" + StuItem.EmailStudent + "'";
                cmd = CreateCommand(selectSTR, con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //check item price
        public int CheckItemPrice(StudentItem StuItem)
        {
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select points from item where id = " + StuItem.IdItem;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    int points =  Convert.ToInt32(dr["points"]);
                    return points;
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //21.05.2021 add exams
        public int InsertExams(List<Exam> exams)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandExam(exams);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommandExam(List<Exam> exams)
        {
            String command = "";

            foreach (var exa in exams)
            {
                StringBuilder sb = new StringBuilder();
                String prefix = " insert into Exam (idclass, examNum, studentEmail, grade) ";
                sb.AppendFormat("values('{0}', '{1}', '{2}', '{3}',)", exa.IdClass, exa.ExamNum, exa.StudentEmail, exa.Grade);
                command += prefix + sb.ToString();
            }
            return command;
        }


        public int InsertExamsFromExcel(List<Student> exams)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandExamFromExcel(exams);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommandExamFromExcel(List<Student> exams)
        {
            String command = "";
            String prefix = "";
            String prefix2 = "";
            prefix2 = "delete from exam";
            command = prefix2;
            foreach (var exa in exams)
            {
                foreach (var ae in exa.AllExam)
                {
                    StringBuilder sb = new StringBuilder();
                    prefix = " insert into Exam (idclass, examNum, studentEmail, grade) ";
                    sb.AppendFormat("values('{0}', '{1}', '{2}', '{3}')", exa.IdClass, ae.ExamNum, exa.Email, ae.Grade);
                    command += prefix + sb.ToString();
                }
                 
            }
            return command;
        }


        public List<Exam> GetStudentExam(int idClass, string studentEmail)
        {
            SqlConnection con = null;
            List<Exam> examList = new List<Exam>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT distinct s.firstname, s.lastname, e.* FROM Exam e inner join studentinClass sic on e.idclass = sic.idclass inner join Student s on e.studentEmail = s.email  where e.idclass=" + idClass + " and e.studentEmail='" + studentEmail + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Exam ex = new Exam();
                    ex.IdClass = Convert.ToInt32(dr["idclass"]);
                    ex.ExamNum = Convert.ToInt32(dr["ExamNum"]);
                    ex.StudentEmail = (string)dr["studentEmail"];
                    ex.Grade = Convert.ToInt32(dr["grade"]);
                    examList.Add(ex);
                }
                return examList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public int UpdateG(int id, Exam e)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildUpdateCommandUpdateGrade(id, e);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private String BuildUpdateCommandUpdateGrade(int id, Exam e)
        {
            String prefix = " UPDATE Exam SET grade=" + e.Grade;
            prefix += " where idclass=" + e.IdClass + " and examNum=" + e.ExamNum + " and studentEmail='" + e.StudentEmail + "'";
            return prefix;
        }

        public void DeleteExam(int idclass, string studentEmail)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "DELETE FROM exam where grade= (select top 1 grade FROM exam where idClass=" + idclass + " and studentEmail='" + studentEmail + "' order by grade asc)";
                cmd = CreateCommand(selectSTR, con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //insert student Item
        public int placeItems(List<StudentItem> studentItems)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandplaceItems(studentItems);
                        
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);

            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        private String BuildInsertCommandplaceItems(List<StudentItem> studentItems)
        {
            int count = 0;
            String prefix = "";
            String command = "";

            foreach (var s in studentItems)
            {
                StringBuilder sb = new StringBuilder();
                prefix = "";
                if (count == 0)
                {
                    prefix += " delete StudentItem where emailStudent='" + s.EmailStudent + "'";
                    count++;
                }
                prefix += " INSERT INTO StudentItem (idItem, emailStudent , topCss, leftCss, img) ";
                sb.AppendFormat("Values({0}, '{1}', '{2}', '{3}', '{4}')", s.IdItem, s.EmailStudent, s.TopCss, s.LeftCss, s.Img);
                command += prefix + sb.ToString();
            }    
            
            return command;
        }
      

        //get Student Items
        public List<StudentItem> GetStudentItems(string studentEmail)
        {
            SqlConnection con = null;
            List<StudentItem> ItemsList = new List<StudentItem>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "select si.*,i.img from StudentItem si inner join Item i on si.idItem = i.id inner join student s on si.emailStudent = s.email";
                selectSTR += " where email='" + studentEmail + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    StudentItem si = new StudentItem();
                    si.Id = Convert.ToInt32(dr["id"]);
                    si.IdItem = Convert.ToInt32(dr["idItem"]);
                    si.EmailStudent = (string)dr["emailStudent"];
                    si.TopCss = (string)dr["topCss"];
                    si.LeftCss = (string)dr["leftCss"];
                    si.Img = (string)dr["img"];
                    
                    ItemsList.Add(si);
                }
                return ItemsList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }



        //new
        public int InsertRandomAddPoints(RandomAddPoints[] RandomAddPointsArr)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandRandomAddPoints(RandomAddPointsArr);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);

            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommandRandomAddPoints(RandomAddPoints[] RandomAddPointsArr)
        {

            String command = "";
            for (int i = 0; i < RandomAddPointsArr.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                String prefix = " INSERT INTO RandomAddPoints (idClass, comment , points) ";
                sb.AppendFormat("Values({0}, '{1}', {2})", RandomAddPointsArr[i].IdClass, RandomAddPointsArr[i].Comment, RandomAddPointsArr[i].Points);
                command += prefix + sb.ToString();
            }
            return command;
        }


        public List<RandomAddPoints> GetRandomAddPoints(int classId)
        {
            SqlConnection con = null;
            List<RandomAddPoints> RandomAdd = new List<RandomAddPoints>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM RandomAddPoints where idClass=" + classId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    RandomAddPoints AR = new RandomAddPoints();
                    AR.IdClass = Convert.ToInt32(dr["idClass"]);
                    AR.Comment = (string)dr["comment"];
                    AR.Points = Convert.ToInt32(dr["points"]);
                    RandomAdd.Add(AR);
                }
                return RandomAdd;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public void Deleterap(RandomAddPoints RAP)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "DELETE FROM RandomAddPoints where idClass=" + RAP.IdClass + " and comment='" + RAP.Comment + "' and points=" + RAP.Points;
                cmd = CreateCommand(selectSTR, con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }





        //update student points
        public int updatePoints(int StudentPoints, int ItemPoints, string studentEmail)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildUpdateCommandStudentPoints(StudentPoints, ItemPoints, studentEmail);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildUpdateCommandStudentPoints(int StudentPoints, int ItemPoints, string studentEmail)
        {
            int newPoints = StudentPoints - ItemPoints;
            String prefix = " UPDATE Student SET numofpoints=" + newPoints;
            prefix += " where email='" + studentEmail + "'";
            return prefix;
        }



        //buy item
        public int BuyItem(int itemId, string studentEmail)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandBuyItem(itemId, studentEmail);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommandBuyItem(int itemId, string studentEmail)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            string dateToday = DateTime.Now.ToString("dd/MM/yyyy");
            String prefix = " INSERT INTO Purchases (idItem, PurcDate, emailStudent) ";
            sb.AppendFormat("Values({0}, '{1}', '{2}')", itemId, dateToday, studentEmail);
            command = prefix + sb.ToString();
            return command;
        }


        public void DeleteRemarks(Remark R)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "DELETE FROM Remarks where idClass=" + R.IdClass + " and remarkname='" + R.Remarkname + "' and takenpoints=" + R.Takepoints;
                cmd = CreateCommand(selectSTR, con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public void DeletePwo(PrizeWithout PWO)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "DELETE FROM PrizeWithout where idClass=" + PWO.IdClass + " and prizetype='" + PWO.Prizetype + "' and priceinstore=" + PWO.Priceinstore + " and bonus=" + PWO.Bonus;
                cmd = CreateCommand(selectSTR, con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public void Deletepw(PrizeWith PW)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "DELETE FROM PrizeWith where idClass=" + PW.IdClass + " and priceinstore=" + PW.Priceinstore + " and prizetype='" + PW.Prizetype;
                selectSTR += "' and referencesTo='" + PW.ReferencesTo + "' and improvmentBy='" + PW.ImprovmentBy + "' and valueimprovement=" + PW.Valueimprovement;
                cmd = CreateCommand(selectSTR, con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }



        //add item to db
        //הכנסת כיתה
        public int addItem(Item item)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandItem(item);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        private String BuildInsertCommandItem(Item it)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            String prefix = " INSERT INTO Item (id, nameItem, img, points) ";
            sb.AppendFormat("Values({0}, '{1}', '{2}', {3})", it.Id, it.NameItem, it.Img, it.Points);
            command = prefix + sb.ToString();
            return command;
        }


        //get prize with for student
        public List<PrizeWith> getPrizeWithForStudent(int classId)
        {
            SqlConnection con = null;
            List<PrizeWith> prizeWithList = new List<PrizeWith>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT pw.*  FROM PrizeWith pw where idClass=" + classId;
                selectSTR += " and Not Exists(select prizeType, purchaseIndex, improvmentBy";
                selectSTR += " from PurchasesPrizes pp";
                selectSTR += " where pw.prizeType = pp.prizeType and pw.improvmentBy=pp.improvmentBy)";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    PrizeWith pw = new PrizeWith();
                    pw.Id = Convert.ToInt32(dr["id"]);
                    pw.IdClass = Convert.ToInt32(dr["idClass"]);
                    pw.Priceinstore = Convert.ToInt32(dr["priceinstore"]);
                    pw.Prizetype = (string)dr["prizetype"];
                    pw.ReferencesTo = (string)dr["referencesTo"];
                    pw.ImprovmentBy = (string)dr["improvmentBy"];
                    pw.Valueimprovement = Convert.ToInt32(dr["valueimprovement"]);
                    pw.Bonus = Convert.ToInt32(dr["bonus"]);
                    prizeWithList.Add(pw);
                }
                return prizeWithList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //ExistingClass html

        public List<PrizeWith> GetPrizeWith(int classId)
        {
            SqlConnection con = null;
            List<PrizeWith> prizeWithList = new List<PrizeWith>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM PrizeWith where idClass=" + classId;

                //String selectSTR = "SELECT pw.*  FROM PrizeWith pw where idClass=" + classId;
                //selectSTR += " and Not Exists(select prizeType, purchaseIndex, improvmentBy";
                //selectSTR += " from PurchasesPrizes pp";
                //selectSTR += " where pw.prizeType = pp.prizeType and pw.improvmentBy=pp.improvmentBy)";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    PrizeWith pw = new PrizeWith();
                    pw.Id = Convert.ToInt32(dr["id"]);
                    pw.IdClass = Convert.ToInt32(dr["idClass"]);
                    pw.Priceinstore = Convert.ToInt32(dr["priceinstore"]);
                    pw.Prizetype = (string)dr["prizetype"];
                    pw.ReferencesTo = (string)dr["referencesTo"];
                    pw.ImprovmentBy = (string)dr["improvmentBy"];
                    pw.Valueimprovement = Convert.ToInt32(dr["valueimprovement"]);
                    pw.Bonus = Convert.ToInt32(dr["bonus"]);
                    prizeWithList.Add(pw);
                }
                return prizeWithList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //get prizewithout for student in class
        public List<PrizeWithout> getPrizeWithoutForStudent(int classId)
        {
            SqlConnection con = null;
            List<PrizeWithout> PrizeWithoutList = new List<PrizeWithout>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT pwo.*  FROM PrizeWithout pwo where idClass=" + classId;
                selectSTR += " and Not Exists(select prizeType, purchaseIndex";
                selectSTR += " from PurchasesPrizes pp";
                selectSTR += " where pwo.prizeType = pp.prizeType and pp.improvmentBy='')";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    PrizeWithout pwo = new PrizeWithout();
                    pwo.Id = Convert.ToInt32(dr["id"]);
                    pwo.IdClass = Convert.ToInt32(dr["idClass"]);
                    pwo.Prizetype = (string)dr["prizetype"];
                    pwo.Priceinstore = Convert.ToInt32(dr["priceinstore"]);
                    pwo.Bonus = Convert.ToInt32(dr["bonus"]);
                    PrizeWithoutList.Add(pwo);
                }
                return PrizeWithoutList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public List<PrizeWithout> GetPrizeWithout(int classId)
        {
            SqlConnection con = null;
            List<PrizeWithout> PrizeWithoutList = new List<PrizeWithout>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM PrizeWithout where idClass=" + classId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    PrizeWithout pwo = new PrizeWithout();
                    pwo.Id = Convert.ToInt32(dr["id"]);
                    pwo.IdClass = Convert.ToInt32(dr["idClass"]);
                    pwo.Prizetype = (string)dr["prizetype"];
                    pwo.Priceinstore = Convert.ToInt32(dr["priceinstore"]);
                    pwo.Bonus = Convert.ToInt32(dr["bonus"]);
                    PrizeWithoutList.Add(pwo);
                }
                return PrizeWithoutList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }



        //rules html

        //rules get remarks
        public List<Remark> GetRemark(int classId)
        {
            SqlConnection con = null;
            List<Remark> remark = new List<Remark>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM Remarks where idClass=" + classId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Remark r = new Remark();
                    r.Id = Convert.ToInt32(dr["id"]);
                    r.IdClass = Convert.ToInt32(dr["idClass"]);
                    r.Remarkname = (string)dr["remarkname"];
                    r.Takepoints = Convert.ToInt32(dr["takenpoints"]);
                    remark.Add(r);
                }
                return remark;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //lose points rules
        public int InsertRemark(Remark[] subPointsArr)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandRemark(subPointsArr);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected; //stop!
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommandRemark(Remark[] subPointsArr)
        {
            String command = "";
            for (int i = 0; i < subPointsArr.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                String prefix = " INSERT INTO Remarks (idClass, remarkname ,takenpoints) ";
                sb.AppendFormat("Values({0}, '{1}', {2})", subPointsArr[i].IdClass, subPointsArr[i].Remarkname, subPointsArr[i].Takepoints);
                command += prefix + sb.ToString();
            }
            return command;
        }
        

        //Prize With
        public int InsertPrizesWith(PrizeWith[] prizewith)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandPrizewith(prizewith);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        private String BuildInsertCommandPrizewith(PrizeWith[] prizewith)
        {
            String command = "";
            for (int i = 0; i < prizewith.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                String prefix = " INSERT INTO PrizeWith (idClass, priceinstore ,prizetype, referencesTo, improvmentBy, valueimprovement, bonus) ";
                sb.AppendFormat("Values({0}, {1}, '{2}', '{3}', '{4}', {5} , {6})", prizewith[i].IdClass, prizewith[i].Priceinstore, prizewith[i].Prizetype, prizewith[i].ReferencesTo, prizewith[i].ImprovmentBy, prizewith[i].Valueimprovement, prizewith[i].Bonus);
                command += prefix + sb.ToString();
            }
            return command;
        }


        //Prize Without
        public int InsertPrizesWithout(PrizeWithout[] prizewithout)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandPrizewithout(prizewithout);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommandPrizewithout(PrizeWithout[] prizewithout)
        {
            String command = "";
            for (int i = 0; i < prizewithout.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                String prefix = " INSERT INTO PrizeWithout (idClass, prizetype ,priceinstore, bonus) ";
                sb.AppendFormat("Values({0}, '{1}', {2} , {3})", prizewithout[i].IdClass, prizewithout[i].Prizetype, prizewithout[i].Priceinstore, prizewithout[i].Bonus);
                command += prefix + sb.ToString();
            }
            return command;
        }

        //create class html


        //return subj ID
        public int ReadSubj(string SubjName)
        {
            SqlConnection con = null;
            Subj s = new Subj();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM Subj";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    if (SubjName == (string)dr["subjname"])
                        return Convert.ToInt32(dr["id"]);
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //הוספת הורים
        public int InsertParents(List<Parent> Parents)
        {
            int parentsTemp = CheckParentsExist(Parents);
            List<Parent> ListParReady = parentFinalList();

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandParents(ListParReady);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommandParents(List<Parent> ListParReady)
        {
            String command = "";

            foreach (var par in ListParReady)
            {
                StringBuilder sb = new StringBuilder();
                String prefix = " insert into Parent (email, pass, fullname, phone) ";
                sb.AppendFormat("values('{0}', '{1}', '{2}', '{3}')", par.Email, par.Pass, par.Fullname, par.Phone); 
                command += prefix + sb.ToString(); //somehow its switching the full name and phone
                command += " INSERT INTO AllUsers (email , pass) Values('" + par.Email + "', '" + par.Pass + "') ";
            }
            return command;
        }


        //check parent exist      
        public int CheckParentsExist(List<Parent> Parents)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandParentsTemp(Parents);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        //insert to temp parent
        private String BuildInsertCommandParentsTemp(List<Parent> Parents)
        {
            String command = "delete from ParentTemp ";
            foreach (var par in Parents)
            {
                StringBuilder sb = new StringBuilder();
                String prefix = " insert into ParentTemp (email, pass, fullname, phone) ";
                sb.AppendFormat("values('{0}', '{1}', '{2}', '{3}')", par.Email, par.Pass, par.Fullname, par.Phone);
                command += prefix + sb.ToString();
            }
            return command;
        }

        //get parents final list
        public List<Parent> parentFinalList()
        {
            SqlConnection con = null;
            List<Parent> par = new List<Parent>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT p.* FROM ParentTemp p where p.email NOT IN (SELECT email FROM Parent)";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Parent p = new Parent();
                    p.Email = (string)dr["email"]; ;
                    p.Pass = (string)dr["pass"]; ;
                    p.Fullname = (string)dr["fullname"];
                    p.Phone = (string)dr["phone"];
                    par.Add(p);
                }
                return par;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //start

        //הכנסת תלמיד למערכת
        public int InsertStudents(List<Student> Students)
        {
            int studentTemp = CheckStudentsExist(Students);
            List<Student> ListStuReady = studentsFinalList();

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandStudents(ListStuReady);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommandStudents(List<Student> ListStuReady)
        {
            String command = "";

            foreach (Student s in ListStuReady)
            {
                StringBuilder sb = new StringBuilder();
                String prefix = " INSERT INTO Student (email , pass, firstname, lastname, birthday, phone, numofpoints, emailParent) ";
                sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}')", s.Email, s.Pass, s.First_name, s.Last_name, s.Birthday, s.Phone, s.NumOfPoints, s.EmailParent);
                command += prefix + sb.ToString();     
                command += " INSERT INTO AllUsers (email , pass) Values('" + s.Email + "', '" + s.Pass + "') ";     
                
            }
            return command;
        }


        //TEMP Students       
        public int CheckStudentsExist(List<Student> Students)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandStudentsTemp(Students);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        //insert to temp student
        private String BuildInsertCommandStudentsTemp(List<Student> Students)
        {
            String command = "delete from StudentTemp ";
            foreach (var s in Students)
            {
                StringBuilder sb = new StringBuilder();
                String prefix = " insert into StudentTemp (email, pass, firstname, lastname, birthday, phone, numofpoints, emailParent) ";
                sb.AppendFormat("values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}')", s.Email, s.Pass, s.First_name, s.Last_name, s.Birthday, s.Phone, s.NumOfPoints, s.EmailParent);
                command += prefix + sb.ToString();
                StringBuilder sb2 = new StringBuilder();
                prefix = " INSERT INTO StudentInClass (email , idClass) ";
                sb2.AppendFormat("Values('{0}', {1})", s.Email, s.IdClass);
                command += prefix + sb2.ToString();
            }
            return command;
        }


        //get students final list
        public List<Student> studentsFinalList()
        {
            SqlConnection con = null;
            List<Student> StuList = new List<Student>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT st.* FROM StudentTemp st where st.email NOT IN (SELECT email FROM Student)";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    Student s = new Student();
                    s.Email = (string)dr["email"]; 
                    s.Pass = (string)dr["pass"]; 
                    s.First_name = (string)dr["firstname"];
                    s.Last_name = (string)dr["lastname"];
                    s.Birthday = (string)dr["birthday"];
                    s.Phone = (string)dr["phone"];
                    s.NumOfPoints = Convert.ToInt32(dr["numofpoints"]);
                    s.EmailParent = (string)dr["emailParent"];
                    StuList.Add(s);
                }
                return StuList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //end









        //בדיקה האם קיימת כיתת אם
        public ClassOfTeacher CheckMotherClasss(string Layer, string Subjname, int Classnum)
        {
            SqlConnection con = null;
            ClassOfTeacher cot = new ClassOfTeacher();

            try
            {
                con = connect("DBConnectionString");

                //כיתות אם
                String selectSTR = " select s.subjname ,c.layer, mc.classnum ";
                selectSTR += "from Class c inner join Motherclass mc on c.id = mc.idClass ";
                selectSTR += "inner join Subj s on c.subjId = s.id ";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    if ((Subjname == (string)dr["subjname"]) && (Layer == (string)dr["layer"]) && (Classnum == Convert.ToInt32(dr["classnum"])))
                    {
                        cot.Subjname = Subjname;
                        cot.Layer = Layer;
                        cot.Classnum = Classnum;
                        return cot;
                    }
                }

                return null;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        //בדיקת האם קיימת כיתה מקצועית
        public ClassOfTeacher CheckProClasss(string Layer, string Subjname, string Levelgroup, int Groupnum)
        {
            SqlConnection con = null;
            ClassOfTeacher cot = new ClassOfTeacher();

            try
            {
                con = connect("DBConnectionString");

                //כיתות מקצועיות
                String selectSTR = " select s.subjname ,c.layer, pc.levelgroup, pc.groupnum ";
                selectSTR += "from Class c inner join ProfessionalClass pc on c.id = pc.idClass ";
                selectSTR += "inner join Subj s on c.subjId = s.id ";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    if ((Subjname == (string)dr["subjname"]) && (Layer == (string)dr["layer"]) && (Levelgroup == (string)dr["levelgroup"]) && (Groupnum == Convert.ToInt32(dr["groupnum"])))
                    {
                        cot.Subjname = Subjname;
                        cot.Layer = Layer;
                        cot.Levelgroup = Levelgroup;
                        cot.Groupnum = Groupnum;
                        return cot;
                    }
                }

                return null;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        //הכנסת כיתה מקצועית
        public int InsertProClass(ProfessionalClass ProData)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandProClass(ProData);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommandProClass(ProfessionalClass pd)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            String prefix = " INSERT INTO ProfessionalClass (idClass, levelgroup, groupnum) ";
            sb.AppendFormat("Values({0}, '{1}', {2})", pd.IdClass, pd.Levelgroup, pd.Groupnum);
            command = prefix + sb.ToString();
            return command;
        }



        //הכנסת כיתת אם
        public int InsertMotherClass(Motherclass MotherData)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandMotherClass(MotherData);
            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private String BuildInsertCommandMotherClass(Motherclass md)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            String prefix = " INSERT INTO Motherclass (idClass, classnum) ";
            sb.AppendFormat("Values({0}, {1})", md.IdClass, md.Classnum);
            command = prefix + sb.ToString();
            return command;
        }

        //הכנסת כיתה
        public int InsertClass(Class ClassData)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (SqlException ex)
            {
                throw new Exception("The server could not get to the database", ex);
            }

            String cStr = BuildInsertCommandClass(ClassData);
            cmd = CreateCommand(cStr, con);

            try
            {// אנחנו רוצים לשמור על האיידי של הכיתה בשביל להכניס את זה לכיתת אם
                int numEffected = (Int32)cmd.ExecuteScalar(); //excute scalar
                return numEffected;
            }
            catch (SqlException ex)
            {
                throw (ex);
                //throw new Exception("Cannot insert", ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        private String BuildInsertCommandClass(Class cd)
        {
            String command;
            StringBuilder sb = new StringBuilder();
            String prefix = " INSERT INTO Class (layer, subjId, idTeacher) output inserted.id ";
            sb.AppendFormat("Values('{0}', {1}, {2})", cd.Layer, cd.SubjId, cd.IdTeacher);
            command = prefix + sb.ToString();
            return command;
        }



        private String BuildInsertCommandSubj(Subj cs)
        {
            String command = "";
            StringBuilder sb = new StringBuilder();
            String prefix = " INSERT INTO Subj (subjname) ";
            sb.AppendFormat("Values('{0}')", cs.SubjName);
            command += prefix + sb.ToString();
            return command;
        }




        //home html



        //בדיקת התחברות תלמיד
        public Student ReadStudent(string email, string pass)
        {
            SqlConnection con = null;
            Student s = new Student();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM Student where email='" + email + "' and pass='" + pass + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    s.Email = email;
                    s.Pass = pass;
                    s.First_name = (string)dr["firstname"];
                    s.Last_name = (string)dr["lastname"];
                    s.Birthday = (string)dr["birthday"];
                    s.Phone = (string)dr["phone"];
                    s.NumOfPoints = Convert.ToInt32(dr["numofpoints"]);
                    s.EmailParent = (string)dr["emailParent"];
                    return s;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //בדיקת התחברות מורה
        public Teacher ReadTeacher(string email, string pass)
        {
            SqlConnection con = null;
            Teacher t = new Teacher();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM Teacher where email='" + email + "' and pass='" + pass + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    t.Id = Convert.ToInt32(dr["id"]);
                    t.Email = email;
                    t.Pass = pass;
                    t.FirstName = (string)dr["firstName"];
                    t.LastName = (string)dr["lastName"];
                    return t;
                    
                }
                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //בדיקת התחברות הורה
        public Parent ReadParent(string email, string pass)
        {
            SqlConnection con = null;
            Parent p = new Parent();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "SELECT * FROM Parent where email='" + email + "' and pass='" + pass + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    p.Email = email;
                    p.Pass = pass;
                    p.Fullname = (string)dr["fullname"];
                    p.Phone = (string)dr["phone"];
                    return p;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }


        //TeacherClasses html

        //שליפת כיתות המורה לפי 
        // במערכת ID
        public List<ClassOfTeacher> FindClasses(int teachId)
        {
            SqlConnection con = null;
            List<ClassOfTeacher> ClassesList = new List<ClassOfTeacher>();

            try
            {
                con = connect("DBConnectionString");

                //כיתות אם
                String selectSTR = " select t.firstName, c.id,c.layer, s.subjname, mc.classnum ";
                selectSTR += "from Class c inner join Teacher t on c.idTeacher = t.id inner join Subj s on c.subjId = s.id ";
                selectSTR += "left join ProfessionalClass pc on c.id = pc.idClass left join Motherclass mc on c.id = mc.idClass ";
                selectSTR += "where c.idTeacher =" + teachId + " and pc.levelgroup IS NULL and classnum is not null ";
                selectSTR += "order by layer, mc.classnum";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    ClassOfTeacher cot = new ClassOfTeacher();
                    cot.FirstName = (string)dr["firstName"];
                    cot.IdClass = Convert.ToInt32(dr["id"]);
                    cot.Layer = (string)dr["layer"];
                    cot.Subjname = (string)dr["subjname"];
                    cot.Classnum = Convert.ToInt32(dr["classnum"]);
                    ClassesList.Add(cot);
                }

                //כיתות מקצועיות
                selectSTR = " select t.firstName, c.id,c.layer, s.subjname, pc.levelgroup, pc.groupnum ";
                selectSTR += "from Class c inner join Teacher t on c.idTeacher = t.id inner join Subj s on c.subjId = s.id ";
                selectSTR += "left join ProfessionalClass pc on c.id = pc.idClass left join Motherclass mc on c.id = mc.idClass ";
                selectSTR += "where c.idTeacher =" + teachId + " and mc.classnum IS NULL and levelgroup is not null and groupnum is not null";
                selectSTR += " order by layer, pc.levelgroup, pc.groupnum ";


                con.Close();
                con = connect("DBConnectionString");


                SqlCommand cmd2 = new SqlCommand(selectSTR, con);
                dr = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    ClassOfTeacher cot = new ClassOfTeacher();
                    cot.FirstName = (string)dr["firstName"];
                    cot.IdClass = Convert.ToInt32(dr["id"]);
                    cot.Layer = (string)dr["layer"];
                    cot.Subjname = (string)dr["subjname"];
                    cot.Levelgroup = (string)dr["levelgroup"];
                    cot.Groupnum = Convert.ToInt32(dr["groupnum"]);
                    ClassesList.Add(cot);
                }
                return ClassesList;
            }
            catch (Exception ex)
            {
               throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }






        //פקודות אוטומטיות
        public SqlConnection connect(String conString)
        {
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }


        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = CommandSTR;
            cmd.CommandTimeout = 10;
            cmd.CommandType = System.Data.CommandType.Text;
            return cmd;
        }
    }
}