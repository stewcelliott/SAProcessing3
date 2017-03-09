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
using System.Configuration;
using System.Linq;
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
