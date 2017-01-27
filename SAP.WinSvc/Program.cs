using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SAP.WinSvc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            string logDirectory = Path.Combine(Environment.CurrentDirectory, "logs");

            if(!Directory.Exists(logDirectory)) {
                Directory.CreateDirectory(logDirectory);
            }

            System.Diagnostics.Trace.WriteLine("Starting Sentiment Service");
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new SentimentQueueManager(),  
            };
            ServiceBase.Run(ServicesToRun);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(e.ExceptionObject);
        }
    }
}
