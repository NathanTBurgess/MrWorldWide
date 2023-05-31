using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MrWorldwide.WebApi.Data;
using MrWorldwide.WebApi.Data.Entities;
using MrWorldwide.WebApi.Features;
using MrWorldwide.WebApi.Features.Authorization;
using MrWorldwide.WebApi.Infrastructure.Configuration;
using MrWorldwide.WebApi.Infrastructure.WebApi.Authentication;
using MrWorldwide.WebApi.Infrastructure.WebApi.ExceptionHandling;
using MrWorldwide.WebApi.Infrastructure.WebApi.Routing;

namespace MrWorldwide.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            ArgumentException.ThrowIfNullOrEmpty(connectionString);
            services.AddDbContext<MrWorldwideDbContext>(opt => opt.UseNpgsql(connectionString));
            services.AddAuthenticationServices();
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<MrWorldwideDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthorization();
            services.AddHealthChecks();
            services.AddProblemDetails(x =>
            {
                x.MapExceptions();
                x.IncludeExceptionDetails();
            });
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddCors();
            services.AddFeatures();
        }

        public void Configure(IApplicationBuilder app, IOptions<CorsHostingOptions> corsOptions)
        {
            app.UseExceptionHandler();
            if (_environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(x =>
            {
                x.WithOrigins(corsOptions.Value.AllowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapBrewCoffee();
                endpoints.MapControllers();
            });
        }
    }
}