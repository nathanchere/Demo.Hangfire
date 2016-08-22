using System;
using System.Diagnostics;

namespace Demo.Hangfire
{    
    public interface IOutput
    {
        void Write(ConsoleColor color, string output);
    }

    public class ConsoleOutput : IOutput
    {
        public ConsoleOutput() { Write(ConsoleColor.DarkGray, "IOutput .ctor called"); }

        protected static readonly object Locktarget = new object();

        public virtual void Write(ConsoleColor color, string output)
        {
            lock (Locktarget)
            {
                Console.ForegroundColor = color;
                Console.Write($"[{DateTime.Now.ToShortTimeString()}]: ");
                Console.ResetColor();
                Console.WriteLine(output);
            }
        }
    }

    public class DebugOutput : IOutput
    {
        public void Write(ConsoleColor color, string output)
        {
            Debug.Write($"[{DateTime.Now.ToShortTimeString()}]: {output}");
        }
    }

}