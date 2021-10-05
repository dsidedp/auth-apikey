using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dside.AspNetCore.Authentication.ApiKey
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly IApiKeyAuthenticationClaimsResolver[] _resolvers;

        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IEnumerable<IApiKeyAuthenticationClaimsResolver> resolvers) : base(options, logger, encoder, clock)
        {
            _resolvers = resolvers as IApiKeyAuthenticationClaimsResolver[] ?? resolvers.ToArray();
            if (_resolvers.Length == 0) throw new AuthenticationException("No Claim resolvers has been provided.");
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authToken = Request.Query[Options.UrlParameterName].FirstOrDefault() ?? Request.Headers[Options.HeaderName].FirstOrDefault();

            if (string.IsNullOrEmpty(authToken)) return AuthenticateResult.NoResult();

            var claims = Array.Empty<Claim>();

            foreach (var resolver in _resolvers.OrderBy(x => x.Order))
            {
                var resolvedClaims = await resolver.Resolve(authToken);
                if (resolvedClaims?.Any() == true)
                {
                    claims = claims.Union(resolvedClaims).ToArray();
                }
            }

            if (!claims.Any()) return AuthenticateResult.NoResult();

            var identity = new ClaimsIdentity(claims, Options.Scheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Options.Scheme);

            return AuthenticateResult.Success(ticket);
        }
    }
}