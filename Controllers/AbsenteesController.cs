using CitWebApi.DB;
using CitWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitWebApi.Controllers;

[ApiController]
public class AbsenteesController : ControllerBase
{
    private readonly ApiContext Db;
    private readonly IAccessTokenValidator Validator;

    public AbsenteesController(ApiContext db, IAccessTokenValidator validator)
    {
        Db = db;
        Validator = validator;
    }

    [HttpGet("/absent/list")]
    public IActionResult GetAbsentResult(DateTime? date = null)
    {
        if (date == null)
        {
            var absenteesToday = Db.Students
                .Where(x => 
                    !Db.StudentAttendances.Any(
                        y => y.Student.CardId == x.CardId &&
                             y.Date.Date == DateTime.Now.Date)).ToList();
            return Ok(new
            {
                absenteesToday.Count,
                Data = absenteesToday
            });
        }
        var absenteesSelect = Db.Students
            .Where(x => 
                !Db.StudentAttendances.Any(
                    y => y.Student.CardId == x.CardId &&
                         y.Date.Date == date.Value.Date)).ToList();
        return Ok(new
        {
            absenteesSelect.Count,
            Data = absenteesSelect
        });
    }
}