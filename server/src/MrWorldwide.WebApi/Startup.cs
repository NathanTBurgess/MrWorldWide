using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MrWorldwide.WebApi.Infrastructure.Authentication;
using MrWorldwide.WebApi.Infrastructure.EntityFramework;
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
            services.AddOptions<AuthorizationOptions>().BindConfiguration("Authorization");
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            ArgumentException.ThrowIfNullOrEmpty(connectionString);
            services.AddDbContext<MrWorldwideDbContext>(opt => opt.UseNpgsql(connectionString));
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
                endpoints.MapGoogleLogin();
            });
        }
    }
}