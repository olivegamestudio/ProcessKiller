using CommandLine;
using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        public class Options
        {
            [Option('p', "process", Required = true, HelpText = "The name of the process to kill.")]
            public string ProcessName { get; set; }

            [Option('n', "number", Required = true, HelpText = "The number of times to kill the process.")]
            public int NumTimes { get; set; }

            [Option('i', "interval", Required = true, HelpText = "The interval (in milliseconds) between killing.")]
            public int Interval { get; set; }

            [Option('l', "log", Required = false, HelpText = "Log the killing process.", Default=false)]
            public bool LogEnabled { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Process Killer");

            string process = string.Empty;
            int numTimes = 1;
            TimeSpan interval = TimeSpan.FromMilliseconds(0);
            bool logging = false;

            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       process = o.ProcessName;
                       numTimes = o.NumTimes;
                       logging = o.LogEnabled;
                       interval = TimeSpan.FromMilliseconds(o.Interval);
                   });

            for (int n = 0; n < numTimes; n++)
            {
                Process[] p = Process.GetProcessesByName(process);
                if (p.Length > 0)
                {
                    if(logging)
                    {
                        Console.WriteLine($"Killing process #{n+1} '{process}/{p[0].Id}'.");
                    }
                    p[0].Kill();
                }
                else
                {
                    if(logging)
                    {
                        Console.WriteLine($"Process #{n + 1} '{process}' was not running.");
                    }
                }

                if (logging)
                {
                    Console.WriteLine($"Sleeping...");
                }
                Thread.Sleep(interval);
            }
        }
    }
}
