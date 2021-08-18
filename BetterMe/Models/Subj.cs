using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class Subj
    {
        int id;
        string subjName;


        public int Id { get => id; set => id = value; }
        public string SubjName { get => subjName; set => subjName = value; }


        public Subj(int id, string subjName)
        {
            this.id = id;
            this.subjName = subjName;
        }


        public Subj() { }


        public int ReadSubj(string SubjName)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadSubj(SubjName);
        }
    }

}