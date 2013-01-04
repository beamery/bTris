using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Engine
{
        [StructLayout(LayoutKind.Sequential)]
    public struct Message
    {
        public IntPtr hWnd;
        public Int32 msg;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public System.Drawing.Point p;
    }
    public class FastLoop
    {
        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool PeekMessage(
            out Message msg,
            IntPtr hWnd,
            uint MessageFilterMin,
            uint MessageFilterMax,
            uint flags);

        public delegate void LoopCallback(double elapsedTime);

        PreciseTimer timer = new PreciseTimer();
        LoopCallback callback;
        public FastLoop(LoopCallback callback)
        {
            this.callback = callback;
            Application.Idle += new EventHandler(OnApplicationEnterIdle);
        }

        void OnApplicationEnterIdle(object sender, EventArgs e)
        {
            while (AppIsStillIdle())
            {
                callback(timer.GetElapsedTime());
            }
        }

        private bool AppIsStillIdle()
        {
            Message msg;
            return !PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
        }
    }
}
