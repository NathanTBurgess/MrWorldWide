using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MrWorldwide.WebApi.Controllers.Public;

[Area("public")]
[ApiController]
[Route("[area]/[controller]")]
[AllowAnonymous]
public class PublicApiController : Controller
{
    
}