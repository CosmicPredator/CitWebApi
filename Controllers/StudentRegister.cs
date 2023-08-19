using CitWebApi.Controllers.Models;
using CitWebApi.DB;
using CitWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitWebApi.Controllers;

[ApiController]
[Route("/student/register")]
public class StudentRegisterController : ControllerBase
{
    private readonly ApiContext Db;
    private readonly IAccessTokenValidator Validator;

    public StudentRegisterController(ApiContext db, IAccessTokenValidator validator)
    {
        Db = db;
        Validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterStudent(StudentRegisterModel model)
    {
        if (!Db.Staffs.Any(x => x.AccessToken == model.AccessToken))
        {
            return BadRequest(
                new
                {
                    Message = "Access token invalid."
                }
            );
        }

        if (Db.Students.Any(x => x.CardId == model.CardId))
        {
            return BadRequest(
                new
                {
                    Message = "Student already registered."
                }
            );
        }

        var student = new StudentModel
        {
            Name = model.Name,
            CardId = model.CardId,
            Department = model.Department,
            Year = model.Year
        };

        Db.Students.Add(student);
        await Db.SaveChangesAsync();
        return Ok(
            new
            {
                Message = "Student registered successfully",
                Data = model
            }
        );
    }
}

[ApiController]
[Route("/student/edit")]
public class StudentEditController : ControllerBase
{
    private readonly ApiContext Db;
    private readonly IAccessTokenValidator Validator;

    public StudentEditController(ApiContext db, IAccessTokenValidator validator)
    {
        Db = db;
        Validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> EditStudent(StudentRegisterModel model)
    {

        if (Validator.Validate(model.AccessToken!))
        {
            return BadRequest(
                new
                {
                    Message = "Access token invalid."
                }
            );
        }

        if (!Db.Students.Any(x => x.CardId == model.CardId))
        {
            return NotFound(
                new
                {
                    Message = "Student not found."
                }
            );
        }

        var student = Db.Students.First(x => x.CardId == model.CardId);

        student.Name = model.Name;
        student.Department = model.Department;
        student.Year = model.Year;

        await Db.SaveChangesAsync();

        return Ok(
            new
            {
                Message = "Student info edited successfully."
            }
        );
    }
}

[ApiController]
[Route("/student")]
public class StudentController : ControllerBase
{
    private readonly ApiContext Db;
    private readonly IAccessTokenValidator Validator;

    public StudentController(ApiContext db, IAccessTokenValidator validator)
    {
        Db = db;
        Validator = validator;
    }

    [HttpGet]
    public IActionResult GetStudent(int cardId, string accessToken)
    {
        if (Validator.Validate(accessToken))
        {
            return BadRequest(new
            {
                Message = "Access token invalid."
            });
        }

        if (!Db.Students.Any(x => x.CardId == cardId))
        {
            return BadRequest(new
            {
                Message = "Student with the card id not found."
            });
        }
        
        return Ok(new
        {
            Data = Db.Students.First(x => x.CardId == cardId)
        });
    }
}