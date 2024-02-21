using Person.Domain.Aggregates.PersonAggregate;
using Person.Domain.Interfaces;

namespace Person.Domain.Aggregates.PersonAggregate.Interface
{
    public interface IPersonRepository : IRepository<Person>
    {        
    }
}
