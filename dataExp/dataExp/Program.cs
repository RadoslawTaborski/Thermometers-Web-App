using System;
using System.Collections.Generic;
using System.Globalization;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;

namespace dataExp
{
    class Program
    {
        public static BindingList<Models.SensorHistory> sensorsList;
        static void Main(string[] args)
        {
            sensorsList = new BindingList<Models.SensorHistory>();

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
                }
                dataReader.Close();
            }
            DataBase.Connection.Close();

            foreach (var item in sensorsList)
            {
                Console.WriteLine(item.Name);
                foreach (var mesure in item.Measurments)
                    Console.WriteLine(mesure.Date);
            }

            /*
            sensorsList = new List<Sensor>();
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
                sensorsList.Add(new Sensor() { Id = _id, Name = _name, Measurment = new Measurement { Temperature = _temperature, Date = _dateTime } });
            }

            DataBase.Connection.Close();

            foreach(var item in sensorsList)
            {
                Console.WriteLine(item.Measurment.Date.ToString());
            }*/
            Console.ReadKey();
        }
    }
}
