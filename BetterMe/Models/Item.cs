using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class Item
    {
        int id;
        string nameItem;
        string img;
        int points;
        //להוסיף טופ ורייט מסוג פלווט

        public int Id { get => id; set => id = value; }
        public string NameItem { get => nameItem; set => nameItem = value; }
        public string Img { get => img; set => img = value; }
        public int Points { get => points; set => points = value; }


        public Item(int id, string nameItem, string img, int points)
        {
            Id = id;
            NameItem = nameItem;
            Img = img;
            Points = points;
        }


        public Item() { }


        public int addItem(Item item)
        {
            DBServices dbs = new DBServices();
            return dbs.addItem(item);
        }

        public int BuyItem(int itemId, string studentEmail)
        {
            DBServices dbs = new DBServices();
            return dbs.BuyItem(itemId, studentEmail);
        }


    }
}