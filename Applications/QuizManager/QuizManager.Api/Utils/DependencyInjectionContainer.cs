using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using QuizManager.Api.Auth;
using QuizManager.Core.BusinessLogic;
using QuizManager.Data.Repositories;
using QuizManager.Data.UnitOfWork;

namespace QuizManager.Api.Utils
{
    internal static class DependencyInjectionContainer
    {
        internal static void SetupDependencyInjectionContainer(this IServiceCollection services)
        {
            services.AddScoped<IQuestionCoordinator, QuestionCoordinator>();
            services.AddScoped<IQuizCoordinator, QuizCoordinator>();

            services.AddScoped<IGenericRepository<Data.Entities.Quiz>, QuizRepository>();
            services.AddScoped<IGenericRepository<Data.Entities.Question>, QuestionRepository>();
            services.AddScoped<IGenericRepository<Data.Entities.Answer>, GenericRepository<Data.Entities.Answer>>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }
    }
}
