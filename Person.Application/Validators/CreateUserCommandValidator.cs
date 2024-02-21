using FluentValidation;
using Person.Application.Commands;
using Person.Application.Util;
using Person.Infra.Validator;

namespace Person.Application.Validators
{
    public interface ICreateUserCommandValidator : IValidator<CreateUserCommand> { }

    public class CreateUserCommandValidator : CommandValidator<CreateUserCommand>, ICreateUserCommandValidator
    {
        public CreateUserCommandValidator()
        {

        }

        protected override void CreateRules()
        {
            RuleFor(x => x.Name)
             .NotEmpty()
             .WithMessage("Name is required");

            RuleFor(x => x.Email)
             .NotEmpty()
             .WithMessage("Email is required")
             .EmailAddress()
             .WithMessage("Email is invalid");

            RuleFor(x => x.Password)
             .NotEmpty()
             .WithMessage("Password is required");
        }
    }
}
