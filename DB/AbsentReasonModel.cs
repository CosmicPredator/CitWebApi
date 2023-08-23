namespace CitWebApi.DB;

public record AbsentReasonModel
{
    public Guid Id { get; set; }
    public StudentModel? Student { get; set; }
    public DateTime Date { get; set; }
    public String? Reason { get; set; }
}