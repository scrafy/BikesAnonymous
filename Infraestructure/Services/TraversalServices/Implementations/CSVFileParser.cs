using Core.Entities;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TraversalServices.Interfaces;
using TraversalServices.Models;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Core.Exceptions;
using Core.Enums;

namespace TraversalServices.Implementations
{
    public class CSVFileParser : ICSVFileParser
    {
        #region private properties
        private CsvConfiguration _csvConfiguration;

        #endregion


        #region constructor

        public CSVFileParser()
        {
            _csvConfiguration = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true               

            };
            _csvConfiguration.ReadingExceptionOccurred = (ex) =>
            {
                throw new GenericException("CSV file invalid", ErrorCode.BAD_REQUEST);
            };
        }

        #endregion


        #region public methods

        public async Task<IEnumerable<Cyclist>> GetCyclistUsersAsync(byte[] csvUsersFile)
        {
            using (var reader = new StreamReader(new MemoryStream(csvUsersFile)))
            {
                reader.BaseStream.Position = 0;
                using (var csv = new CsvReader(reader, _csvConfiguration))
                {
                    var task = Task.Factory.StartNew(() => csv.GetRecords<CyclistDTO>());
                    var cyclist = (await task).ToList();
                    return cyclist.Select(obj => obj.ToDomain());                    
                }
            }
            
        }        

        #endregion
    }
}
