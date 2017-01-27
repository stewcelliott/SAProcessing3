using System;
using SAP.Interfaces.Dtos;

namespace SAP.Dtos
{
    public class SentimentBatchDto : ISentimentBatchDto
    {
        public int Id { get; set; }
        public int BatchLimit { get; set; }
        public int BatchSize { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateFinish { get; set; }
    }
}
