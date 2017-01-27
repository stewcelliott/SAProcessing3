using System;
using System.Collections.Generic;
using SAP.DataModel;
using SAP.Interfaces.Dtos;

namespace SAP.Interfaces
{
    public interface IEngine
    {
        ISentimentBatchDto StartBatch(int batchLimit, int batchSize, DateTime dateStart);
        void FinishBatch(ISentimentBatchDto sentimentBatch);
        List<ISentimentQueueDto> GetSentimentQueueForProcessing(int batchSize, bool retryFailed = false);
        void SaveSentimentQueueProcessingOutcome(ISentimentQueueDto sentimentQueue);
        //TODO: Remove commented items if superfluous
        //List<ISentimentDto> GetSentiments();
        //ISentimentDto GetSentimentById(int id);
        void SaveSentiment(ISentimentDto sentiment);
        //List<ISentimentSentenceDto> GetSentimentSentences(ISearchFilterDto searchFilter);
        //ISentimentSentenceDto GetSentimentSentence(int id);
        //bool FlagSentimentHiddenById(int id);
    }
}
