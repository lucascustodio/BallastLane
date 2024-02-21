using Person.Domain.Aggregates.PersonAggregate.Interface;

namespace Person.Infra.Data.Repositories
{
    public class PersonRepository : Repository<Domain.Aggregates.PersonAggregate.Person>, IPersonRepository
    {
        private const string CollectionName = "Persons";

        public PersonRepository(MongoContext context) : base(context, CollectionName)
        {
        }
    }
}
