namespace CitWebApi.DB;

public record StudentAttendanceModel
{
    public Guid Id { get; set; }
    public StudentModel Student { get; set; }
    public DateTime Date { get; set; }
    public bool isOnDuty { get; set; } = false;
    public bool isResting { get; set; } = false;
}