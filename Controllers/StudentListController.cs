using CitWebApi.DB;
using CitWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitWebApi.Controllers;

[ApiController]
[Route("/students/list")]
public class StudentListController : ControllerBase
{
    private readonly ApiContext Db;
    private readonly IAccessTokenValidator Validator;

    public StudentListController(ApiContext db, IAccessTokenValidator validator)
    {
        Db = db;
        Validator = validator;
    }
    
    [HttpGet]
    public IActionResult GetStudentList(String accessToken)
    {
        if (!Db.Staffs.Any(x => x.AccessToken == accessToken))
        {
            return BadRequest(new
            {
                Message = "Access token invalid"
            });
        }
        return Ok(new
        {
            Count = Db.Students.Count(),
            Data = Db.Students.ToList()
        });
    }
}