using System;

namespace SAP.Interfaces.Dtos
{
    public class SentimentQueueErrorDto : ISentimentQueueErrorDto
    {
        public int Id { get; set; }
        public int SentimentQueueId { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
