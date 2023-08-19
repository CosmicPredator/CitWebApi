using CitWebApi.DB;
using CitWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitWebApi.Controllers;

[ApiController]
[Route("/students/entry/list")]
public class StudentEntryList : ControllerBase
{
    private readonly ApiContext Db;
    private readonly ILogger<StudentEntryList> Logger;
    private readonly IAccessTokenValidator AccessTokenValidator;

    public StudentEntryList(ApiContext db, ILogger<StudentEntryList> logger, IAccessTokenValidator validator)
    {
        Db = db;
        Logger = logger;
        AccessTokenValidator = validator;
    }

    [HttpGet]
    public IActionResult GetStudentEntries(string accessToken, DateTime? time = null)
    {
        if (AccessTokenValidator.Validate(accessToken))
        {
            return BadRequest(new
            {
                Message = "Access token invalid."
            });
        }

        if (time == null)
        {
            return Ok(new
            {
                Data = Db.StudentEntries.Include(x =>
                    x.Student).Where(y => y.Date.Date == DateTime.Now.Date)
            });
        }

        return Ok(new
        {
            Data = Db.StudentEntries.Include(x => x.Student)
                .Where(y =>
                    y.Date.Date == time.Value.Date).ToList()
        });
    }
}