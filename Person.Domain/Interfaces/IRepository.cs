using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> GetAll();
        Task<T> GetByIdAsync(string id);
        Task<T> Update(string id, T entity);
        Task Remove(string id);
    }
}
