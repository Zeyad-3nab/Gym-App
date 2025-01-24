using Gym.Api.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Api.BLL.Interfaces
{
    public interface IGenaricRepository<T , Key> where T : BaseModel<Key>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> SearchByName(string Name);
        Task<T> GetByIdAsync(Key Id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
