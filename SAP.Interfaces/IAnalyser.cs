using SAP.DataModel;
using SAP.Interfaces.Dtos;

namespace SAP.Interfaces
{
    public interface IAnalyser
    {
        void Init(string sentimentModelPath);
        ISentimentDto GetSentiment(ISentimentQueueDto sentimentQueueItem);
    }
}
