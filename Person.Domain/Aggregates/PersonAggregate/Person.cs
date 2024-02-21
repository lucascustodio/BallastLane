using Person.Domain.Core;

namespace Person.Domain.Aggregates.PersonAggregate
{
    public class Person : Entity
    {
        public Person()
        {

        }
        public Person(string? id, string? name, string? document, DateTime birthDate)
        {
            Id = id;
            Name = name;
            CPF = document;
            BirthDate = birthDate;
        }

        public Person(string? name, string? document, DateTime birthDate)
        {
            Name = name;
            CPF = document;
            BirthDate = birthDate;
        }

        public string? Name { get; set; }
        public string? CPF { get; set; }
        public DateTime? BirthDate { get; set; }

        public void Update(string? name, string? cPF, DateTime birthDate)
        {
            Name = name;
            CPF = cPF;
            BirthDate = birthDate;
        }
    }
}
