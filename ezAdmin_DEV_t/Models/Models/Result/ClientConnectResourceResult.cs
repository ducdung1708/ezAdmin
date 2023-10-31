using System.ComponentModel;
using System.Data;

namespace Models.Models.Result
{
    public class ClientConnectResourceResult
    {
        public Guid ClientId { get; set; }
        public string? ClientCode { get; set; }
        public string? ClientName { get; set; }
        public string? Logo { get; set; }
    }
}
