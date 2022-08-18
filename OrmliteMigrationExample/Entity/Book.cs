using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace OrmliteMigrationExample.Entity
{
    public class Book
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }

        public string Name { get; set; }//new field
        //public double ToDelete{get;set;} //Field removed from model
    }
}
