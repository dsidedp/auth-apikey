# Dside.AspNetCore.Authentication.ApiKey
API Key authentication for AspNet Core

Authentication handler for Asp Net Core to support Api Key authentication. Api key can be supplied as URL parameter or http header.

## Usage:
1. Implement IApiKeyAuthenticationClaimsResolver.
   It will be responsible for generation Claims based on ApiKey value.
   Multiple resolvers can be registered.
   If at least single claim returned from resolver - authentication will be successful.
2. Enable ApiKey Authentication like this:
   `services.AddAuthentication(ApiKeyAuthenticationOptions.SchemeName).AddApiKey<ApiKeyResolver>();`
   or like this
   `services.AddAuthentication(ApiKeyAuthenticationOptions.SchemeName).AddApiKey(opts => config.Bind("ApiKeyConfig", opts));`

The default URL parameter name is `api-key`, http header name - `X-Api-Key` but `ApiKeyAuthenticationOptions` allow to change those.
