using System.Security.Claims;
using System.Threading.Tasks;

namespace Dside.AspNetCore.Authentication.ApiKey
{
    public interface IApiKeyAuthenticationClaimsResolver
    {
        int Order { get; }
        /// <summary>
        /// Resolves Api Key to a set of UserClaims. 
        /// </summary>
        /// <param name="apiKey">Api Key</param>
        /// <returns>Array of Claims. Null indicate authentication failure.</returns>
        Task<Claim[]> Resolve(string apiKey);
    }
}
