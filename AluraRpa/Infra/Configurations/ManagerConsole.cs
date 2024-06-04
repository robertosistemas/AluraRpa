using System.Runtime.InteropServices;

namespace AluraRpa.Infra.Configurations
{
    public static class ManagerConsole
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindowByCaptionExtern(IntPtr zeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        public static IntPtr FindWindowByCaption(string lpWindowName)
        {
            return FindWindowByCaptionExtern(IntPtr.Zero, lpWindowName);
        }

        public static void SwitchWindow(IntPtr windowHandle)
        {
            if (GetForegroundWindow() == windowHandle)
                return;

            IntPtr foregroundWindowHandle = GetForegroundWindow();
            uint currentThreadId = GetCurrentThreadId();
            uint foregroundThreadId = GetWindowThreadProcessId(foregroundWindowHandle, out uint _);
            AttachThreadInput(currentThreadId, foregroundThreadId, true);
            SetForegroundWindow(windowHandle);
            AttachThreadInput(currentThreadId, foregroundThreadId, false);

            while (GetForegroundWindow() != windowHandle)
            {
                Thread.Sleep(10);
            }

        }

        public static void BringConsoleToFront()
        {
            var windowHandle = GetConsoleWindow();
            SwitchWindow(windowHandle);
        }

    }
}
