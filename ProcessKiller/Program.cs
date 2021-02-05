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

            [Option('n', "number", Required = false, HelpText = "The number of times to kill the process.")]
            public int NumTimes { get; set; } = 1;

            [Option('i', "interval", Required = false, HelpText = "The interval (in milliseconds) between killing.")]
            public int Interval { get; set; } = 0;

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
                Process[] processes = Process.GetProcessesByName(process);
                if (processes.Length > 0)
                {
                    foreach (Process p in processes)
                    {
                        if (logging)
                        {
                            Console.WriteLine($"Killing process '{process}/{p.Id}'.");
                        }

                        try
                        {
                            p.Kill();
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
                else
                {
                    if(logging)
                    {
                        Console.WriteLine($"Process '{process}' was not running.");
                    }
                }

                if (logging)
                {
                    Console.WriteLine("Sleeping...");
                }

                Thread.Sleep(interval);
            }
        }
    }
}
