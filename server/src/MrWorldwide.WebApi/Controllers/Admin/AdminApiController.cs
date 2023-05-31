using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MrWorldwide.WebApi.Controllers.Admin;

[Area("public")]
[ApiController]
[Route("[area]/[controller]")]
[AllowAnonymous]
public class AdminApiController : Controller
{
    
}