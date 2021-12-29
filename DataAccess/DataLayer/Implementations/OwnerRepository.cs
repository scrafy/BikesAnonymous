using Core.Entities;
using DataLayer.Enums;
using DataLayer.Interfaces;
using Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Implementations
{
    public class OwnerRepository : BaseRepository, IOwnerRepository<Owner>
    {

        #region constructor
        
        public OwnerRepository() { }

        #endregion

        #region public methods

        public async Task<IEnumerable<Owner>> AllAsync()
        {
            using (var stream = GetFileStream(DB.Owner))
            {
                return JsonConvert.DeserializeObject<IEnumerable<Owner>>(stream.ReadToEnd());
            }
        }


        public async Task<Owner> GetOwnerByUsernameAndPasswordAsync(string username, string password)
        {
            var owners = (await AllAsync()).ToList();
            return owners.FirstOrDefault(c => c.Username == username && c.Password == Security.GenerateMD5(password));
        }


        public async Task SaveAsync(Owner entity)
        {
            var owners = (await AllAsync()).ToList();
            if (owners.Count() > 0)
            {
                var emailList = owners.Select(c => c.Email).ToList();
                if (emailList.Exists(c => c == entity.Email))
                    throw new Exception($"Email address in use: {entity.Email}");
            }
            owners.Add(entity);
            await WriteDataAsync(JsonConvert.SerializeObject(owners), DB.Owner);
            return;
        }

        #endregion

    }
}
