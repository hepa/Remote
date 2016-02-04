using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RemotePC
{
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
}