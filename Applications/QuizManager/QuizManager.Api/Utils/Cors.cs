using Microsoft.Extensions.DependencyInjection;

namespace QuizManager.Api.Utils
{
    internal static class Cors
    {
        internal const string CorsPolicy = "_CorsPolicy";

        internal static void SetupCors(this IServiceCollection services, string allowedOrigin)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    CorsPolicy,
                    policy =>
                    {
                        policy.WithOrigins(allowedOrigin)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }
    }
}
