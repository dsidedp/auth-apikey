using System;
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
    }
}
