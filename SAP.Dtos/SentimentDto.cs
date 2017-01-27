using System;
using System.Collections.Generic;
using SAP.Interfaces.Dtos;

namespace SAP.Dtos
{
    public class SentimentDto : ISentimentDto
    {
        public int Id { get; set; }        
        public DateTime DateCreated { get; set; }
        public decimal? AverageScore { get; set; } //TODO: made this nullable for now, the DataModel needs to be updated to set AverageScore NOT NULL
        public List<ISentimentSentenceDto> SentimentSentences { get; set; }
    }
}
