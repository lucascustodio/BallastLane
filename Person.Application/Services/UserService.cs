using Flunt.Notifications;
using Person.Application.Commands;
using Person.Application.Services.Interfaces;
using Person.Application.Validators;
using Person.Domain.Aggregates.UserAggregate.Interface;

namespace Person.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICreateUserCommandValidator _createCommandValidator;
        private readonly IUpdateUserCommandValidator _updateCommandValidator;
        public UserService(IUserRepository userRepository, ICreateUserCommandValidator commandValidator, IUpdateUserCommandValidator updateCommandValidator)
        {

            _userRepository = userRepository;
            _createCommandValidator = commandValidator;
            _updateCommandValidator = updateCommandValidator;
        }

        public async Task<CommandResponse<Domain.Aggregates.UserAggregate.User>> Create(CreateUserCommand userCommand)
        {
            var validator = _createCommandValidator.Validate(userCommand);

            if (!validator.IsValid)
            {
                var error = validator.Errors?.First()?.ToString();
                return new CommandResponse<Domain.Aggregates.UserAggregate.User>(null, error);
            }

            var user = new Domain.Aggregates.UserAggregate.User(userCommand.Name, userCommand.Email, userCommand.Password);

            await _userRepository.AddAsync(user);

            return new CommandResponse<Domain.Aggregates.UserAggregate.User>(user, null);
        }

        public async Task<CommandResponse<bool?>> Delete(string id)
        {
            var person = await _userRepository.GetByIdAsync(id);

            if (person == null)
            {
                return new CommandResponse<bool?>(null, "User was not deleted");
            }

            await _userRepository.Remove(id);

            return new CommandResponse<bool?>(true, null);
        }

        public async Task<CommandResponse<List<Domain.Aggregates.UserAggregate.User>>> GetAll()
        {
            var person = await _userRepository.GetAll();
            return new CommandResponse<List<Domain.Aggregates.UserAggregate.User>>(person, null);
        }

        public async Task<CommandResponse<Domain.Aggregates.UserAggregate.User>> GetById(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return new CommandResponse<Domain.Aggregates.UserAggregate.User>(null, "User was not found.");

            return new CommandResponse<Domain.Aggregates.UserAggregate.User>(user, null);
        }

        public async Task<CommandResponse<Domain.Aggregates.UserAggregate.User>> Update(UpdateUserCommand userCommand)
        {
            var user = await _userRepository.GetByIdAsync(userCommand.Id);

            if (user == null)
                return new CommandResponse<Domain.Aggregates.UserAggregate.User>(null, "User was not found.");

            user.Update(userCommand.Name, userCommand.Email);
            var validator = _updateCommandValidator.Validate(userCommand);

            if (!validator.IsValid)
            {
                var error = validator.Errors?.First()?.ToString();
                return new CommandResponse<Domain.Aggregates.UserAggregate.User>(null, error);
            }

            await _userRepository.Update(user.Id, user);

            return new CommandResponse<Domain.Aggregates.UserAggregate.User>(user, null);
        }
    }
}
