using Core.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Enums;
using Helpers;

namespace DataLayer.Implementations
{
    public class CyclistRepository : BaseRepository, ICyclistRepository<Cyclist>
    {
        #region constructor

        public CyclistRepository() {

            
        }     

        #endregion


        #region public methods
        
        public async Task<IEnumerable<Cyclist>> AllAsync()
        {
            using (var stream = GetFileStream(DB.Cyclist))
            {
                return JsonConvert.DeserializeObject<IEnumerable<Cyclist>>(stream.ReadToEnd());

            }
        }

        public async Task<Cyclist> GetAsync(Guid id)
        {
            using (var stream = GetFileStream(DB.Cyclist))
            {
                var cyclists =  JsonConvert.DeserializeObject<List<Cyclist>>(stream.ReadToEnd());
                return cyclists.FirstOrDefault(c => c.Id == id);
            }
        }

        public async Task<Cyclist> GetCyclistByUsernameAndPasswordAsync(string username, string password)
        {
            var cyclists = (await AllAsync()).ToList();
            return cyclists.FirstOrDefault(c => c.Username == username && c.Password == Security.GenerateMD5(password));
        }

        public async Task SaveAllAsync(IEnumerable<Cyclist> entities)
        {
            if (entities.Count() == 0)
                return;

            using (var stream = GetFileStream(DB.Cyclist))
            {
                var cyclists = JsonConvert.DeserializeObject<List<Cyclist>>(stream.ReadToEnd());
                if(cyclists.Count() > 0)
                {
                    var emailList = cyclists.Select(c => c.Email);
                    var emailsRepeated = emailList.Intersect(entities.Select(c => c.Email));
                    if (emailsRepeated.Count() > 0)
                        throw new Exception($"Email address in use: {emailsRepeated.First()}");

                }
                cyclists.AddRange(entities);
                stream.Close();
                await WriteDataAsync(JsonConvert.SerializeObject(cyclists), DB.Cyclist);
                return;

            }
        }

        public async Task SaveAsync(Cyclist entity)
        {
            using (var stream = GetFileStream(DB.Cyclist))
            {
                var cyclists = (await AllAsync()).ToList();
                if (cyclists.Count() > 0)
                {
                    var emailList = cyclists.Select(c => c.Email).ToList();
                    if (emailList.Exists(c => c == entity.Email))
                        throw new Exception($"Email address in use: {entity.Email}");
                }                
                stream.Close();
                cyclists.Add(entity);
                await WriteDataAsync(JsonConvert.SerializeObject(cyclists), DB.Cyclist);
                return;

            }
        }

        #endregion
        
    }
}
