using Core.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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

        private StreamReader GetFileStream()
        {
            if (!File.Exists(@$"{Directory.GetCurrentDirectory()}/DataLayer/DB/Owner.json"))
                CreateDB();

            return new StreamReader(@$"{Directory.GetCurrentDirectory()}/DataLayer/DB/Owner.json");
        }

        private void CreateDB()
        {
            using (var writer = new StreamWriter(@$"{Directory.GetCurrentDirectory()}/DataLayer/DB/Owner.json", false))
            {

            }
        }

        private async Task WriteData(string jsonData)
        {
            using (var writer = new StreamWriter(@$"{Directory.GetCurrentDirectory()}/DataLayer/DB/Owner.json", false))
            {
                await writer.WriteAsync(jsonData);

            }
        }
    }
}
