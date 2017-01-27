using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.Interfaces;
using SAP.Dtos;
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
