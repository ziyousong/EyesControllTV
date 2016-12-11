using System;
using System.IO.Ports

namespace EyesControllTV
{
    public class ArduinoConnection
    {
        public static SerialPort port;
        //public string PortName { get; set; }
        //public int BaudRate { get; set; }
        public DateTime eyeTime = DateTime.Now;

        private bool inblock = false;

        public ArduinoConnection(string PortName, int BaudRate)
        {
            try
            {
                port = new SerialPort();
                port.BaudRate = BaudRate;
                port.PortName = PortName;
                port.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error");
            }
        }

        public void PowerON(int seconds)
        {
            if (inblock)
            {
                if((DateTime.Now - eyeTime).TotalMilliseconds > seconds)
                {
                    inblock = false;
                    eyeTime = DateTime.Now;
                    port.WriteLine("1");
                }
            }
            else
            {
                inblock = true;
                eyeTime = DateTime.Now;
            }
        }

        public void NextChannel(int seconds)
        {
            if (inblock)
            {
                if ((DateTime.Now - eyeTime).TotalMilliseconds > seconds)
                {
                    inblock = false;
                    eyeTime = DateTime.Now;
                    port.WriteLine("2");
                }
            }
            else
            {
                inblock = true;
                eyeTime = DateTime.Now;
            }
        }

        public void PrevChannel(int seconds)
        {
            if (inblock)
            {
                if ((DateTime.Now - eyeTime).TotalMilliseconds > seconds)
                {
                    inblock = false;
                    eyeTime = DateTime.Now;
                    port.WriteLine("3");
                }
            }
            else
            {
                inblock = true;
                eyeTime = DateTime.Now;
            }
        }

    }
}
