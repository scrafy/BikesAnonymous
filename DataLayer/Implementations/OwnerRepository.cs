using Core.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Implementations
{
    public class OwnerRepository : IOwnerRepository<Owner>
    {
        public OwnerRepository() { }

        public async Task<IEnumerable<Owner>> AllAsync()
        {
            throw new NotImplementedException();
        }

        public async void DeleteAsync(Owner entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Owner> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Owner> GetOwnerByUsernameAndPasswordAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async void SaveAllAsync(IEnumerable<Owner> entities)
        {
            throw new NotImplementedException();
        }

        public async void SaveAsync(Owner entity)
        {
            throw new NotImplementedException();
        }
    }
}
