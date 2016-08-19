using System;
using System.Threading;

namespace Demo.Hangfire
{
    public interface IIocJob
    {
        void Run(ConsoleColor color);
    }

    public class IocJob : IIocJob
    {
        private readonly IOutput _output;

        public IocJob(IOutput output) { _output = output; }

        public void Run(ConsoleColor color)
        {
            while (true)
            {                
                _output.Write(color, $"ping!~~~~ {DateTime.Now}");
                Thread.Sleep(1000);
            }
        }
    }
}