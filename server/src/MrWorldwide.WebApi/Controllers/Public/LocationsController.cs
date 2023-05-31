using Microsoft.AspNetCore.Mvc;

namespace MrWorldwide.WebApi.Controllers.Public;

public class LocationsController : PublicApiController
{
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentLocation()
    {
        throw new NotImplementedException();
    }
    [HttpGet]
    public async Task<IActionResult> GetLocationHistory()
    {
        throw new NotImplementedException();
    }
}