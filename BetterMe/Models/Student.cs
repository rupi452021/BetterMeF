using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BetterMe.Models.DAL;

namespace BetterMe.Models
{
    public class Student
    {
        string email;
        string pass;
        string first_name;
        string last_name;
        string birthday;
        string phone;
        int numOfPoints;
        int idClass; //for the sql
        string emailParent;
        List<Exam> allExam;


        public string Email { get => email; set => email = value; }
        public string Pass { get => pass; set => pass = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public string Birthday { get => birthday; set => birthday = value; }
        public string Phone { get => phone; set => phone = value; }
        public int NumOfPoints { get => numOfPoints; set => numOfPoints = value; }
        public int IdClass { get => idClass; set => idClass = value; }
        public string EmailParent { get => emailParent; set => emailParent = value; }
        public List<Exam> AllExam { get => allExam; set => allExam = value; }

        public Student(string email, string pass, string first_name, string last_name, string birthday, string phone, int numOfPoints, int idClass, string emailParent, List<Exam> allExam)
        {
            Email = email;
            Pass = pass;
            First_name = first_name;
            Last_name = last_name;
            Birthday = birthday;
            Phone = phone;
            NumOfPoints = numOfPoints;
            IdClass = idClass;
            EmailParent = emailParent;
            AllExam = allExam;
        }

        public Student()
        {            
        }

        public void InsertStudents(List<Student> Students)
        {
            DBServices dbs = new DBServices();
            dbs.InsertStudents(Students);
        }


        public void updatePoints(int StudentPoints, int ItemPoints, string studentEmail)
        {
            DBServices dbs = new DBServices();
            dbs.updatePoints(StudentPoints, ItemPoints, studentEmail);
        }

        public Student Read(string email, string pass)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadStudent(email,pass);             
        }

        public int GetStudentPoints(string studentEmail)
        {
            DBServices dbs = new DBServices();
            return dbs.GetStudentPoints(studentEmail);
        }



        public List<int> getGrades(int idClass, int examNum)
        {
            DBServices dbs = new DBServices();
            return dbs.getGrades(idClass, examNum);
        }


        public List<Student> GetStudDetails(int idClass)
        {
            DBServices dbs = new DBServices();
            return dbs.GetStudDetails(idClass);
        }

        public void updateStud(int idClass, List<Student> AfterEditStudArr)
        {
            DBServices dbs = new DBServices();
            dbs.updateStud(idClass, AfterEditStudArr);
        }

        public List<Student> ReadExams(int classId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetExam(classId);
        }

        public void updateStudentPass(string email, string newPass)
        {
            DBServices dbs = new DBServices();
            dbs.updateStudentPass(email, newPass);
        }


        public Student GetStuDetails(string emailStu)
        {
            DBServices dbs = new DBServices();
            return dbs.GetStuDetails(emailStu);
        }


        public void InsertExams(List<Student> exams)
        {
            DBServices dbs = new DBServices();
            dbs.InsertExamsFromExcel(exams);
        }
    }
}