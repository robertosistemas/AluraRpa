using System.Runtime.InteropServices;

namespace AluraRpa.Infra.Configurations
{
    public static class ManagerConsole
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void BringConsoleToFront()
        {
            SetForegroundWindow(GetConsoleWindow());
        }
    }
}
