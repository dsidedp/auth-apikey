using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Dside.AspNetCore.Authentication.ApiKey
{
    public class ApiKeyChallengeContext : PropertiesContext<ApiKeyAuthenticationOptions>
    {
        /// <summary>
        /// If true, will skip any default logic for this challenge.
        /// </summary>
        public bool Handled { get; private set; }

        /// <summary>
        /// Skips any default logic for this challenge.
        /// </summary>
        public void HandleResponse() => Handled = true;

        /// <summary>
        /// Any failures encountered during the authentication process.
        /// </summary>
#if NETSTANDARD2_0
        public Exception AuthenticateFailure { get; set; }
#else
        public Exception? AuthenticateFailure { get; set; }
#endif

        public ApiKeyChallengeContext(HttpContext context, AuthenticationScheme scheme, ApiKeyAuthenticationOptions options, AuthenticationProperties properties) 
            : base(context, scheme, options, properties) { }
    }
}