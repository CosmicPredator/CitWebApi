using Microsoft.EntityFrameworkCore;

namespace CitWebApi.DB;


public class ApiContext : DbContext
{

    private readonly string _dbPath = "users.db";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }

    public DbSet<StaffModel> Staffs { get; set; }
    public DbSet<StudentModel> Students {get; set;}
    public DbSet<StudentEntryModel> StudentEntries {get; set;}
    public DbSet<StudentAttendanceModel> StudentAttendances { get; set; }
    public DbSet<AbsentReasonModel> AbsentReasons { get; set; }
}