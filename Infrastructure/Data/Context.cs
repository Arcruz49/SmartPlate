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

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserData> UserData { get; set; } = null!;
    public DbSet<UserDataInsights> UserDataInsights { get; set; } = null!;
    public DbSet<UserMeal> UserMeal { get; set; } = null!;
    public DbSet<UserBodyMetrics> UserBodyMetrics { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<BiologicalSex>();
        modelBuilder.HasPostgresEnum<TrainingType>();
        modelBuilder.HasPostgresEnum<TrainingIntensity>();
        modelBuilder.HasPostgresEnum<DailyActivityLevel>();
        modelBuilder.HasPostgresEnum<UserGoals>();

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<UserData>().ToTable("user_data");
        modelBuilder.Entity<UserDataInsights>().ToTable("user_data_insights");
        modelBuilder.Entity<UserMeal>().ToTable("user_meals");
        modelBuilder.Entity<UserBodyMetrics>().ToTable("user_body_metrics");

        base.OnModelCreating(modelBuilder);
    }
}
