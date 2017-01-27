using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.Configuration;

namespace SAP.WinSvc
{
    partial class SentimentQueueManager : ServiceBase
    {

        private static Timer _timer;

        public SentimentQueueManager()
        {
            InitializeComponent();
            Map.Mappings.Setup();
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer { Interval = Convert.ToInt32(ConfigurationManager.AppSettings["timerInterval"]) };
            _timer.Elapsed += ProcessBatch;
            _timer.Start();
        }

        protected override void OnStop()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        private static void ProcessBatch(object source, ElapsedEventArgs e)
        {
            Trace.WriteLine("Processing Batch...");
            _timer.Stop();

            try
            {
                var batchLimit = Convert.ToInt32(ConfigurationManager.AppSettings["batchLimit"]);
                var retryFailed = false;
                if (ConfigurationManager.AppSettings["retryFailed"] != null)
                {
                    retryFailed = bool.Parse(ConfigurationManager.AppSettings["retryFailed"]);
                }
                Helper.ProcessBatch(batchLimit, retryFailed);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Error processing sentiments: [{0}]", ex.ToString()));
            }

            _timer.Start();
            Trace.WriteLine("Batch Processing Ended.");
        }
    }
}
