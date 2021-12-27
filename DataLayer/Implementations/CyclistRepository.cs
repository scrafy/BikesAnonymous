using Core.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Implementations
{
    public class CyclistRepository : ICyclistRepository<Cyclist>
    {
        #region constructor

        public CyclistRepository() {

            
        }

        #endregion


        #region public methods

        public async Task<IEnumerable<Cyclist>> AllAsync()
        {
            using (var stream = GetFileStream())
            {
                return JsonConvert.DeserializeObject<IEnumerable<Cyclist>>(stream.ReadToEnd());

            }
        }

        public async void DeleteAsync(Cyclist entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Cyclist> GetAsync(Guid id)
        {
            using (var stream = GetFileStream())
            {
                var cyclists =  JsonConvert.DeserializeObject<List<Cyclist>>(stream.ReadToEnd());
                return cyclists.FirstOrDefault(c => c.Id == id);
            }
        }

        public async Task<Cyclist> GetCyclistByUsernameAndPasswordAsync(string username, string password)
        {
            using (var stream = GetFileStream())
            {
                var cyclists = JsonConvert.DeserializeObject<List<Cyclist>>(stream.ReadToEnd());
                if (cyclists.Count() == 0)
                    throw new Exception("The Cylist DB is empty, please, load some cyclists");

                return cyclists.FirstOrDefault(c => c.Username == username && c.Password == password);
            }
        }

        public async void SaveAllAsync(IEnumerable<Cyclist> entities)
        {
            if (entities.Count() == 0)
                return;

            using (var stream = GetFileStream())
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
                await WriteData(JsonConvert.SerializeObject(cyclists));
                return;

            }
        }

        public async void SaveAsync(Cyclist entity)
        {
            using (var stream = GetFileStream())
            {
                var cyclists = JsonConvert.DeserializeObject<List<Cyclist>>(stream.ReadToEnd());
                if (cyclists.Count() > 0)
                {
                    var emailList = cyclists.Select(c => c.Email).ToList();
                    if (emailList.Exists(c => c == entity.Email))
                        throw new Exception($"Email address in use: {entity.Email}");
                }                
                stream.Close();
                cyclists.Add(entity);
                await WriteData(JsonConvert.SerializeObject(cyclists));

            }
        }

        #endregion


        #region private methods

        private StreamReader GetFileStream()
        {
            if (!File.Exists(@$"{Directory.GetCurrentDirectory()}/DataLayer/DB/Cyclist.json"))
                CreateDB();

            return new StreamReader(@$"{Directory.GetCurrentDirectory()}/DataLayer/DB/Cyclist.json");
        }

        private void CreateDB()
        {
            using (var writer = new StreamWriter(@$"{Directory.GetCurrentDirectory()}/DataLayer/DB/Cyclist.json", false))
            {

            }
        }

        private async Task WriteData(string jsonData)
        {
            using (var writer = new StreamWriter(@$"{Directory.GetCurrentDirectory()}/DataLayer/DB/Cyclist.json", false))
            {
                await writer.WriteAsync(jsonData);
                
            }
        }

        #endregion



    }
}
