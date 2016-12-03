using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using exp2.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace exp2.Controllers
{
    public class HighChartsController : Controller
    {
        public ActionResult Index()
        {
            /*var chartData = new List<DataPoint>
            {
                new DataPoint() {Temp=5M, Data="AAA" },
                new DataPoint() {Temp=8, Data="BBB" },
                new DataPoint() {Temp=7, Data="CCC" },
                new DataPoint() {Temp=3, Data="DDD" },
            };

            var xData = chartData.Select(i => i.Data).ToArray();
            var yData = chartData.Select(i => new object[] { i.Temp }).ToArray();

            //instanciate an object of the Highcharts type
            var chart = new Highcharts("chart")
                        //define the type of chart 
                        .InitChart(new Chart { DefaultSeriesType = ChartTypes.Line })
                        //overall Title of the chart 
                        .SetTitle(new Title { Text = "Incoming Transacions per hour" })
                        //small label below the main Title
                        .SetSubtitle(new Subtitle { Text = "Accounting" })
                        //load the X values
                        .SetXAxis(new XAxis { Categories = xData })
                        //set the Y title
                        .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Number of Transactions" } })
                        .SetTooltip(new Tooltip
                        {
                            Enabled = true,
                            Formatter = @"function() { return '<b>'+ this.series.name +'</b><br/>'+ this.x +': '+ this.y; }"
                        })
                        .SetPlotOptions(new PlotOptions
                        {
                            Line = new PlotOptionsLine
                            {
                                DataLabels = new PlotOptionsLineDataLabels
                                {
                                    Enabled = true
                                },
                                EnableMouseTracking = false
                            }
                        })
                        //load the Y values 
                        .SetSeries(new[]
                    {
                        new Series {Name = "Hour", Data = new Data(yData)},
                            //you can add more y data to create a second line
                            // new Series { Name = "Other Name", Data = new Data(OtherData) }
                    });


            return View(chart);*/


            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
                .SetXAxis(new XAxis
                {
                    Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                })
                .SetSeries(new Series
                {
                    Data = new Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
                });

            return View(chart);

    }
    }
}