using Person.Application.Commands;


namespace Person.Application.Services.Interfaces
{
    public interface IUserService
    {        
        Task<CommandResponse<List<Domain.Aggregates.UserAggregate.User>>> GetAll();
        Task<CommandResponse<Domain.Aggregates.UserAggregate.User>> GetById(string id);
        Task<CommandResponse<Domain.Aggregates.UserAggregate.User>> Create(CreateUserCommand userCommand);
        Task<CommandResponse<Domain.Aggregates.UserAggregate.User>> Update(UpdateUserCommand userCommand);
        Task<CommandResponse<bool?>> Delete(string id);

    }
}
