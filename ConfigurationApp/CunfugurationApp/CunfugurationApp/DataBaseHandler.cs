using System;
using MySql.Data.MySqlClient;

namespace ConfigurationApp
{
    class DataBaseHandler
    {
        private MySqlConnection dataBaseConnector;
        private string connectionString;
        private string dataBaseName;
        private string userID;
        private string password;
        private string serverName;
        private string mainTableName;

        public DataBaseHandler(string server, string name, string user, string pass, string mainTable)
        {
            this.serverName = server;
            this.dataBaseName = name;
            this.userID = user;
            this.password = pass;
            this.mainTableName = mainTable;
        }

        public void ConnectToServer()
        {
            this.connectionString = String.Format(
                @"Server={0};
                User ID={1};
                Password={2};
                Pooling=false;",
                this.serverName,
                this.userID,
                this.password);
            this.dataBaseConnector = new MySqlConnection(connectionString);

            try
            {
                this.dataBaseConnector.Open();
                Console.WriteLine("Polaczono z serwerem: {0}.", serverName);
            }
            catch
            {
                Console.WriteLine("Nie mozna polaczyc z serwerem. Nacisnij dowolny przycisk aby wyjsc.");
                Console.ReadKey();
                System.Environment.Exit(-1);
            }
        }

        public bool CheckDataBaseExists()
        {
            string commandText = String.Format(
                @"SHOW DATABASES LIKE '{0}';",
                dataBaseName);
            try
            {
                using (var command = new MySqlCommand(commandText, this.dataBaseConnector))
                {
                    if (command.ExecuteScalar() == null) return false;
                    else return true;
                }
            }
            catch { return false; }
        }

        public bool CheckMainSensorsTableExist()
        {
            string commandText = String.Format(
                @"SHOW TABLES LIKE '{0}';",
                mainTableName);

            try
            {
                using (var command = new MySqlCommand(commandText, this.dataBaseConnector))
                {
                    if (command.ExecuteScalar() == null) return false;
                    else return true;
                }
            }
            catch { return false; }
        }

        public void ConnectToDataBase()
        {
            this.connectionString = String.Format(
                @"Server={0};
                Database={1};
                User ID={2};
                Password={3};
                Pooling=false;",
                this.serverName,
                this.dataBaseName,
                this.userID,
                this.password);

            this.dataBaseConnector = new MySqlConnection(connectionString);
            try
            {
                dataBaseConnector.Open();
                Console.WriteLine("Pomyslnie polaczono z baza danych.");
            }
            catch
            {
                Console.WriteLine("Nie mozna polaczyc z baza danych. Nacisnij dowolny przycisk aby wyjsc.");
                Console.ReadKey();
                System.Environment.Exit(-1);
            }
        }

        public void CreateDataBase()
        {
            string commandText = String.Format(
               @"CREATE DATABASE `{0}`;",
               dataBaseName);
            try
            {
                using (var command = new MySqlCommand(commandText, this.dataBaseConnector))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Utworzono baze danych: {0}.", dataBaseName);
                }
            }
            catch
            {
                Console.WriteLine("Nie mozna utworzyc bazy danych. Nacisnij dowolny przycisk aby wyjsc.");
                Console.ReadKey();
                System.Environment.Exit(-1);
            }
        }

