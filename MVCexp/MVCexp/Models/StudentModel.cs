using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCexp.Models
{
    public class StudentModel
    {
        public List<Detail> DetailModel { get; set; }
    }
    public class Detail
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public int Marks { get; set; }
        public string Address { get; set; }
    }
}