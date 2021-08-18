using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class User
    {
        string email;
        string pass;


        public string Email { get => email; set => email = value; }
        public string Pass { get => pass; set => pass = value; }


        public User(string email, string pass)
        {
            Email = email;
            Pass = pass;
        }

        public User() { }

        public User Forgot(string email)
        {
            DBServices dbs = new DBServices();
            User u = dbs.Forgot(email);
            return u;

        }
    }
}