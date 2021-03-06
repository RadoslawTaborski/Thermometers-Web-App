﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class SensorHistory
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<Measurement> Measurments { get; set; }

        public SensorHistory()
        {
            Measurments = new List<Measurement>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}