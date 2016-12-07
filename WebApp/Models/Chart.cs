using DotNet.Highcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Chart
    {
        public Highcharts Highchart { get; set; }
        public IEnumerable<Measurement> Data { get; set; }

        public Chart()
        {
            Data = new List<Measurement>();
        }
    }
}