using Person.Application.Commands;
using Person.Application.Services.Interfaces;
using Person.Application.Validators;
using Person.Domain.Aggregates.PersonAggregate.Interface;

namespace Person.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICreatePersonCommandValidator _createCommandValidator;
        private readonly IUpdatePersonCommandValidator _updateCommandValidator;
        public PersonService(IPersonRepository PersonRepository, ICreatePersonCommandValidator commandValidator, IUpdatePersonCommandValidator updateCommandValidator)
        {

            _personRepository = PersonRepository;
            _createCommandValidator = commandValidator;
            _updateCommandValidator = updateCommandValidator;
        }

        public async Task<CommandResponse<Domain.Aggregates.PersonAggregate.Person>> Create(CreatePersonCommand personCommand)
        {
            var validator = _createCommandValidator.Validate(personCommand);

            if (!validator.IsValid)
            {
                var error = validator.Errors?.First()?.ToString();
                return new CommandResponse<Domain.Aggregates.PersonAggregate.Person>(null, error);
            }

            var person = new Domain.Aggregates.PersonAggregate.Person(personCommand.Name, personCommand.CPF, personCommand.BirthDate);

            await _personRepository.AddAsync(person);

            return new CommandResponse<Domain.Aggregates.PersonAggregate.Person>(person, null);
        }


        public async Task<CommandResponse<bool?>> Delete(string id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person == null)
            {
                return new CommandResponse<bool?>(null, "Person was not deleted");
            }

            await _personRepository.Remove(id);

            return new CommandResponse<bool?>(true, null);
        }

        public async Task<CommandResponse<List<Domain.Aggregates.PersonAggregate.Person>>> GetAll()
        {
            var person = await _personRepository.GetAll();
            return new CommandResponse<List<Domain.Aggregates.PersonAggregate.Person>>(person, null);
        }

        public async Task<CommandResponse<Domain.Aggregates.PersonAggregate.Person>> GetById(string id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person == null)
                return new CommandResponse<Domain.Aggregates.PersonAggregate.Person>(null, "Person was not found.");

            return new CommandResponse<Domain.Aggregates.PersonAggregate.Person>(person, null);
        }

        public async Task<CommandResponse<Domain.Aggregates.PersonAggregate.Person>> Update(UpdatePersonCommand personCommand)
        {
            var person = await _personRepository.GetByIdAsync(personCommand.Id);

            if (person == null)
                return new CommandResponse<Domain.Aggregates.PersonAggregate.Person>(null, "Person was not found");
                        
            person.Update(personCommand.Name, personCommand.CPF, personCommand.BirthDate);

            var validator = _updateCommandValidator.Validate(personCommand);

            if (!validator.IsValid)
                throw new Exception(validator.ToString());

            await _personRepository.Update(person.Id, person);

            return new CommandResponse<Domain.Aggregates.PersonAggregate.Person>(person, null);
        }
    }
}
