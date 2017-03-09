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
using System.Collections.Generic;
using System.Diagnostics;
using SAP.Interfaces;
using SAP.Interfaces.Dtos;

namespace SAP.Process
{
    /// <summary>
    /// returns data from dataaccess project and performs any pre-processing
    /// </summary>
    public class Engine : IEngine
    {
        public ISentimentBatchDto StartBatch(int batchLimit, int batchSize, DateTime dateStart)
        {
            try
            {
                return DataAccess.Queue.StartBatch(batchLimit, batchSize, dateStart);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("StartBatch exception: {0}", ex.Message));
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
                throw ex;
            }
        }

        public void FinishBatch(ISentimentBatchDto sentimentBatch)
        {
            try
            {
                DataAccess.Queue.FinishBatch(sentimentBatch);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("FinishBatch exception: {0}", ex.Message));
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
                throw ex;
            }
        }

        public List<ISentimentQueueDto> GetSentimentQueueForProcessing(int batchSize, bool retryFailed= false)
        {
            try
            {
                return DataAccess.Queue.GetSentimentQueueForProcessing(batchSize, retryFailed);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("GetSentimentQueueForProcessing exception: {0}", ex.Message));
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
                throw ex;
            }
        }

        public void SaveSentimentQueueProcessingOutcome(ISentimentQueueDto sentimentQueue)
        {
            try
            {
                DataAccess.Queue.SaveSentimentQueueProcessingOutcome(sentimentQueue);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("SaveSentimentQueueProcessingOutcome exception: {0}", ex.Message));
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
                throw ex;
            }
        }
        
        public void SaveSentiment(ISentimentDto sentiment)
        {
            try
            {
                DataAccess.Sentiment.SaveSentiment(sentiment);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("SaveSentiment exception: {0}", ex.Message));
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
                throw ex;
            }
        }
    }
}
