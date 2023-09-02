using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CitWebApi.Controllers;

[ApiController]
[Route("/")]
[EnableCors("*")]
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
}