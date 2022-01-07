using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using DataLayer.Enums;
using DataLayer.Extensions;
using DataLayer.Interfaces;
using Helpers;
using Newtonsoft.Json;
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
            var owners = (await AllAsync())?.ToList();
            return owners?.FirstOrDefault(c => c.Username == username && c.Password == Security.GenerateMD5(password));
        }


        public async Task SaveAsync(Owner entity)
        {
            var owners = (await AllAsync())?.ToList() ?? new List<Owner>();

            if (owners.Count() > 0)
            {
                var emailRepeated = owners.FirstOrDefault(c => c.Email == entity.Email);
                if (emailRepeated != null)
                    throw new GenericException($"Email address in use: {emailRepeated.Email}", ErrorCode.BAD_REQUEST);

                var usernameRepeated = owners.FirstOrDefault(c => c.Username == entity.Username);
                if (usernameRepeated != null)
                {
                    throw new GenericException($"The username {usernameRepeated.Username} is repeated", ErrorCode.BAD_REQUEST);
                }
            }
            owners.Add(entity);
            await WriteDataAsync(JsonConvert.SerializeObject(owners.Select(o => o.OwnerEntityToDBModel())), DB.Owner);
            return;
        }

        #endregion

    }
}
