using Flunt.Notifications;

namespace Person.Application.Commands
{
    public class CommandResponse<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
        public bool IsSuccess => string.IsNullOrEmpty(Error);

        public CommandResponse(T data, string error)
        {
            Data = data;
            Error = error;
        }
    }
}
