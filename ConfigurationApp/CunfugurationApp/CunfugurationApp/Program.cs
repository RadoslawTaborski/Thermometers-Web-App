using System;

namespace ConfigurationApp
{
    class Program
    {
        public static void Main()
        {
            DataBaseHandler dataBaseHandler = new 
                DataBaseHandler("localhost","Temperature_measurements", "root", "","Main_Sensors_Table");

            dataBaseHandler.ConnectToServer();
            if (!dataBaseHandler.CheckDataBaseExists())
            {
                dataBaseHandler.CreateDataBase();
            }
            dataBaseHandler.ConnectToDataBase();
            if (!dataBaseHandler.CheckMainSensorsTableExist())
            {
                dataBaseHandler.CreateMainTable();
            }

            SerialPortHandler serialPortHandler = new SerialPortHandler("COM4");
            ReceivedDataHandler receivedDataHandler = new ReceivedDataHandler(dataBaseHandler);

            Console.WriteLine();
            Console.WriteLine("Gotowy na przyjmowanie komunikatow.");
            Console.WriteLine();

            while (true)
            {
                serialPortHandler.ReadLineFromSerialPort();
                receivedDataHandler.SaveReceivedDataToDataBase(serialPortHandler.receivedData);                
            }
        }
    }
}