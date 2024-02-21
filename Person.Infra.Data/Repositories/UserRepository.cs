using MongoDB.Driver;
using Person.Domain.Aggregates.UserAggregate;
using Person.Domain.Aggregates.UserAggregate.Interface;

namespace Person.Infra.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private const string CollectionName = "Users";

        public UserRepository(MongoContext context) : base(context, CollectionName)
        {
        }

        public async Task<User> GetByEmail(string username)
        {            
            var filter = Builders<User>.Filter.Eq(u => u.Email, username);
            var user = await _collection.Find(filter).FirstOrDefaultAsync();

            return user;
        }
    }
}
