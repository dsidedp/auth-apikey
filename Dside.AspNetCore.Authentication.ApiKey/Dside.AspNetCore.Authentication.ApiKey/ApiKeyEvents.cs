using System;
using System.Threading.Tasks;

namespace Dside.AspNetCore.Authentication.ApiKey
{
    public class ApiKeyEvents
    {

        /// <summary>
        /// Invoked if Authorization fails and results in a Forbidden response.
        /// </summary>
        public Func<ApiKeyForbiddenContext, Task> OnForbidden { get; set; } = context => Task.CompletedTask;

        /// <summary>
        /// Invoked before a challenge is sent back to the caller.
        /// </summary>
        public Func<ApiKeyChallengeContext, Task> OnChallenge { get; set; } = context => Task.CompletedTask;

        /// <summary>
        /// Invoked if Authorization fails and results in a Forbidden response
        /// </summary>
        public virtual Task Forbidden(ApiKeyForbiddenContext context) => OnForbidden(context);

        /// <summary>
        /// Invoked before a challenge is sent back to the caller.
        /// </summary>
        public virtual Task Challenge(ApiKeyChallengeContext context) => OnChallenge(context);
    }
}