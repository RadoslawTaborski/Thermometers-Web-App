using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Highcharts;

namespace WebApp.Models
{
    public class SensorHistory
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<Measurement> Measurments { get; set; }
        public List<DateTime> Days { get; set; }


        public SensorHistory()
        {
            Measurments = new List<Measurement>();
            Days = new List<DateTime>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}