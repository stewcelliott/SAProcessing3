using System;
using System.Collections.Generic;

namespace SAP.Interfaces.Dtos
{
    public interface ISentimentSentenceDto
    {
        int Id { get; set; }
        int SentimentId { get; set; }
        string Text { get; set; }
        int Score { get; set; }
        DateTime DateCreated { get; set; }
        //ISentimentDto Sentiment { get; set; }
    }
}
