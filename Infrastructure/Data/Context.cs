using Microsoft.EntityFrameworkCore;
using SmartPlate.Domain.Entities;
using SmartPlate.Domain.Enums;

namespace SmartPlate.Infrastructure.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    public DbSet<User> users { get; set; } = null!;
    public DbSet<UserData> user_data { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<BiologicalSex>();
        modelBuilder.HasPostgresEnum<TrainingType>();
        modelBuilder.HasPostgresEnum<TrainingIntensity>();
        modelBuilder.HasPostgresEnum<DailyActivityLevel>();
        modelBuilder.HasPostgresEnum<UserGoals>();

        base.OnModelCreating(modelBuilder);
    }
}
