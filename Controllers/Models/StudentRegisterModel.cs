using CitWebApi.Enums;

namespace CitWebApi.Controllers.Models;

public record StudentRegisterModel
{
    public string? Name {get; set;}
    public Department Department {get; set;}
    public int Year {get; set;}
    public long CardId {get; set;}
    public string? AccessToken {get; set;}
}