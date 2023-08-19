namespace CitWebApi.DB;

public record StaffModel
{
    public Guid Id {get; set;}
    public byte[]? PasswordHash {get; set;}
    public byte[]? PasswordSalt {get; set;}
    public string? AccessToken {get; set;}
    public string? Email {get; set;}
}