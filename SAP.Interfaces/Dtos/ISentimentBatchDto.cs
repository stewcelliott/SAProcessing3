using System;

namespace SAP.Interfaces.Dtos
{
    public interface ISentimentBatchDto
    {
        int Id { get; set; }
        int BatchLimit { get; set; }
        int BatchSize { get; set; }
        DateTime DateStart { get; set; }
        DateTime? DateFinish { get; set; }
    }
}
