namespace CitWebApi.DB;

public record StudentEntryModel
{
    public DateTime? InTime{get; set;}
    public DateTime? OutTime{get; set;}
    public StudentModel? Student{get; set;}
    public Guid Id {get; set;}
    public DateTime Date {get; set;}
}