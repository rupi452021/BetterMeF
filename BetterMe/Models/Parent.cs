using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class Parent
    {
        string email;
        string pass;
        string fullname;
        string phone;


        public string Email { get => email; set => email = value; }
        public string Pass { get => pass; set => pass = value; }
        public string Fullname { get => fullname; set => fullname = value; }
        public string Phone { get => phone; set => phone = value; }


        public Parent(string email, string pass, string fullname, string phone)
        {
            this.email = email;
            this.pass = pass;
            this.fullname = fullname;
            this.phone = phone;
        }

        public Parent(){}

        public Parent Read(string email, string pass)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadParent(email, pass);
        }


        public void InsertParents(List<Parent> Parents)
        {
            DBServices dbs = new DBServices();
            dbs.InsertParents(Parents);
        }

        public List<Parent> ReadParentDetails(int idClass)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadParentDetails(idClass);
        }

        public void updatePar(int idClass, List<Parent> AfterEditStudArr)
        {
            DBServices dbs = new DBServices();
            dbs.updatePar(idClass, AfterEditStudArr);
        }

        public void updateParentPass(string email, string newPass)
        {
            DBServices dbs = new DBServices();
            dbs.updateParentPass(email, newPass);
        }

    }
}