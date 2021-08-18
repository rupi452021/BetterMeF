using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class ProfessionalClass
    {
        int idClass;
        string levelgroup; 
        int groupnum;


        public int IdClass { get => idClass; set => idClass = value; }
        public string Levelgroup { get => levelgroup; set => levelgroup = value; }
        public int Groupnum { get => groupnum; set => groupnum = value; }


        public ProfessionalClass(int idClass, string levelgroup, int groupnum)
        {
            this.idClass = idClass;
            this.levelgroup = levelgroup;
            this.groupnum = groupnum;
        }

        public ProfessionalClass() { }



        public void InsertProClass(ProfessionalClass ProData)
        {
            DBServices dbs = new DBServices();
            dbs.InsertProClass(ProData);
        }
    }
}