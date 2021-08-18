using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models.DAL
{
    public class RandomAddPoints
    {
        int idClass;
        string comment;
        int points;

        public RandomAddPoints(int idClass, string comment, int points)
        {
            this.IdClass = idClass;
            this.Comment = comment;
            this.Points = points;
        }

        public int IdClass { get => idClass; set => idClass = value; }
        public string Comment { get => comment; set => comment = value; }
        public int Points { get => points; set => points = value; }


        public RandomAddPoints() { }

        public int InsertRandomPoints(RandomAddPoints[] RandomAddPointsArr)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertRandomAddPoints(RandomAddPointsArr);
        }

        public List<RandomAddPoints> ReadRandomAddPoints(int classId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetRandomAddPoints(classId);
        }

        public void DeleteRAP(RandomAddPoints RAP)
        {
            DBServices dbs = new DBServices();
            dbs.Deleterap(RAP);
        }

    }
}