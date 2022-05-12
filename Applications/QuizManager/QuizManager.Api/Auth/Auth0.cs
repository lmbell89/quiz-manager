using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace QuizManager.Api.Auth
{
    internal static class Auth0
    {
        internal static void SetupAuthentication(this IServiceCollection services, string audience, string domain)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = domain;
                    options.Audience = audience;
                    // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`.
                    // Map it to a different claim by setting the NameClaimType below.
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });

            services.AddAuthorization(options =>
            {
                AddPolicyRequirement(Permissions.CreateQuiz, domain, options);
                AddPolicyRequirement(Permissions.ReadQuiz, domain, options);
                AddPolicyRequirement(Permissions.UpdateQuiz, domain, options);
                AddPolicyRequirement(Permissions.DeleteQuiz, domain, options);
                AddPolicyRequirement(Permissions.ReadAnswer, domain, options);
                AddPolicyRequirement(Permissions.CreateQuestion, domain, options);
                AddPolicyRequirement(Permissions.ReadQuestion, domain, options);
                AddPolicyRequirement(Permissions.UpdateQuestion, domain, options);
                AddPolicyRequirement(Permissions.DeleteQuestion, domain, options);
            });
        }

        private static void AddPolicyRequirement(string name, string domain, AuthorizationOptions options)
        {
            options.AddPolicy(name, policy => policy.Requirements.Add(new HasScopeRequirement(name, domain)));
        }
    }
}
