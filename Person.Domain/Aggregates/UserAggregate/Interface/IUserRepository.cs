using Person.Domain.Interfaces;

namespace Person.Domain.Aggregates.UserAggregate.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string username);
    }
}
