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
using MrWorldwide.WebApi.Infrastructure.ExceptionHandling;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            ArgumentException.ThrowIfNullOrEmpty(connectionString);
            var jwtOptions = _configuration.GetSection("Authorization:Jwt").Get<JwtOptions>();
            services.AddDbContext<MrWorldwideDbContext>(opt => opt.UseNpgsql(connectionString));
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters.ValidIssuer = jwtOptions.Issuer;
                    opt.TokenValidationParameters.ValidAudience = jwtOptions.Audience;
                    opt.TokenValidationParameters.IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
                })
                .AddJwtBearer("RefreshAuthentication", opt =>
                {
                    opt.TokenValidationParameters.ValidIssuer = jwtOptions.Issuer;
                    opt.TokenValidationParameters.ValidAudience = jwtOptions.Audience;
                    opt.TokenValidationParameters.IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
                    opt.TokenValidationParameters.ValidateLifetime = false;
                });
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<MrWorldwideDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthorization();
            services.AddHealthChecks();
            services.AddProblemDetails(x =>
            {
                x.MapExceptions();
                if (_environment.IsDevelopment())
                {
                    x.IncludeExceptionDetails();
                }
            });
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddCors();
            services.AddFeatures();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                endpoints.MapControllers();
            });
        }
    }
}