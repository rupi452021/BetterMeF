using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class PrizeWithout
    {
        int id;
        int idClass;
        string prizetype;
        int priceinstore;
        int bonus;


        public int Id { get => id; set => id = value; }
        public int IdClass { get => idClass; set => idClass = value; }
        public string Prizetype { get => prizetype; set => prizetype = value; }
        public int Priceinstore { get => priceinstore; set => priceinstore = value; }
        public int Bonus { get => bonus; set => bonus = value; }



        public PrizeWithout(int id, int idClass, string prizetype, int priceinstore, int bonus)
        {
            Id = id;
            IdClass = idClass;
            Prizetype = prizetype;
            Priceinstore = priceinstore;
            Bonus = bonus;
        }

        public PrizeWithout() { }

        public int InsertPrizesWithout(PrizeWithout[] prizewithout)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertPrizesWithout(prizewithout);
        }

        public List<PrizeWithout> ReadPrizeWithout(int classId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetPrizeWithout(classId);
        }

        public List<PrizeWithout> getPrizeWithoutForStudent(int classId)
        {
            DBServices dbs = new DBServices();
            return dbs.getPrizeWithoutForStudent(classId);
        }

        public void DeletePWO(PrizeWithout PWO)
        {
            DBServices dbs = new DBServices();
            dbs.DeletePwo(PWO);
        }
    }
}