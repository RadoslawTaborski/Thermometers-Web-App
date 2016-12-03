using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebApp
{
    public static class DataBase
    {
        public static string ConStrng = "Server=localhost;Database=Temperature_measurements;Uid=root;Pwd=";
        public static MySqlConnection Connection = new MySqlConnection(ConStrng);

        public static bool WykonajPolecenie(MySqlCommand Command)
        {
            bool wynik = true;
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                wynik = false;
            }
            finally
            {
                Connection.Close();
            }
            return wynik;
        }
    }
}