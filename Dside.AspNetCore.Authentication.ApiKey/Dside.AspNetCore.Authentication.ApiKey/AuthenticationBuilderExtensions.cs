using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Dside.AspNetCore.Authentication.ApiKey
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder authenticationBuilder, Action<ApiKeyAuthenticationOptions> options = null)
        {
            return authenticationBuilder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.SchemeName, options);
        }

        public static AuthenticationBuilder AddApiKey<TResolver>(this AuthenticationBuilder authenticationBuilder, Action<ApiKeyAuthenticationOptions> options = null)
            where TResolver : class, IApiKeyAuthenticationClaimsResolver
        {
            authenticationBuilder.Services.AddTransient<IApiKeyAuthenticationClaimsResolver, TResolver>();
            return authenticationBuilder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.SchemeName, options);
        }

        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder authenticationBuilder, Func<string, Claim[]> resolveFunc, Action<ApiKeyAuthenticationOptions> options = null)
        {
            authenticationBuilder.Services.AddTransient<IApiKeyAuthenticationClaimsResolver>(sp => new DelegateClaimResolver(resolveFunc));
            return authenticationBuilder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.SchemeName, options);
        }

        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder authenticationBuilder, Func<IServiceProvider, string, Task<Claim[]>> resolveFunc, Action<ApiKeyAuthenticationOptions> options = null)
        {
            authenticationBuilder.Services.AddTransient<IApiKeyAuthenticationClaimsResolver>(sp => new DelegateClaimResolver((s) => resolveFunc(sp, s)));
            return authenticationBuilder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.SchemeName, options);
        }

        private class DelegateClaimResolver : IApiKeyAuthenticationClaimsResolver
        {
            private readonly Func<string, Task<Claim[]>> _resolveFunc;
            
            public int Order => 0;

            public DelegateClaimResolver(Func<string, Task<Claim[]>> resolveFunc) => _resolveFunc = resolveFunc;
            public DelegateClaimResolver(Func<string, Claim[]> resolveFunc) => _resolveFunc = (s) => Task.FromResult(resolveFunc(s));

            public async Task<Claim[]> Resolve(string apiKey) => await _resolveFunc(apiKey);
        }
    }
}
