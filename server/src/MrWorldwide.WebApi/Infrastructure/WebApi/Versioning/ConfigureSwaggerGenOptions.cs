using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MrWorldwide.WebApi.Infrastructure.WebApi.Versioning;

public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        options.OperationFilter<SwaggerDefaultValues>();
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "Mr Worldwide API",
            Version = description.ApiVersion.ToString(),
            Description = "Programmatic access to globetrotting data",
            Contact = new OpenApiContact
            {
                Email = "fuqua.matt@gmail.com",
                Name = "Matt Fuqua",
                Url = new Uri("https://mattfuqua.dev")
            }
        };
        return info;
    }
}