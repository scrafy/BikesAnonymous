using DataLayer.Enums;
using System.IO;
using System.Threading.Tasks;

namespace DataLayer.Implementations
{
    public abstract class BaseRepository
    {
        #region protected methods

        protected virtual StreamReader GetFileStream(DB db)
        {
            if (!File.Exists(@$"{Directory.GetCurrentDirectory()}/DataLayer/DB/{db.ToString()}.json"))
                CreateDB(db);

            return new StreamReader(@$"{Directory.GetCurrentDirectory()}/DataLayer/DB/{db.ToString()}.json");
        }

        protected virtual void CreateDB(DB db)
        {
            using (var writer = new StreamWriter(@$"{Directory.GetCurrentDirectory()}/DataLayer/DB/{db.ToString()}.json", false))
            {

            }
        }

        protected virtual async Task WriteDataAsync(string jsonData, DB db)
        {
            using (var writer = new StreamWriter(@$"{Directory.GetCurrentDirectory()}/DataLayer/DB/{db.ToString()}.json", false))
            {
                await writer.WriteAsync(jsonData);

            }
        }

        #endregion
    }
}
