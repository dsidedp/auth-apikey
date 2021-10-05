using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dside.AspNetCore.Authentication.ApiKey
{
    internal class DelegateClaimResolver : IApiKeyAuthenticationClaimsResolver
    {
        private readonly string _schemaName;
        private readonly Func<string, Task<Claim[]>> _resolveFunc;

        public int Order => 0;

        public DelegateClaimResolver(string schemaName, Func<string, Task<Claim[]>> resolveFunc)
        {
            _schemaName = schemaName;
            _resolveFunc = resolveFunc;
        }

        public DelegateClaimResolver(string schemaName, Func<string, Claim[]> resolveFunc)
        {
            _resolveFunc = (s) => Task.FromResult(resolveFunc(s));
            _schemaName = schemaName;
        }

        public async Task<Claim[]> Resolve(string apiKey, ApiKeyAuthenticationOptions options) 
            => options.Scheme == _schemaName ? await _resolveFunc(apiKey) : null;
    }
}
