using FluentValidation;
using Person.Application.Commands;
using Person.Application.Util;
using Person.Infra.Validator;

namespace Person.Application.Validators
{
    public interface IUpdateUserCommandValidator : IValidator<UpdateUserCommand> { }

    public class UpdateUserCommandValidator : CommandValidator<UpdateUserCommand>, IUpdateUserCommandValidator
    {
        public UpdateUserCommandValidator()
        {

        }

        protected override void CreateRules()
        {
            RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID required");

            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("Name is required");

            RuleFor(x => x.Email)
             .NotEmpty()
             .WithMessage("Email is required")
             .EmailAddress()
             .WithMessage("Email is invalid");
        }
    }
}
