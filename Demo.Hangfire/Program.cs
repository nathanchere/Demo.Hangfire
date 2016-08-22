using System;
using Hangfire;

namespace Demo.Hangfire
{
    class Program
    {
        private static BackgroundJobClient jobClient;
        private const int NumberOfBackgroundWorkers = 3;

        static void Main(string[] args)
        {
            // There is also InjectedBackgroundTaskManager to show how simple it is without the IOC setup
            var backgroundTaskManager = new BackgroundTaskManager(NumberOfBackgroundWorkers);

            jobClient = new BackgroundJobClient();

            try
            {
                Console.WriteLine("== Hangfire demo ==");
                Console.WriteLine("Beginning job scheduling...");

                // Uncomment whichever one(s) you want to run
                // Remember to increase NumberOfBackgroundWorkers if you have 3 or more long running jobs
                //////
                //RunSimpleJobs();
                //RunOngoingJob();
                //RunInstancedJobs();
                //////

                jobClient.

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
                backgroundTaskManager.Dispose();
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
            // Simple long-running background job
            jobClient.Enqueue(() => HeartbeatJob.Run());


            // Slightly less simple background job
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
