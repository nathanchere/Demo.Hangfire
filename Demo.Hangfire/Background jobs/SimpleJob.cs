using System;
using System.Threading;

namespace Demo.Hangfire
{
    public static class SimpleJob
    {
        private static readonly ConsoleOutput ConsoleOutput = new ConsoleOutput();

        public static void Run(int counter)
        {
            ConsoleOutput.Write((ConsoleColor)counter+3, $"Running job {counter}");

            Thread.Sleep(5000); // simulate doing something

            ConsoleOutput.Write((ConsoleColor)counter+3, $"Job {counter} all done - hej då!");
        }
    }
}