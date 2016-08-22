using System;
using System.Diagnostics;
using System.Threading;
using Hangfire;

namespace Demo.Hangfire
{
    public static class HeartbeatJobInception
    {        
        private static BackgroundJobClient jobClient;
        private static TimeSpan Uptime;

        public static void Run()
        {
            UpdateUptime();

            while (true)
            {
                jobClient.Enqueue(() =>
                    EmailAgent.SendMail(
                        $"System status: [online]; Uptime: {Uptime}",
                        "alerts@sop.se")
                    );

                Thread.Sleep(1000);
            }
        }

        private static void UpdateUptime()
        {
            var ticks = Stopwatch.GetTimestamp();
            var uptime = ((double)ticks) / Stopwatch.Frequency;
            Uptime = TimeSpan.FromSeconds(uptime);

            jobClient.Schedule(() => UpdateUptime(), TimeSpan.FromSeconds(3));
        }
    }

    public class EmailAgent
    {
        public static void SendMail(string body, string to)
        {
            Console.WriteLine($"To: {{{to}}}\n{body}");
        }
    }
}