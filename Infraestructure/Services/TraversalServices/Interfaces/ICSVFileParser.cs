using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TraversalServices.Interfaces
{
    public interface ICSVFileParser
    {
        Task<IEnumerable<Cyclist>> GetCyclistUsersAsync( byte[] file );
    }
}
