using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class Purchase
    {
        int id;
        string purcDate;
        int idStudent;


        public int Id { get => id; set => id = value; }
        public string PurcDate { get => purcDate; set => purcDate = value; }
        public int IdStudent { get => idStudent; set => idStudent = value; }


        public Purchase(int id, string purcDate, int idStudent)
        {
            this.id = id;
            this.purcDate = purcDate;
            this.idStudent = idStudent;
        }


        public Purchase() { }

    }
}