using System.Threading.Tasks;

namespace TraversalServices.Interfaces
{
    public interface IPDFGeneratorService
    {
        Task<byte[]> ObjectToPDFAsync<U>(U obj) where U : class;
    }
}
