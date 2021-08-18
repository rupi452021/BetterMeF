using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class ClassOfTeacher
    {
        string firstName; // שם המורה 
        string lastName; //שם משפחה
        int idClass;    // איי די כיתה
        string layer;    // שכבה
        string subjname; // מקצוע
        int classnum;   // מספר כיתה
        string levelgroup; // הקבצה
        int groupnum;  // מספר קבוצה


        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int IdClass { get => idClass; set => idClass = value; }
        public string Layer { get => layer; set => layer = value; }
        public string Subjname { get => subjname; set => subjname = value; }
        public int Classnum { get => classnum; set => classnum = value; }
        public string Levelgroup { get => levelgroup; set => levelgroup = value; }
        public int Groupnum { get => groupnum; set => groupnum = value; }

        public ClassOfTeacher(string firstName, string lastName, int idClass, string layer, string subjname, int classnum, string levelgroup, int groupnum)
        {
            FirstName = firstName;
            LastName = lastName;
            IdClass = idClass;
            Layer = layer;
            Subjname = subjname;
            Classnum = classnum;
            Levelgroup = levelgroup;
            Groupnum = groupnum;
        }

        public ClassOfTeacher() { }


        public List<ClassOfTeacher> ReadClassses(int teachId)
        {
            DBServices dbs = new DBServices();
            return dbs.FindClasses(teachId);
        }


        public ClassOfTeacher CheckMotherClasss(string Layer, string Subjname, int Classnum)
        {
            DBServices dbs = new DBServices();
            return dbs.CheckMotherClasss(Layer, Subjname, Classnum);
        }   

        public ClassOfTeacher CheckProClasss(string Layer, string Subjname, string Levelgroup, int Groupnum)
        {
            DBServices dbs = new DBServices();
            return dbs.CheckProClasss(Layer, Subjname, Levelgroup, Groupnum);
        }

        public List<ClassOfTeacher> ReadClasssesStudent(string email)// הבא כיתות תלמיד
        {
            DBServices dbs = new DBServices();
            return dbs.FindClassesStudent(email);
        }


        public List<ClassOfTeacher> GetParentStudents(string email)// הבא תלמידים של הורים
        {
            DBServices dbs = new DBServices();
            return dbs.GetParentStudents(email);
        }

        public ClassOfTeacher ReadClassDetails(int idClass)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadClassDetails(idClass);
        }
    }
}