using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ICyclistRepository<U> : IBaseRepository<U> where U : Cyclist
    {
        Task<U> GetCyclistByUsernameAndPasswordAsync(string username, string password);
    }
}
