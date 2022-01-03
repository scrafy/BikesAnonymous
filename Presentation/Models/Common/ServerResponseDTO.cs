using Core.Exceptions;


namespace BikesAnonymous.Models.Common
{
    public class ServerResponseDTO<T> where T : class
    {
        public short StatusCode { get; set; }

        public object ServerError { get; set; }

        public ServerDataDTO<T> ServerData { get; set; }
    }
}
