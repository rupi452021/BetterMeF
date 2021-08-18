using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class Exam
    {
        int idClass;
        int examNum;
        int weightPrecent;
        string studentEmail;
        int grade;

        public int IdClass { get => idClass; set => idClass = value; }
        public int ExamNum { get => examNum; set => examNum = value; }
        public int WeightPrecent { get => weightPrecent; set => weightPrecent = value; }
        public string StudentEmail { get => studentEmail; set => studentEmail = value; }
        public int Grade { get => grade; set => grade = value; }

        public Exam(int idClass, int examNum, int weightPrecent, string studentEmai, int grade)
        {
            IdClass = idClass;
            ExamNum = examNum;
            WeightPrecent = weightPrecent;
            StudentEmail = studentEmail;
            Grade = grade;
        }


        public Exam() { }


        //try


        public void InsertExams(List<Exam> exams)
        {
            DBServices dbs = new DBServices();
            dbs.InsertExams(exams);
        }


        public List<Student> ReadExams(int classId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetExam(classId);
        }

        public List<Exam> ReadStudentExams(int idClass, string studentEmail)
        {
            DBServices dbs = new DBServices();
            return dbs.GetStudentExam(idClass, studentEmail);
        }

        public void UpdateGrade(int id, Exam e)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateG(id, e);
        }

        public void DExam(int idclass, string studentEmail)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteExam(idclass, studentEmail);
        }

        public void updateExams(string email, List<Exam> examAfterEdit)
        {
            DBServices dbs = new DBServices();
            dbs.updateExams(email, examAfterEdit);
        }

        public List<Exam> getGradesForGraph(string email, string subjName)
        {
            DBServices dbs = new DBServices();
            return dbs.getGradesForGraph(email, subjName);
        }

    }
}