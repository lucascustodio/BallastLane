using Person.Infra.Validator;

namespace Person.Application.Commands
{
    public class CreatePersonCommand : Command
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CPF { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
