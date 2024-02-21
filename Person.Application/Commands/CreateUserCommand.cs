using Person.Infra.Validator;

namespace Person.Application.Commands
{
    public class CreateUserCommand : Command
    {
        public string? Name { get; set; }
        public string? Email { get; set; }        
        public string? Password { get; set; }        
    }
}
