using Core.Enums;

namespace TraversalServices.Interfaces
{
    public interface ITokenGeneratorService
    {
        string CreateToken(string secret, ROLE role);
    }
}
