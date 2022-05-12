using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QuizManager.Api.Auth;
using QuizManager.Api.Filters;
using QuizManager.Api.Profiles;
using QuizManager.Api.Utils;
using QuizManager.Core.BusinessLogic;
using QuizManager.Data;
using QuizManager.Data.Repositories;
using QuizManager.Data.UnitOfWork;

namespace QuizManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add<ExceptionFilter>());
            services.AddAutoMapper(typeof(Startup), typeof(ApiToCoreProfile), typeof(CoreToDataProfile));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuizManager.Api", Version = "v1" });
            });
            services.AddDbContext<QuizContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Database"));
            });


            services.SetupAuthentication(Configuration["Auth0:Audience"], 
                $"https://{Configuration["Auth0:Domain"]}/");

            services.SetupDependencyInjectionContainer();

            services.SetupCors(Configuration["ClientOrigin"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuizManager.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(Cors.CorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
