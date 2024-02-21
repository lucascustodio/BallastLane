using Person.Infra.Validator;

namespace Person.Application.Commands
{
    public class UpdateUserCommand : Command
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }        
    }
}
