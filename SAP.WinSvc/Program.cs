/*
    Copyright 2016 Healthcare Communications UK Ltd
 
    This file is part of HCSentimentAnalysisProcessor.

    HCSentimentAnalysisProcessor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    HCSentimentAnalysisProcessor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with HCSentimentAnalysisProcessor.  If not, see <http://www.gnu.org/licenses/>.

 */

using System;
using System.IO;
using System.ServiceProcess;

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
