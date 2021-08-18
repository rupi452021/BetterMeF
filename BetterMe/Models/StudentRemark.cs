using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class StudentRemark
    {
        string studentemail;
        int idClass;
        string remarkname;
        int points;

        public string Studentemail { get => studentemail; set => studentemail = value; }
        public int IdClass { get => idClass; set => idClass = value; }
        public string Remarkname { get => remarkname; set => remarkname = value; }
        public int Points { get => points; set => points = value; }

        public StudentRemark(string studentemail, int idClass, string remarkname, int points)
        {
            Studentemail = studentemail;
            IdClass = idClass;
            Remarkname = remarkname;
            Points = points;
        }

        public StudentRemark()
        {

        }

        public void SetRemark(StudentRemark[] SetRemarkArr)
        {
            DBServices dbs = new DBServices();
            dbs.SetRemark(SetRemarkArr);
        }

        public List<StudentRemark> getRemarkForClass(int idClass)
        {
            DBServices dbs = new DBServices();
            return dbs.getRemarkForClass(idClass);
        }

        public void delRemark(int idclass, string studentEmail)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteRemrk(idclass, studentEmail);
        }
    }
}