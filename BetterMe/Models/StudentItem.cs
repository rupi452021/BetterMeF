using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class StudentItem
    {
        int id;
        int idItem;
        string emailStudent;
        string topCss;
        string leftCss;
        string img;


        public int Id { get => id; set => id = value; }
        public int IdItem { get => idItem; set => idItem = value; }
        public string EmailStudent { get => emailStudent; set => emailStudent = value; }
        public string TopCss { get => topCss; set => topCss = value; }
        public string LeftCss { get => leftCss; set => leftCss = value; }
        public string Img { get => img; set => img = value; }



        public StudentItem(int id, int idItem, string emailStudent, string topCss, string leftCss, string img)
        {
            Id = id;
            IdItem = idItem;
            EmailStudent = emailStudent;
            TopCss = topCss;
            LeftCss = leftCss;
            Img = img;
        }

        public StudentItem() { }

        public void placeItems(List<StudentItem> studentItems)
        {
            DBServices dbs = new DBServices();
            dbs.placeItems(studentItems);
        }



        public List<StudentItem> GetStudentItems(string studentEmail)
        {
            DBServices dbs = new DBServices();
            return dbs.GetStudentItems(studentEmail);
        }

        public void DeleteItem(StudentItem StuItem)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteItem(StuItem);
        }
    }
}