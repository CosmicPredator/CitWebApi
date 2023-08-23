using CitWebApi.DB;
using Microsoft.AspNetCore.Mvc;

namespace CitWebApi.Controllers;

[ApiController]
public class AbsentReasonController : ControllerBase
{
    private readonly ApiContext Db;

    private AbsentReasonController(ApiContext db) => Db = db;
    
    [HttpGet("/absent/reason/list")]
    public IActionResult GetAbsentiesList(DateTime? date = null)
    {
        if (date == null)
        {
            var absentees = Db
                .AbsentReasons
                .Where(x => x.Date.Date == DateTime.Now.Date)
                .ToList();
            return Ok(new
            {
                absentees.Count,
                Data = absentees
            });
        }
        var absenteesSpecific = Db
            .AbsentReasons
            .Where(x => x.Date.Date == date.Value.Date)
            .ToList();
        return Ok(new
        {
            absenteesSpecific.Count,
            Data = absenteesSpecific
        });
    }
}