using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.sun.tools.javac.util;
using SAP.DataModel;
using SAP.Interfaces.Dtos;
using SAP.Process;
using System.Diagnostics;

namespace SAP.WinSvc
{
    public static class Helper
    {
        /// <summary>
        /// processes a batch of specified size
        /// </summary>
        /// <param name="batchLimit"></param>
        /// <param name="retryFailed"></param>
        public static void ProcessBatch(int batchLimit, bool retryFailed = false)
        {
            Factory.Current.RegisterInterfaces();
            var eng = Factory.Current.Engine();

            Trace.WriteLine("Calling eng.GetSentimentQueueForProcessing...");
            var queueToProcess = eng.GetSentimentQueueForProcessing(batchLimit, retryFailed);
            var batchSize = queueToProcess.Count();

            //if theres nothing to process jump out
            if (batchSize == 0) return;

            Trace.WriteLine("Calling eng.StartBatch...");
            var batch = eng.StartBatch(batchLimit, batchSize, DateTime.Now);

            var analyser = Factory.Current.Analyse();//moved below batch size check so it's not span up needlessly
            var sentimentModelPath = ConfigurationManager.AppSettings["sentimentModelPath"];
            Trace.WriteLine("Calling analyser.Init...");
            analyser.Init(sentimentModelPath);

            Trace.WriteLine("Processing queue...");
            foreach (var item in queueToProcess)
            {
                try
                {
                    item.BatchId = batch.Id;
                    item.Processed = true;
                    item.DateProcessed = DateTime.Now;
                    item.Error = null;
                    var sentiment = analyser.GetSentiment(item);
                    eng.SaveSentiment(sentiment);
                }

                catch (Exception ex)
                {
                    item.Error = true;
                    ISentimentQueueErrorDto error = new SentimentQueueErrorDto
                    {
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        DateCreated = DateTime.Now
                    };
                    item.SentimentQueueErrors.Add(error);
                }
                finally
                {
                    eng.SaveSentimentQueueProcessingOutcome(item);
                }
            }

            batch.DateFinish = DateTime.Now;
            eng.FinishBatch(batch);
        }
    }
}
