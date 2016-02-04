using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemotePC
{
    class Program
    {
        private const int SLEEP_TIME = 90;

        static void Main(string[] args)
        {
            SerialPort serialPort;

            var signal = ""; //ARDUINO will changes this to hex of the button.

            try
            {
                serialPort = new SerialPort("COM4", 9600);
                serialPort.Open();

                while (true)
                {
                    try
                    {
                        signal = serialPort.ReadLine();
                        if (!signal.StartsWith("INFRA:"))
                        {
                            Thread.Sleep(SLEEP_TIME);
                            continue;
                        }
                        signal = signal.Split(':').Last();
                        Console.WriteLine(signal.Trim());

                        if (!string.IsNullOrEmpty(signal))
                        {
                            SignalReceived(signal);
                        }

                        Thread.Sleep(SLEEP_TIME);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        private static void SignalReceived(string signal)
        {
            var activeProfile = ProfileManager.GetActiveProfile();
            if (activeProfile == null)
            {
                return;
            }

            var buttonPressed = SignalManager.GetButtonFromSignal(signal);
            activeProfile.Execute(buttonPressed);
        }
    }                    
}