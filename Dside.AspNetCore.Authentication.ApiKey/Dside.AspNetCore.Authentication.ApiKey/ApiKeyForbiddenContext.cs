using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Dside.AspNetCore.Authentication.ApiKey
{
    public class ApiKeyForbiddenContext : ResultContext<ApiKeyAuthenticationOptions>
    {
        public ApiKeyForbiddenContext(HttpContext context, AuthenticationScheme scheme, ApiKeyAuthenticationOptions options) : base(context, scheme, options) { }
    }
}