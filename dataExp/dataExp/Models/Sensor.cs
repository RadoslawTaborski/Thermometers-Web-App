using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dataExp.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Measurement Measurment { get; set; }
    }
}