# Dside.AspNetCore.Authentication.ApiKey
API Key authentication for AspNet Core

Authenication handler for AspNet Core to support Api Key authentication. Api key can be supplied as URL parameter or http header.

## Usage:
1. Implement IApiKeyAuthenticationClaimsResolver.
2. Enable ApiKey Authentication
   Examples:
   - `services.AddAuthentication(ApiKeyAuthenticationOptions.SchemeName).AddApiKey<ApiKeyResolver>();`
   - `services.AddAuthentication(ApiKeyAuthenticationOptions.SchemeName).AddApiKey(opts => config.Bind("ApiKeyConfig", opts));`

The default URL parameter name is `api-key`, http header - `X-Api-Key` but `ApiKeyAuthenticationOptions` allow to change those.
