using Microsoft.AspNetCore.Authentication;

namespace Dside.AspNetCore.Authentication.ApiKey
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string SchemeName = "ApiKey";
        public string Scheme => SchemeName;

        public string HeaderName { get; set; } = "X-Api-Key";
        public string UrlParameterName { get; set; } = "api-key";

        protected new ApiKeyEvents Events
        {
#if NETSTANDARD2_0
            get => (ApiKeyEvents)base.Events;
#else
            get => (ApiKeyEvents)base.Events!;
#endif
            set => base.Events = value;
        }
    }
}
