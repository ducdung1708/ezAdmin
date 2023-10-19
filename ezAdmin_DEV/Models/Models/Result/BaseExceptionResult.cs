using Models.Models.Response;

namespace Models.Models.Result
{
    public class BaseExceptionResult : Exception
    {
        public string StatusCode = StatusCodes.Status400BadRequest.ToString();
        public string? Messages { get; set; }
        public List<MessageResponseBase> MessagesDetails { get; set; } = new List<MessageResponseBase>();
    }
}
