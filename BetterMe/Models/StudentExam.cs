using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class StudentExam
    {
        string subj;
        string firstname;
        string lastname;
        int examnum;
        int grade;


        public string Subj { get => subj; set => subj = value; }
        public string Firstname { get => firstname; set => firstname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public int Examnum { get => examnum; set => examnum = value; }
        public int Grade { get => grade; set => grade = value; }


        public StudentExam(string subj, string firstname, string lastname, int examnum, int grade)
        {
            Subj = subj;
            Firstname = firstname;
            Lastname = lastname;
            Examnum = examnum;
            Grade = grade;
        }
        public StudentExam() { }

        public List<StudentExam> GetStudentGrades(string studentEmail)
        {
            DBServices dbs = new DBServices();
            return dbs.GetStudentGrades(studentEmail);
        }


    }
}