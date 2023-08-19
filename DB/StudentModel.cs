using CitWebApi.Enums;

namespace CitWebApi.DB;

public record StudentModel
{
    public Guid Id {get; set;}
    public string? Name {get; set;}
    public Department Department {get; set;}
    public int Year {get; set;}
    public long CardId {get; set;}
}