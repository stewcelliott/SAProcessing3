using System;
using System.Collections.Generic;
using SAP.Interfaces.Dtos;

namespace SAP.Dtos
{
    public class SentimentSentenceDto : ISentimentSentenceDto
    {
        public int Id { get; set; }
        public int SentimentId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public DateTime DateCreated { get; set; }
        //public ISentimentDto Sentiment { get; set; }
        
    }
}
