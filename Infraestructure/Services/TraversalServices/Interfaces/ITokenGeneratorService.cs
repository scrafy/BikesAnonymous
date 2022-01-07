using System.Collections.Generic;

namespace TraversalServices.Interfaces
{
    public interface ITokenGeneratorService
    {
        string CreateToken(string secret, Dictionary<string, string> claims);
    }
}
