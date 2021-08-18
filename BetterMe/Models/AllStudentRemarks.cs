using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class AllStudentRemarks
    {
        string firstName;
        string lastName;
        string subjname;
        string remarkName;
        int takenpoints;


        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Subjname { get => subjname; set => subjname = value; }
        public string RemarkName { get => remarkName; set => remarkName = value; }
        public int Takenpoints { get => takenpoints; set => takenpoints = value; }


        public AllStudentRemarks(string firstName, string lastName, string subjname, string remarkName, int takenpoints)
        {
            FirstName = firstName;
            LastName = lastName;
            Subjname = subjname;
            RemarkName = remarkName;
            Takenpoints = takenpoints;
        }

        public AllStudentRemarks() { }


        public List<AllStudentRemarks> GetAllStudentRemarks(string studentEmail)
        {
            DBServices dbs = new DBServices();
            return dbs.GetAllStudentRemarks(studentEmail);
        }
    }
}