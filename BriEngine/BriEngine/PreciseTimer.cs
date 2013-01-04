using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Engine
{
    public class PreciseTimer
    {
        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        private static extern bool QueryPerformanceFrequency(ref long 
            PerformanceFrequency);

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        private static extern bool QueryPerformanceCounter(ref long 
            PerformanceCount);

        long ticksPerSecond = 0;
        long previousElapsedTime = 0;

        public PreciseTimer()
        {
            QueryPerformanceFrequency(ref ticksPerSecond);
            GetElapsedTime(); // throw out the first rubbish result
        }
        public double GetElapsedTime()
        {
            long time = 0;
            QueryPerformanceCounter(ref time);
            double elapsedTime = (double)(time - previousElapsedTime) /
                (double)ticksPerSecond;
            previousElapsedTime = time;
            return elapsedTime;
        }
    }
}
