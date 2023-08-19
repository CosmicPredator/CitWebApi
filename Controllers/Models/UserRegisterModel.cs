using System.ComponentModel.DataAnnotations;

namespace CitWebApi.Controllers.Models;

public record UserRegisterModel
{
    [Required, EmailAddress]
    public string? Email {get; set;} = string.Empty;
    [Required, MinLength(8)]
    public string? Password {get; set;} = string.Empty;
}