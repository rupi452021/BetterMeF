using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class Teacher
    {
        int id;
        string email;
        string pass;
        string firstName;
        string lastName;


        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string Pass { get => pass; set => pass = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }


        public Teacher(int id, string email, string pass, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            Pass = pass;
            FirstName = firstName;
            LastName = lastName;
        }

        public Teacher() { }

        public Teacher Read(string email, string pass)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadTeacher(email, pass);
        }
        
        public void updateTeacherPass(string email, string newPass)
        {
            DBServices dbs = new DBServices();
            dbs.updateTeacherPass(email, newPass);
        }
    }
}