using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IBaseRepository<U> where U : BaseEntity
    {
        void SaveAllAsync(IEnumerable<U> entities);
        void SaveAsync(U entity);
        void DeleteAsync(U entity);
        Task<IEnumerable<U>> AllAsync();
        Task<U> GetAsync(Guid id);
    }
}
