using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;

namespace MrWorldwide.WebApi.Controllers;
[ApiController]
[AllowAnonymous]
[Route("[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class OpenApiController
{
    private readonly ISwaggerProvider _swaggerProvider;

    public OpenApiController(ISwaggerProvider swaggerProvider)
    {
        _swaggerProvider = swaggerProvider;
    }
    
    
}