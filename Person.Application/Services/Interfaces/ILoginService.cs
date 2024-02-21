namespace Person.Application.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(string username, string password);
    }
}
