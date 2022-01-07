using Core.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Enums;
using Helpers;
using Core.Exceptions;
using Core.Enums;
using DataLayer.Extensions;
using DataLayer.Comparers;

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
            var cyclists = (await AllAsync())?.ToList();
            return cyclists?.FirstOrDefault(c => c.Id == id);
           
        }

        public async Task<Cyclist> GetCyclistByUsernameAndPasswordAsync(string username, string password)
        {
            var cyclists = (await AllAsync())?.ToList();
            return cyclists?.FirstOrDefault(c => c.Username == username && c.Password == Security.GenerateMD5(password));
        }

        public async Task SaveAllAsync(IEnumerable<Cyclist> entities)
        {
            if (entities.Count() == 0)
                return;

            var groups = entities.GroupBy(c => c.Username).ToList();
            if (groups.Exists(g => g.Count() > 1))
                throw new GenericException($"The username {groups.First(g => g.Count() > 1).First().Username} is repeated", ErrorCode.BAD_REQUEST);

            groups = entities.GroupBy(c => c.Email).ToList();
            if (groups.Exists(g => g.Count() > 1))
                throw new GenericException($"Email address in use: {groups.First(g => g.Count() > 1).First().Email}", ErrorCode.BAD_REQUEST);

            var cyclists = (await AllAsync())?.ToList() ?? new List<Cyclist>();

            if (cyclists.Count() > 0)
            {
                var emailsRepeated = cyclists.Intersect(entities, new CyclistComparer(COMPARERBY.EMAIL));
                if (emailsRepeated.Count() > 0)
                    throw new GenericException($"Email address in use: {emailsRepeated.First().Email}", ErrorCode.BAD_REQUEST);

                var usernameRepeated = cyclists.Intersect(entities, new CyclistComparer(COMPARERBY.USERNAME));
                if (usernameRepeated.Count() > 0)
                {
                    throw new GenericException($"The username {usernameRepeated.First().Username} is repeated", ErrorCode.BAD_REQUEST);
                }

            }
            cyclists.AddRange(entities);
            await WriteDataAsync(JsonConvert.SerializeObject(cyclists.Select(c => c.CyclistEntityToDBModel())), DB.Cyclist);
            return;
        }

        public async Task SaveAsync(Cyclist entity)
        {
            var cyclists = (await AllAsync())?.ToList() ?? new List<Cyclist>();
            if (cyclists.Count() > 0)
            {
                var emailsRepeated = cyclists.FirstOrDefault(c => c.Email == entity.Email);
                if (emailsRepeated != null)
                    throw new GenericException($"Email address in use: {emailsRepeated.Email}", ErrorCode.BAD_REQUEST);

                var usernameRepeated = cyclists.FirstOrDefault(c => c.Username == entity.Username);
                if (usernameRepeated != null)
                {
                    throw new GenericException($"The username {usernameRepeated.Username} is repeated", ErrorCode.BAD_REQUEST);
                }
            }
            cyclists.Add(entity);
            await WriteDataAsync(JsonConvert.SerializeObject(cyclists.Select(c => c.CyclistEntityToDBModel())), DB.Cyclist);
            return;
        }

        #endregion

    }
}
