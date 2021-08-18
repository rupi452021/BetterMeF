using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class Remark
    {
        int id;
        int idClass;
        string remarkname;
        int takepoints;

        public int Id { get => id; set => id = value; }
        public int IdClass { get => idClass; set => idClass = value; }
        public string Remarkname { get => remarkname; set => remarkname = value; }
        public int Takepoints { get => takepoints; set => takepoints = value; }


        public Remark(int id, int idClass, string remarkname, int takepoints)
        {
            Id = id;
            IdClass = idClass;
            Remarkname = remarkname;
            Takepoints = takepoints;
        }


        public Remark() { }


        public int InsertRemark(Remark[] subPointsArr)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertRemark(subPointsArr);
        }

        public List<Remark> ReadRemark(int classId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetRemark(classId);
        }

        public void DeleteR(Remark R)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteRemarks(R);
        }
    }
}