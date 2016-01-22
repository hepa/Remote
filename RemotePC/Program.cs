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
                    signal = serialPort.ReadLine();
                    if (!signal.StartsWith("INFRA:"))
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    signal = signal.Split(':').Last();
                    Console.WriteLine(signal);

                    if (!string.IsNullOrEmpty(signal))
                    {
                        SignalReceived(signal);
                    }

                    Thread.Sleep(10);
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



    static class ProfileManager
    {
        private static ProfileMapper mapper = new ProfileMapper();

        internal static Profile GetActiveProfile()
        {
            var activeProcess = ProcessManager.GetActiveProcess();
            return GetProfileForProcess(activeProcess);
        }

        private static Profile GetProfileForProcess(Process proc)
        {
            return mapper[proc.ProcessName];
        }

        private class ProfileMapper
        {
            private Dictionary<string, Profile> mapping = new Dictionary<string, Profile>();

            internal ProfileMapper()
            {
                mapping.Add("mpc-hc64", new MediaPlayerClassic());
            }

            internal Profile this[string processName]
            {
                get
                {
                    if (!mapping.ContainsKey(processName))
                    {
                        return null;
                    }
                    return mapping[processName];
                }
            }
        }
    }

    static class ProcessManager
    {
        private static IntPtr _activeWindow;
        private static Process _activeProcess;

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        internal static Process GetActiveProcess()
        {
            IntPtr hwnd = GetForegroundWindow();

            if (hwnd.Equals(_activeWindow))
            {
                return _activeProcess;
            }

            Console.WriteLine(hwnd);
            _activeWindow = hwnd;

            uint pid;

            GetWindowThreadProcessId(hwnd, out pid);
            _activeProcess = Process.GetProcessById((int)pid);

            return _activeProcess;
        }
    }

    interface Profile
    {
        void Execute(RemoteButton button);
    }

    public class DefaultProfile : Profile
    {
        public void Execute(RemoteButton btn)
        {
            switch (btn)
            {
                case RemoteButton.Close:
                    PressClose();
                    break;
                case RemoteButton.Play:
                    PressPlay();
                    break;
                case RemoteButton.VolumeDown:
                    PressVolumeDown();
                    break;
                case RemoteButton.VolumeUp:
                    PressVolumeUp();
                    break;
                case RemoteButton.OFF:
                    ShutDown();
                    break;
                case RemoteButton.Audio:
                    PressAudio();
                    break;
                case RemoteButton.Subtitle:
                    PressSubtitle();
                    break;
                case RemoteButton.Backward:
                    PressBackward();
                    break;
                case RemoteButton.Forward:
                    PressForward();
                    break;
                case RemoteButton.Three:
                    PressThree();
                    break;
                case RemoteButton.Seven:
                    PressSeven();
                    break;
                case RemoteButton.Zoom:
                    PressZoom();
                    break;
                default:
                    //PressPlay();
                    break;
            }
        }

        internal void PressKey(string key)
        {
            System.Windows.Forms.SendKeys.SendWait(key);
        }

        private void ShutDown() { }

        protected virtual void PressPlay() { }

        protected virtual void PressClose() { }

        protected virtual void PressVolumeDown() { }

        protected virtual void PressVolumeUp() { }

        protected virtual void PressAudio() { }

        protected virtual void PressSubtitle() { }

        protected virtual void PressBackward() { }

        protected virtual void PressForward() { }

        protected virtual void PressThree() { }

        protected virtual void PressSeven() { }

        protected virtual void PressZoom() { }
    }

    public class Media : DefaultProfile
    {
        protected override void PressPlay()
        {
            PressKey(" ");
        }

        protected override void PressClose()
        {
            PressKey("%{F4}");
        }

        protected override void PressVolumeUp()
        {
            PressKey("{UP}");
        }

        protected override void PressVolumeDown()
        {
            PressKey("{DOWN}");
        }

        protected override void PressZoom()
        {
            PressKey("%{ENTER}");
        }
    }

    class MediaPlayerClassic : Media
    {
        protected override void PressAudio()
        {
            PressKey("A");
        }

        protected override void PressSubtitle()
        {
            PressKey("S");
        }

        protected override void PressBackward()
        {
            PressKey("^{LEFT}");
        }

        protected override void PressForward()
        {
            PressKey("^{RIGHT}");
        }

        protected override void PressThree()
        {
            PressKey("{9}");
        }

        protected override void PressSeven()
        {
            PressKey("{1}");
        }
    }
}