using FluentValidation;
using Person.Application.Commands;
using Person.Application.Util;
using Person.Infra.Validator;

namespace Person.Application.Validators
{
    public interface ICreatePersonCommandValidator : IValidator<CreatePersonCommand> { }

    public class CreatePersonCommandValidator : CommandValidator<CreatePersonCommand>, ICreatePersonCommandValidator
    {
        public CreatePersonCommandValidator()
        {

        }

        protected override void CreateRules()
        {
            RuleFor(x => x.Name)
             .NotEmpty()
             .WithMessage("Name is required");

            RuleFor(x => x.BirthDate)
             .NotEmpty()
             .WithMessage("BirthDate is required");

            RuleFor(x => x.CPF)
             .NotEmpty()
             .WithMessage("Document is required");


            RuleFor(x => x.CPF)
              .Must(CommonValidators.CPFValidation)
              .WithMessage("Document is invalid");

        }

        public static class CommonValidators
        {
            public static bool CPFValidation(string? arg)
            {
                return DocumentValidation.IsValidCpf(arg);
            }
        }
    }
}
