using System;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;

namespace WebApp.Controllers
{
    public class SituationController : Controller
    {
        private List<Models.Sensor> sensorsList;
        public ActionResult Index()
        {
            sensorsList = new List<Models.Sensor>();
            string sql = "SELECT * FROM main_sensors_table";
            DataBase.Connection.Open();
            MySqlCommand cmdSel = new MySqlCommand(sql, DataBase.Connection);
            int _id;
            string _name;
            double _temperature;
            DateTime _date;
            DateTime _time;

            DataTable dt = new DataTable();
            MySqlDataReader dataReader = cmdSel.ExecuteReader();
            while (dataReader.Read())
            {
                _id = Convert.ToInt32(dataReader["id"].ToString());
                _name = dataReader["Name"].ToString();
                _temperature = Convert.ToDouble(dataReader["CurrentTemperature"].ToString());
                _date = DateTime.Parse(dataReader["Date"].ToString());
                _time = DateTime.Parse(dataReader["Time"].ToString());
                DateTime _dateTime = new DateTime(_date.Year, _date.Month, _date.Day, _time.Hour, _time.Minute, _time.Second);
                sensorsList.Add(new Models.Sensor() { Id = _id, Name = _name, Measurment = new Models.Measurement { Temperature = _temperature, Date = _dateTime } });
            }

            DataBase.Connection.Close();

            return View(sensorsList);
        }
    }
}