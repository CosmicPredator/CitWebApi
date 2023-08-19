using Microsoft.AspNetCore.Mvc;

namespace CitWebApi.Controllers;

[ApiController]
[Route("/")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult GetHome()
    {
        return Ok(
            new
            {
                Status = Response.StatusCode,
                Message = "Welcome to the API"
            }
        );
    }

    [HttpPost]
    public IActionResult PostHome()
    {
        return Ok(
            new 
            {
                Status = Response.StatusCode,
                Message = "Welcome to the API"
            }
        );
    }
}