using System.IO.Ports;
using System;

// Dodano nowy czujnik: 44\r\n
// Adres: 44 Temperatura: 27.42152\r\n

namespace ConfigurationApp
{
    class SerialPortHandler
    {
        public string receivedData;
        private SerialPort serialPort;

        public SerialPortHandler(string portName)
        {
            this.serialPort = new SerialPort();
            this.serialPort.PortName = portName;
            this.serialPort.BaudRate = 2400;
            this.serialPort.Parity = Parity.None;
            this.serialPort.DataBits = 8;
            this.serialPort.StopBits = StopBits.One;
            try
            {
                this.serialPort.Open();
            }
            catch
            {
                Console.WriteLine("Nie mozna polaczyc z portem: {0}. Nacisnij dowolny przycisk aby wyjsc.",portName);
                Console.ReadKey();
                System.Environment.Exit(-1);
            }

        }

        public void ReadLineFromSerialPort()
        {
            receivedData = this.serialPort.ReadLine();
            Console.WriteLine("WIADOMOSC: {0}", receivedData);
        }
    }
}
