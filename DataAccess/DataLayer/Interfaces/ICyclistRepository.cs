using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ICyclistRepository<U> : IBaseRepository<U> where U : Cyclist
    {
        Task<U> GetAsync(Guid id);
        Task<U> GetCyclistByUsernameAndPasswordAsync(string username, string password);
        Task SaveAllAsync(IEnumerable<U> entities);


    }
}
