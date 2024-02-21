using Person.Domain.Core;

namespace Person.Domain.Aggregates.UserAggregate
{
    public class User : Entity
    {
        public User(string? id, string? name, string? email, string? password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = Core.Util.Crypto.HashPassword(password);
        }

        public User(string? name, string? email, string? password)
        {
            Name = name;
            Email = email;
            Password = Core.Util.Crypto.HashPassword(password);
        }

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public void Update(string? name, string? email)
        {
            Name = name;
            Email = email;
        }
    }
}
