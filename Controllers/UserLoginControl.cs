using CitWebApi.Controllers.Models;
using CitWebApi.DB;
using CitWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitWebApi.Controllers;

[ApiController]
[Route("/login")]
public class LoginController : ControllerBase
{
    private readonly ApiContext Db;
    private readonly IEncryptor Encryptor;

    public LoginController(ApiContext db, IEncryptor encryptor) 
    {
        Db = db;
        Encryptor = encryptor;
    }

    [HttpPost]
    public IActionResult LoginUser(UserRegisterModel model)
    {
        if (!Db.Staffs.Any(x => x.Email == model.Email))
        {
            return NotFound(
                new 
                {
                    Message = "Staff not found with the email."
                }
            );
        }

        var student = Db.Staffs.Where(
            x => x.Email == model.Email
        ).First();

        if (Encryptor.verifyPasswordHash(model.Password!, student.PasswordHash!, student.PasswordSalt!))
        {
            return Ok(
                new
                {
                    student.AccessToken
                }
            );
        }

        return BadRequest(
            new
            {
                Message = "Email or password error."
            }
        );
        
    }
}

[ApiController]
[Route("/register")]
public class RegisterController : ControllerBase
{
    private readonly ApiContext Db;
    private readonly IEncryptor Encryptor;
    public RegisterController(ApiContext db, IEncryptor encryptor) 
    {
        Db = db;
        Encryptor = encryptor;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(UserRegisterModel model)
    {
        if (Db.Staffs.Any(x => x.Email == model.Email))
        {
            return BadRequest(
                new
                {
                    Message = "Staff already exists in the database."
                }
            );
        }

        Encryptor.generatePasswordHash(model.Password!, out byte[] passwordHash, out byte[] passwordSalt);

        var staff = new StaffModel
        {
            Email = model.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            AccessToken = Encryptor.generateAccessToken()
        };

        Db.Staffs.Add(staff);
        await Db.SaveChangesAsync();

        return Ok(
            new
            {
                Message = "Staff added successfully."
            }
        );
    }
}