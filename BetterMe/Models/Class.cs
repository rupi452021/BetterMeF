using BetterMe.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterMe.Models
{
    public class Class
    {
        int id;   //2
        string layer; 
        int subjId;
        int idTeacher;
        //int classnum;
        //string levelgroup;
        //int groupnum;

        public int Id { get => id; set => id = value; }
        public string Layer { get => layer; set => layer = value; }
        public int SubjId { get => subjId; set => subjId = value; }
        public int IdTeacher { get => idTeacher; set => idTeacher = value; }

        public Class(int id, string layer, int subjId, int idTeacher)
        {
            Id = id;
            Layer = layer;
            SubjId = subjId;
            IdTeacher = idTeacher;
        }
        
        public Class() { }

        public int InsertClass(Class ClassData)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertClass(ClassData);
        }

        public void DeleteClass(int classId)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteClass(classId);
        }
    }
}