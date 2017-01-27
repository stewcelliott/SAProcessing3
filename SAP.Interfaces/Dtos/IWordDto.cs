using System.Collections.Generic;

namespace SAP.Interfaces.Dtos
{
    public interface IWordDto
    {
        int Id { get; set; }
        int Score { get; set; }
        string Title { get; set; }
        List<IWordDto> Items { get; set; }
        bool IsWord { get; set; }
        bool CanDelete { get; set; }
    }
}
