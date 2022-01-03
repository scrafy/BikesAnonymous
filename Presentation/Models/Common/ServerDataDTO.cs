
namespace BikesAnonymous.Models.Common
{
    public class ServerDataDTO<T> where T : class
    {   
        public T Data { get; set; }
        public PaginationDTO PaginationData { get; set; }
    }

}