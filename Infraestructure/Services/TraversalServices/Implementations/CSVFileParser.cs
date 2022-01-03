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
using CsvHelper.TypeConversion;
using System;

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
            //TO-DO read from appsettings.json
            _csvConfiguration = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true               

            };
            _csvConfiguration.ReadingExceptionOccurred = (ex) =>
            {
                throw ex.Exception;
            };
        }

        #endregion


        #region public methods

        public async Task<IEnumerable<Cyclist>> GetCyclistUsersAsync(byte[] csvUsersFile)
        {
            using (var reader = new StreamReader(new MemoryStream(csvUsersFile)))
            {
                using (var csv = new CsvReader(reader, _csvConfiguration))
                {
                    var task = Task.Factory.StartNew(() => csv.GetRecords<CyclistDTO>());
                    return (await task).ToList().Select(obj => obj.ToDomain());                    
                }
            }
            
        }        

        #endregion
    }
}
