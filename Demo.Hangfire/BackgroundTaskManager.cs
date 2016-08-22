using System;
using Autofac;
using Demo.Hangfire.Db;
using Hangfire;
using Hangfire.MemoryStorage;

namespace Demo.Hangfire
{
    public class BackgroundTaskManager : IDisposable
    {        
        private readonly BackgroundJobServer _server; // HangFire job runner / thread manager / master of ceremonies

        public BackgroundTaskManager(int workerCount)
        {
            var iocContainer = InitialiseAutofacContainer();

            GlobalConfiguration.Configuration.UseMemoryStorage();            
            GlobalConfiguration.Configuration.UseAutofacActivator(iocContainer);            
            //// Uncomment this if you want to see more of what HangFire is doing under the hood
            //GlobalConfiguration.Configuration.UseColouredConsoleLogProvider();

            _server = new BackgroundJobServer(new BackgroundJobServerOptions
            {
                WorkerCount = workerCount,
            });
        }

        private ILifetimeScope InitialiseAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<IocJob>().As<IIocJob>();
            builder.RegisterType<RegressionMonitor>().As<IRegressionMonitor>();            
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<PensionContext>();

            return builder.Build();
        }

        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}