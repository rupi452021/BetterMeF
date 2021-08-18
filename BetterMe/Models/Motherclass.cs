using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BetterMe.Models.DAL;


namespace BetterMe.Models
{
    public class Motherclass
    {
        int idClass;
        int classnum;


        public int IdClass { get => idClass; set => idClass = value; }
        public int Classnum { get => classnum; set => classnum = value; }


        public Motherclass(int idClass, int classnum)
        {
            this.idClass = idClass;
            this.classnum = classnum;
        }

        public Motherclass() { }


        public void InsertMotherClass(Motherclass MotherData)
        {
            DBServices dbs = new DBServices();
            dbs.InsertMotherClass(MotherData);
        }
    }
}