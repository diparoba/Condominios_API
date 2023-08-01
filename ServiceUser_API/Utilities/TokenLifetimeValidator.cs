using Microsoft.IdentityModel.Tokens;

namespace ServiceUser_API.Utilities
{
    public class TokenLifetimeValidator
    {
        public static bool Validate(
            DateTime? notBefore,
            DateTime? expires,
            SecurityToken tokenToValidate,
            TokenValidationParameters @param
            )
        {
            return (expires != null && expires > DateTime.UtcNow);
        }
    }
}