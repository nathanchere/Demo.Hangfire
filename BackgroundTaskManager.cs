using System;
using Autofac;
using Demo.Hangfire.Db;
using Hangfire;
using Hangfire.MemoryStorage;

namespace Demo.Hangfire
{
    public class BackgroundTaskManager : IDisposable
    {
        private IContainer Container;
        private BackgroundJobServer server; // Hangfire server

        public BackgroundTaskManager(int workerCount)
        {
            var iocContainer = InitialiseAutofacContainer();

            GlobalConfiguration.Configuration.UseMemoryStorage();
            GlobalConfiguration.Configuration.UseAutofacActivator(iocContainer);
            //GlobalConfiguration.Configuration.UseColouredConsoleLogProvider();

            server = new BackgroundJobServer(new BackgroundJobServerOptions
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
            server?.Dispose();
        }
    }
}