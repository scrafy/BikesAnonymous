using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IOwnerRepository<U> : IBaseRepository<U> where U : Owner
    {
        Task<U> GetOwnerByUsernameAndPasswordAsync(string username, string password);
    }
}
