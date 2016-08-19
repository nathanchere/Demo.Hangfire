using System;
using Hangfire;

namespace Demo.Hangfire
{
    class Program
    {
        private static BackgroundJobClient jobClient;

        static void Main(string[] args)
        {            
            var backgroundTaskManager = new BackgroundTaskManager(3);

            jobClient = new BackgroundJobClient();

            try
            {
                Console.WriteLine("== Hangfire demo ==");
                Console.WriteLine("Beginning job scheduling...");

                RunSimpleJobs();
                //RunOngoingJob();
                //RunInstancedJobs();

                Console.WriteLine("Job scheduling complete... tada!");
                Console.WriteLine("== Press [Enter] to exit ==");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("== Cleaning up ==");
            }
        }

        private static void RunSimpleJobs()
        {
            for (int i = 0; i < 10; i++)
            {
                var counter = i;
                jobClient.Enqueue(() => SimpleJob.Run(counter));
            }
        }

        private static void RunOngoingJob()
        {
            jobClient.Enqueue(() => HeartbeatJob.Run());
            //jobClient.Enqueue(() => HeartbeatJobInception.Run());
        }

        private static void RunInstancedJobs()
        {
            jobClient.Enqueue<IIocJob>(j => j.Run(ConsoleColor.Cyan));            
            jobClient.Enqueue<IRegressionMonitor>(j => j.Run(new RegressionMonitorSettings(6, ConsoleColor.DarkMagenta)));
            jobClient.Enqueue<IIocJob>(j => j.Run(ConsoleColor.Green));
            jobClient.Enqueue<IRegressionMonitor>(j => j.Run(new RegressionMonitorSettings(4, ConsoleColor.DarkRed)));
        }
    }
}
