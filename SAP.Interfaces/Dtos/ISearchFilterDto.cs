using System;

namespace SAP.Interfaces.Dtos
{
    public interface ISearchFilterDto
    {
        int? Score { get; set; }
        DateTime? DateFrom { get; set; }
        DateTime? DateTo { get; set; }
        string[] IncludeKeywords { get; set; }
        string[] ExcludeKeywords { get; set; }
        bool ExcludeTrainedAndDuplicates { get; set; }
    }
}