using Marten;
using Microsoft.AspNetCore.Authentication.Cookies;
using MrWorldwide.WebApi.Infrastructure.ExceptionHandling;
using MrWorldwide.WebApi.Infrastructure.OpenIddict;
using Quartz;
using Weasel.Core;

namespace MrWorldwide.WebApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            ArgumentException.ThrowIfNullOrEmpty(connectionString);
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();
                options.UseSimpleTypeLoader();
                options.UseInMemoryStore();
            });
            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseMartenDataStores();
                })
                .AddClient(options =>
                {
                    options.AllowAuthorizationCodeFlow();
                    options.AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();
                    options.UseAspNetCore().EnableRedirectionEndpointPassthrough();
                    options.UseSystemNetHttp().SetProductInformation(typeof(Program).Assembly);
                    options.UseWebProviders()
                        .UseGoogle(google =>
                        {
                            google
                                .SetClientId("TODO")
                                .SetClientSecret("TODO")
                                .SetRedirectUri("callback/login/google");
                        });
                })
                .AddServer(options =>
                {
                    options
                        .SetAuthorizationEndpointUris("authorize")
                        .SetTokenEndpointUris("token");
                    options.AllowAuthorizationCodeFlow();
                    options.AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();
                    options.UseAspNetCore().EnableAuthorizationEndpointPassthrough();
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });
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
            services.AddMarten(options =>
            {
                options.Connection(connectionString);
                options.UseOpenIddict();
                options.AutoCreateSchemaObjects = AutoCreate.All;
            });
            services.AddHostedService<OpenIddictRegistrationWorker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IDocumentSession session)
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
                endpoints.MapAuthorization();
            });
        }
    }
}