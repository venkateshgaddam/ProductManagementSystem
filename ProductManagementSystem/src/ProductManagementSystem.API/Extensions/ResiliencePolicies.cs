using Polly;
using Polly.Registry;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;

namespace ProductManagementSystem.CommonAPI.Extensions
{
    public static class ResiliencePolicies
    {

        private static AsyncRetryPolicy _asyncRetryPolicy;

        private static int _numberOfRetries;

        public static IServiceCollection ConfigurePollyPolicies(this IServiceCollection services)
        {
            PolicyRegistry policyRegistry = new()
            {
                { "SQLAsyncResilienceStrategy", GetAsyncRetryPolicy() },
                { "SQLResilienceStrategy", GetRetryPolicy() }
            };
            services.AddSingleton<IReadOnlyPolicyRegistry<string>>(policyRegistry);
            return services;
        }


        private static AsyncPolicyWrap GetAsyncRetryPolicy()
        {
            _numberOfRetries = 1;
            _asyncRetryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(_numberOfRetries, retryAttempts =>
            {
                var timeToWait = TimeSpan.FromSeconds(Math.Pow(_numberOfRetries, retryAttempts));
                Console.WriteLine($"Waiting {timeToWait.TotalSeconds} seconds");
                return timeToWait;
            });
            var timeoutPolicy = Policy.TimeoutAsync
                                (TimeSpan.FromSeconds(30),
                                TimeoutStrategy.Pessimistic);
            return Policy.WrapAsync(_asyncRetryPolicy, timeoutPolicy);
        }


        private static PolicyWrap GetRetryPolicy()
        {
            var timeoutPolicy = Policy.Timeout
                                (TimeSpan.FromSeconds(30),
                                TimeoutStrategy.Pessimistic);
            var policy = Policy.Handle<Exception>().WaitAndRetry(3, retryAttempts =>
            {
                var timeToWait = TimeSpan.FromSeconds(Math.Pow(_numberOfRetries, retryAttempts));
                Console.WriteLine($"Waiting {timeToWait.TotalSeconds} seconds");
                return timeToWait;
            });
            return Policy.Wrap(policy, timeoutPolicy);
        }
    }
}
