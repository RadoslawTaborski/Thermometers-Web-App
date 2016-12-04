using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace WebApp.Controllers
{
    public class HistoryController : Controller
    {
        private static List<Models.SensorHistory> sensorsList;
        // GET: History
        public ActionResult Index(int? value, String date)
        {
            if (value != null)
            {
                ViewBag.List = new SelectList(sensorsList, "Id", "Name", value);
                var DaysString = new List<String>();
                foreach (var item in sensorsList[(value - 1).Value].Days)
                {
                    DaysString.Add(item.ToShortDateString());
                }
                ViewBag.Day = new SelectList(DaysString,date);
                if (date == "")
                {
                    var tmp=sensorsList[(value - 1).Value].Data = sensorsList[(value - 1).Value].Measurments;
                    var data = new object[tmp.Count(), 2];
                    for (var i = 0; i < tmp.Count(); ++i)
                    {
                        data[i, 0] = tmp.ElementAt(i).Date;
                        data[i, 1] = tmp.ElementAt(i).Temperature;
                    }

                    sensorsList[(value - 1).Value].Chart = new Highcharts("chart")
                    .InitChart(new Chart { DefaultSeriesType = ChartTypes.Spline })
                    .SetOptions(new GlobalOptions { Global = new Global { UseUTC = false } })
                    .SetTitle(new Title { Text = "Wykres" })
                    .SetSubtitle(new Subtitle { Text = tmp.First().Date.ToShortDateString() + "-" + tmp.Last().Date.ToShortDateString() })
                    .SetXAxis(new XAxis
                    {
                        Type = AxisTypes.Datetime,
                        DateTimeLabelFormats = new DateTimeLabel { Month = "%e. %b", Year = "%b" },
                        Title = new XAxisTitle { Text = "Godzina" },
                    })
                    .SetYAxis(new YAxis
                    {
                        Title = new YAxisTitle { Text = "Temperatura [*C]" },
                        Min = 15
                    })
                    .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.series.name +'</b><br/>'+ Highcharts.dateFormat('%e. %b', this.x) +': '+ this.y +' m'; }" })
                    .SetSeries(new Series { Name = sensorsList[(value - 1).Value].Name, Data = new Data(data) });
                }
                else
                {
                    DateTime date2;
                    if (DateTime.TryParseExact(date, "dd'.'MM'.'yyyy", null, System.Globalization.DateTimeStyles.None, out date2))
                    {
                        var tmp=sensorsList[(value - 1).Value].Data = sensorsList[(value - 1).Value].Measurments.Where(p=> sensorsList[(value - 1).Value].Measurments.Any(i => i.Date.Date == date2.Date));

                        var data = new object[tmp.Count(), 2];
                        for (var i = 0; i < tmp.Count(); ++i)
                        {
                            data[i, 0] = tmp.ElementAt(i).Date;
                            data[i, 1] = tmp.ElementAt(i).Temperature;
                        }

                        sensorsList[(value - 1).Value].Chart = new Highcharts("chart")
                        .InitChart(new Chart { DefaultSeriesType = ChartTypes.Spline })
                        .SetOptions(new GlobalOptions { Global = new Global { UseUTC = false } })
                        .SetTitle(new Title { Text = "Wykres" })
                        .SetSubtitle(new Subtitle { Text = date2.ToShortDateString() })
                        .SetXAxis(new XAxis
                        {
                            Type = AxisTypes.Datetime,
                            DateTimeLabelFormats = new DateTimeLabel { Month = "%e. %b", Year = "%b" },
                            Title = new XAxisTitle { Text = "Godzina" },
                        })
                        .SetYAxis(new YAxis
                        {
                            Title = new YAxisTitle { Text = "Temperatura [*C]" },
                            Min = 15
                        })
                        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.series.name +'</b><br/>'+ Highcharts.dateFormat('%e. %b', this.x) +': '+ this.y +' m'; }" })
                        .SetSeries(new Series { Name = sensorsList[(value - 1).Value].Name, Data = new Data(data) });
                    }
                }
                return View(sensorsList[(value - 1).Value]);
            }
            else
            {
                sensorsList = new List<Models.SensorHistory>();
                DataBase.Connection.Open();
                string sql = "SELECT COUNT(*) FROM main_sensors_table";
                int num = 0;
                MySqlCommand cmdSel = new MySqlCommand(sql, DataBase.Connection);
                num = Convert.ToInt32(cmdSel.ExecuteScalar());

                sql = "SELECT * FROM main_sensors_table";
                cmdSel = new MySqlCommand(sql, DataBase.Connection);
                DataTable dt = new DataTable();
                MySqlDataReader dataReader = cmdSel.ExecuteReader();
                int _id;
                string _name;

                while (dataReader.Read())
                {
                    _id = Convert.ToInt32(dataReader["id"].ToString());
                    _name = dataReader["Name"].ToString();
                    sensorsList.Add(new Models.SensorHistory() { Id = _id, Name = _name });
                }
                dataReader.Close();

                for (int i = 0; i < num; ++i)
                {
                    sql = "SELECT * FROM sensor_nr_" + (i + 1).ToString();
                    cmdSel = new MySqlCommand(sql, DataBase.Connection);
                    dt = new DataTable();
                    dataReader = cmdSel.ExecuteReader();


                    double _temperature;
                    DateTime _date;
                    DateTime _time;

                    while (dataReader.Read())
                    {
                        _temperature = Convert.ToDouble(dataReader["Temperature"].ToString());
                        _date = DateTime.Parse(dataReader["Date"].ToString());
                        _time = DateTime.Parse(dataReader["Time"].ToString());
                        DateTime _dateTime = new DateTime(_date.Year, _date.Month, _date.Day, _time.Hour, _time.Minute, _time.Second);

                        sensorsList[i].Measurments.Add(new Models.Measurement { Temperature = _temperature, Date = _dateTime });
                        sensorsList[i].Days.Add(_dateTime.Date);
                    }
                    dataReader.Close();
                    sensorsList[i].Days = sensorsList[i].Days.Distinct().ToList();
                }
                DataBase.Connection.Close();

                sensorsList.Add(new Models.SensorHistory());

                ViewBag.List = new SelectList(sensorsList, "Id", "Name");
                ViewBag.Day = new SelectList(sensorsList.Last().Measurments, null, "Date");

                return View(sensorsList.Last());
            }
        }
    }
}