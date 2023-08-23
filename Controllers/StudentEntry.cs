using CitWebApi.DB;
using Microsoft.AspNetCore.Mvc;

namespace CitWebApi.Controllers;

[ApiController]
[Route("/student/entry")]
public class StudentEntryController : ControllerBase
{

    private readonly ApiContext Db;

    public StudentEntryController(ApiContext db) => Db = db;

    [HttpPost]
    public async Task<IActionResult> PostStudentEntry(int cardId)
    {

        if (!Db.Students.Any(x => x.CardId == cardId))
        {
            return NotFound(
                new
                {
                    Message = "failed"
                }
            );
        }

        var student = Db.Students.First(x => x.CardId == cardId);

        if (Db.StudentEntries.Any(x => x.Student!.CardId == cardId && x.OutTime == null))
        {
            var currStudent = Db.StudentEntries.First(
                x => x.Student!.CardId == cardId && x.OutTime == null
            );
            currStudent.OutTime = DateTime.Now;
            await Db.SaveChangesAsync();
            return Ok(
                new
                {
                    Message = "success"
                }
            );
        }

        var entry = new StudentEntryModel
        {
            Student = student,
            InTime = DateTime.Now,
            OutTime = null,
            Date = DateTime.Now
        };

        Db.StudentEntries.Add(entry);
        await Db.SaveChangesAsync();
        
        if (Db.StudentEntries.Count(x => x.Student!.CardId == cardId
                                         && x.Date.Date == DateTime.Now.Date) <= 1)
        {
            var newAttendance = new StudentAttendanceModel()
            {
                Student = Db.Students.First(x => x.CardId == cardId),
                Date = DateTime.Now,
                isOnDuty = false,
                isResting = false
            };

            Db.StudentAttendances.Add(newAttendance);
            await Db.SaveChangesAsync();
        }

        return Ok(
            new
            {
                Message = "success"
            }
        );
    }
}