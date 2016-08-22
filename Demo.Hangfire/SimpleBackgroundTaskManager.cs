using System;
using Hangfire;
using Hangfire.MemoryStorage;

namespace Demo.Hangfire
{
    public class SimpleBackgroundTaskManager : IDisposable
    {        
        private readonly BackgroundJobServer _server;

        public SimpleBackgroundTaskManager(int workerCount)
        {            
            GlobalConfiguration.Configuration.UseMemoryStorage();

            //// Uncomment this if you want to see more of what HangFire is doing under the hood
            //GlobalConfiguration.Configuration.UseColouredConsoleLogProvider();

            _server = new BackgroundJobServer(new BackgroundJobServerOptions {
                WorkerCount = workerCount,
            });
        }

        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}