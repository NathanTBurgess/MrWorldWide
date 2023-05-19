using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MrWorldwide.WebApi.Controllers;

[ApiController]
[Area("api")]
[Route("[area]/[controller]")]
[Authorize]
public abstract class ApiController : Controller
{
    
}