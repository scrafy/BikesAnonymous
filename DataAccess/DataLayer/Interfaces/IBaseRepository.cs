using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IBaseRepository<U> where U : BaseEntity
    {
        Task SaveAsync(U entity);
        Task<IEnumerable<U>> AllAsync();

    }
}
