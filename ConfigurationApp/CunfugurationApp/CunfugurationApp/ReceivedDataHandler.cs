using System;
using System.Text;

namespace ConfigurationApp
{
    class ReceivedDataHandler
    {
        public int sensorAdress;
        public string temperature;
        private string name;
        public DataBaseHandler dataBaseHandler;

        public ReceivedDataHandler(DataBaseHandler dataBase)
        {
            dataBaseHandler = dataBase;
        }

        public void SaveReceivedDataToDataBase(string receivedData)
        {
            if (receivedData.Contains("Dodano nowy czujnik:")) 
            {
                SetSensorAdress(receivedData);
                dataBaseHandler.AddSensorToMainTable(name, sensorAdress);
                int? id = dataBaseHandler.ReturnIdOfSensorWithAdress(sensorAdress);
                dataBaseHandler.CreateSensorTable(id);
            }

            else if (receivedData.Contains("Adres:") && (receivedData.Contains("Temperatura:")))
            {               
                SetSensorAdressAndTemperature(receivedData);
                int? id = dataBaseHandler.ReturnIdOfSensorWithAdress(sensorAdress);
                dataBaseHandler.AddMeasurementToSensorTable(id, temperature);
                dataBaseHandler.UpdateMeasurementInMainTable(id, temperature);
            }
        }

        public void SetSensorAdress(string receivedData)
        {
            string[] splittedMsg = receivedData.Split(new char[] { ' ' });
            sensorAdress = Convert.ToInt32(splittedMsg[3]);
            Console.Write("Dodano czujnik o adresie: {0}  Podaj nazwe: ", sensorAdress);
            name = Console.ReadLine();
           

        }

        public void SetSensorAdressAndTemperature(string receivedData)
        {
            string[] splittedMsg = receivedData.Split(new char[] { ' ' });
            sensorAdress = Convert.ToInt32(splittedMsg[1]);
            temperature = splittedMsg[3];
            var sb = new StringBuilder(temperature);
            sb.Replace(".", ",");
            double temperatureDouble = Convert.ToDouble(sb.ToString());
            temperatureDouble = Math.Round(temperatureDouble, 1);
            sb = new StringBuilder(temperatureDouble.ToString());
            sb.Replace(",", ".");
            temperature = sb.ToString();
        }
    }
}
