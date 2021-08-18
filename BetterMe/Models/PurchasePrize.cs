using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class PurchasePrize
    {
        int id;
        int idClass;
        string emailStudent;
        string prizeType;
        int purchaseIndex;
        string improvmentBy;


        public int Id { get => id; set => id = value; }
        public int IdClass { get => idClass; set => idClass = value; }
        public string EmailStudent { get => emailStudent; set => emailStudent = value; }
        public string PrizeType { get => prizeType; set => prizeType = value; }
        public int PurchaseIndex { get => purchaseIndex; set => purchaseIndex = value; }
        public string ImprovmentBy { get => improvmentBy; set => improvmentBy = value; }

        public PurchasePrize(int id, int idClass, string emailStudent, string prizeType, int purchaseIndex, string improvmentBy)
        {
            Id = id;
            IdClass = idClass;
            EmailStudent = emailStudent;
            PrizeType = prizeType;
            PurchaseIndex = purchaseIndex;
            ImprovmentBy = improvmentBy;
        }


        public PurchasePrize() { }

        public void addPurchPrize(PurchasePrize purchPrize)
        {
            DBServices dbs = new DBServices();
            dbs.addPurchPrize(purchPrize);
        }

        public List<PurchasePrize> GetStudentPurchases(string studentEmail)
        {
            DBServices dbs = new DBServices();
            return dbs.GetStudentPurchases(studentEmail);
        }


    }
}