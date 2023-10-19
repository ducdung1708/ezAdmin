using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Models.Models.Response
{
    public class BaseResponse<T>
    {
        public string StatusCode { get; set; }
        public string Messages { get; set; }
        public List<MessageResponseBase> MessagesDetails { get; set; } = new List<MessageResponseBase>();
        public T Data { get; set; } = default;

        public BaseResponse() { }

        /// <summary>
        /// Valid Model
        /// </summary>
        /// <param name="modelState"></param>
        public BaseResponse(ModelStateDictionary modelState)
        {
            Messages = "Validation form error";
            StatusCode = StatusCodes.Status422UnprocessableEntity.ToString();
            MessagesDetails = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new MessageResponseBase(key, x.ErrorMessage)))
                .ToList();
        }

    }

    public class MessageResponseBase
    {
        public string Field { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="message"></param>
        public MessageResponseBase(string field, string message)
        {
            Field = field;
            Message = !string.IsNullOrEmpty(message) || !string.IsNullOrWhiteSpace(message) ?  message : "";
        }
    }
}
