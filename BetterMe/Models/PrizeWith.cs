using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class PrizeWith
    {
        int id;
        int idClass;
        int priceinstore;
        string prizetype;
        string referencesTo;
        string improvmentBy;
        int valueimprovement;
        int bonus;
        //int purchaseId;    //1


        public int Id { get => id; set => id = value; }
        public int IdClass { get => idClass; set => idClass = value; }
        public int Priceinstore { get => priceinstore; set => priceinstore = value; }
        public string Prizetype { get => prizetype; set => prizetype = value; }
        public string ReferencesTo { get => referencesTo; set => referencesTo = value; }
        public string ImprovmentBy { get => improvmentBy; set => improvmentBy = value; }
        public int Valueimprovement { get => valueimprovement; set => valueimprovement = value; }
        public int Bonus { get => bonus; set => bonus = value; }




        //public int PurchaseId { get => purchaseId; set => purchaseId = value; }


        public PrizeWith(int id, int idClass, int priceinstore, string prizetype, string referencesTo, string improvmentBy, int valueimprovement, int bonus/*/int purchaseId/*/)
        {
            Id = id;
            IdClass = idClass;
            Priceinstore = priceinstore;
            Prizetype = prizetype;
            ReferencesTo = referencesTo;
            ImprovmentBy = improvmentBy;
            Valueimprovement = valueimprovement;
            Bonus = bonus;
            //PurchaseId = purchaseId;
        }

        public PrizeWith() { }


        public int InsertPrizesWith(PrizeWith[] prizewith)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertPrizesWith(prizewith);
        }

        public List<PrizeWith> ReadPrizeWith(int classId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetPrizeWith(classId);
        }

        public List<PrizeWith> getPrizeWithForStudent(int classId)
        {
            DBServices dbs = new DBServices();
            return dbs.getPrizeWithForStudent(classId);
        }

        public void DeletePW(PrizeWith PW)
        {
            DBServices dbs = new DBServices();
            dbs.Deletepw(PW);
        }
    }

}