using System;
using System.Linq;
using System.Threading;
using Demo.Hangfire.Db;

namespace Demo.Hangfire
{
    public interface IRegressionMonitor
    {
        void Run(RegressionMonitorSettings settings);
    }

    public class RegressionMonitor : IRegressionMonitor
    {
        private readonly ConsoleOutput _output;
        private static readonly Random random = new Random();
        private readonly PensionContext _context;

        public RegressionMonitor(PensionContext context)
        {
            _output = new ConsoleOutput();
            _context = context;
            Console.WriteLine("EfJob Injected ctor");
        }

        public void Run(RegressionMonitorSettings settings)
        {
            while (true)
            {
                var skip = random.Next(0, 50);
                var item = _context.FileRegistries.OrderByDescending(f => f.FileId).Skip(skip).First();

                if (item.StatusId == settings.StatusIdToMonitor)
                {
                    _output.Write(ConsoleColor.Red, "*** ALERT ***  PROBLEM FILE DETECTED!");
                }

                _output.Write(settings.LogColor, $"{skip}{ToOrdinal(skip)} file: {item.FileId} (created {item.Created}), {item.StatusType.Description}");

                Thread.Sleep(2000);
            }
        }

        private string ToOrdinal(int num)
        {
            if (num.ToString().EndsWith("11")) return "th";
            if (num.ToString().EndsWith("12")) return "th";
            if (num.ToString().EndsWith("13")) return "th";
            if (num.ToString().EndsWith("1")) return "st";
            if (num.ToString().EndsWith("2")) return "nd";
            if (num.ToString().EndsWith("3")) return "rd";
            return "th";
        }
    }

    public struct RegressionMonitorSettings
    {
        public RegressionMonitorSettings(int statusIdToMonitor, ConsoleColor logColor)
        {
            StatusIdToMonitor = statusIdToMonitor;
            LogColor = logColor;
        }

        public int StatusIdToMonitor;
        public ConsoleColor LogColor;
    }
}