using Marten;
using MrWorldwide.WebApi.Infrastructure.ExceptionHandling;
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
                options.AutoCreateSchemaObjects = AutoCreate.All;
            });
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
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}

public class TestObj
{
    public int Id { get; set; }
    public string Message { get; set; }
}