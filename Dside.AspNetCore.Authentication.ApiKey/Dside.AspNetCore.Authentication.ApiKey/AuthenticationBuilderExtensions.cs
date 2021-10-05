using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Dside.AspNetCore.Authentication.ApiKey
{
    public static partial class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder authenticationBuilder, string schemeName, Action<ApiKeyAuthenticationOptions> options = null) 
            => authenticationBuilder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(schemeName, options);

        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder authenticationBuilder, Action<ApiKeyAuthenticationOptions> options = null) 
            => authenticationBuilder.AddApiKey(ApiKeyAuthenticationOptions.SchemeName, options);

        public static AuthenticationBuilder AddApiKey<TResolver>(this AuthenticationBuilder authenticationBuilder, Action<ApiKeyAuthenticationOptions> options = null)
            where TResolver : class, IApiKeyAuthenticationClaimsResolver
        {
            authenticationBuilder.Services.AddTransient<IApiKeyAuthenticationClaimsResolver, TResolver>();
            return authenticationBuilder.AddApiKey(options);
        }

        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder authenticationBuilder, string schemeName, Func<IServiceProvider, string, Task<Claim[]>> resolveFunc, Action<ApiKeyAuthenticationOptions> options = null)
        {
            authenticationBuilder.Services.AddTransient<IApiKeyAuthenticationClaimsResolver>(sp => new DelegateClaimResolver(schemeName, (s) => resolveFunc(sp, s)));
            return authenticationBuilder.AddApiKey(schemeName, options);
        }

        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder authenticationBuilder, Func<IServiceProvider, string, Task<Claim[]>> resolveFunc, Action<ApiKeyAuthenticationOptions> options = null)
            => authenticationBuilder.AddApiKey(ApiKeyAuthenticationOptions.SchemeName, resolveFunc, options);

        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder authenticationBuilder, Func<string, Claim[]> resolveFunc, Action<ApiKeyAuthenticationOptions> options = null) 
            => authenticationBuilder.AddApiKey((sp, s) => Task.FromResult(resolveFunc(s)), options);

        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder authenticationBuilder, string schemeName, Func<string, Claim[]> resolveFunc, Action<ApiKeyAuthenticationOptions> options = null)
            => authenticationBuilder.AddApiKey(schemeName, (sp, s) => Task.FromResult(resolveFunc(s)), options);
    }
}
