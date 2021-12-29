using Core.Entities;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TraversalServices.Interfaces;
using TraversalServices.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalServices.Implementations
{
    public class CSVFileParser : ICSVFileParser
    {
        #region constructor
        
        public CSVFileParser()
        {

        }

        #endregion


        #region public methods

        public async Task<IEnumerable<Cyclist>> GetCyclistUsersAsync(byte[] csvUsersFile)
        {
            using (var reader = new StreamReader(new MemoryStream(csvUsersFile)))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var task = Task.Factory.StartNew(() => csv.GetRecords<CyclistDTO>().Select(obj => obj.ToDomain()));
                    return await task;
                }
            }
            
        }

        #endregion
    }
}
