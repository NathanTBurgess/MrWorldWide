using Microsoft.AspNetCore.Mvc;
using MrWorldwide.WebApi.Infrastructure.WebApi.Exceptions;

namespace MrWorldwide.WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class CoffeeController: Controller
{
    [HttpGet]
    public IActionResult BrewCoffee()
    {
        throw new ServerIsTeapotException();
    }
}