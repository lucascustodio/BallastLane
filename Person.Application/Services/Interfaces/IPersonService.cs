using Person.Application.Commands;

namespace Person.Application.Services.Interfaces
{
    public interface IPersonService
    {
        Task<CommandResponse<List<Domain.Aggregates.PersonAggregate.Person>>> GetAll();
        Task<CommandResponse<Domain.Aggregates.PersonAggregate.Person>> GetById(string id);
        Task<CommandResponse<Domain.Aggregates.PersonAggregate.Person>> Create(CreatePersonCommand personCommand);
        Task<CommandResponse<Domain.Aggregates.PersonAggregate.Person>> Update(UpdatePersonCommand personCommand);
        Task<CommandResponse<bool?>> Delete(string id);

    }
}
