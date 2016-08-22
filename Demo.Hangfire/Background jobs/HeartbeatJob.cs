using System;
using System.Diagnostics;
using System.Threading;

namespace Demo.Hangfire
{
    public static class HeartbeatJob
    {
        private static readonly ConsoleOutput ConsoleOutput = new ConsoleOutput();

        public static void Run()
        {
            while (true)
            {
                var ticks = Stopwatch.GetTimestamp();
                var uptime = ((double)ticks) / Stopwatch.Frequency;                

                ConsoleOutput.Write(ConsoleColor.Red, $"System status: [online], uptime: [{TimeSpan.FromSeconds(uptime)}]");

                Thread.Sleep(5000);
            }            
        }
    }
}