        public void CreateMainTable()
        {
            string commandText = String.Format(
               @"CREATE TABLE `{0}`.`{1}` 
                ( `ID` INT NOT NULL AUTO_INCREMENT PRIMARY KEY , 
                `Name` VARCHAR(25) NOT NULL ,
                `Adress` INT NOT NULL ,
                `CurrentTemperature` FLOAT NULL ,
                `Date` DATE NULL , 
                `Time` TIME NULL ) 
                ENGINE = InnoDB;",
               dataBaseName,
               mainTableName);

            try
            {
                using (var command = new MySqlCommand(commandText, this.dataBaseConnector))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Utworzono glowna tabele czujnikow.");
                }
            }
            catch
            {
                Console.WriteLine("Nie mozna utworzyc glownej tabeli czujnikow. Nacisnij dowolny przycisk aby wyjsc.");
                Console.ReadKey();
                System.Environment.Exit(-1);
            }
        }

        public void AddSensorToMainTable(string name, int adress)
        {
            string commandText = String.Format(
                @"INSERT INTO `{0}` 
                (`Name`, `Adress`, `CurrentTemperature`, `Date`, `Time`) 
                VALUES('{1}', '{2}', NULL, NULL, NULL)",
                mainTableName,
                name,
                adress);

            try
            {
                using (var command = new MySqlCommand(commandText, this.dataBaseConnector))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Dodano czujnik {0} o adresie {1} do glownej tabeli", name, adress);
                }
            }
            catch
            {
                Console.WriteLine("Nie mozna dodac czujnika {0} do glownej tabeli. Nacisnij dowolny przycisk aby wyjsc.");
                Console.ReadKey();
                System.Environment.Exit(-1);
            }
        }

        public int? ReturnIdOfSensorWithAdress(int adress)
        {
            string commandText = String.Format(
                @"SELECT `ID` FROM `{0}` 
                WHERE `Adress`= {1}",
                mainTableName,
                adress);
            int? id = null;

            try
            {
                using (var command = new MySqlCommand(commandText, this.dataBaseConnector))
                {
                    return id = (int?)command.ExecuteScalar();
                }
            }
            catch
            {
                Console.WriteLine("Nie ma czujnika o ID: {0} i adresie {1}. Nacisnij dowolny przycisk aby wyjsc.", id, adress);
                Console.ReadKey();
                System.Environment.Exit(-1);
                return id;
            }
        }

        public void CreateSensorTable(int? id)
        {
            string tableName = String.Format("Sensor_Nr_{0}", id);

            string commandText = String.Format(
                @"CREATE TABLE `{0}`.`{1}` 
                ( `ID` INT NOT NULL AUTO_INCREMENT PRIMARY KEY ,
                `Date` DATE NOT NULL, 
                `Time` TIME NOT NULL, 
                `Temperature` FLOAT NOT NULL ) 
                ENGINE = InnoDB;",
                dataBaseName,
                tableName);

            try
            {
                using (var command = new MySqlCommand(commandText, this.dataBaseConnector))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Utworzono tabele Sensor_Nr_{0}", id);
                }

            }
            catch
            {
                Console.WriteLine("Nie mozna utworzyc tabeli Sensor_Nr_{0}. Nacisnij dowolny przycisk aby wyjsc.", id);
                Console.ReadKey();
                System.Environment.Exit(-1);
            }
        }

        public void AddMeasurementToSensorTable(int? id, string temperature)
        {
            string tableName = String.Format("Sensor_Nr_{0}", id);

            string commandText = String.Format(
                @"INSERT INTO `{0}` 
                (`Date`, `Time`, `Temperature`) 
                VALUES (CURRENT_DATE(), CURRENT_TIME(), '{1}')",
                tableName,
                temperature);

            try
            {
                using (var command = new MySqlCommand(commandText, this.dataBaseConnector))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Dodano pomiar Temperatura {0} do tabeli Sensor_Nr_{1}", temperature, id);
                }

            }
            catch
            {
                Console.WriteLine("Nie mozna dodac pomiaru.");
            }
        }

        public void UpdateMeasurementInMainTable(int? id, string temperature)
        {
            string commandText = String.Format(
                @"UPDATE `{0}` SET `CurrentTemperature` = '{1}', 
                `Date` = CURRENT_DATE(),
                `Time` = CURRENT_TIME() 
                WHERE `{0}`.`ID` = {2}",
                mainTableName,
                temperature,
                id);

            try
            {
                using (var command = new MySqlCommand(commandText, this.dataBaseConnector))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Zaktualizowano temperature: {0} dla czujnika o ID: {1}", temperature, id);
                }
                    
            }
            catch
            {
                Console.WriteLine("Nie mozna zaktualizowac aktualnej temperatury.");
            }
        }

    }
}



