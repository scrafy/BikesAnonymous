using Microsoft.AspNetCore.Authorization;

namespace BikesAnonymous.Attributes
{
    public class JWTAuthorizeAttribute : AuthorizeAttribute
    {
        public JWTAuthorizeAttribute() : base()
        {

        }

        public JWTAuthorizeAttribute(string policy) : base(policy)
        {

        }
    }
}
