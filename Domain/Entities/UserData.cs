using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartPlate.Domain.Enums;

namespace SmartPlate.Domain.Entities
{
    [Table("user_data")]
    public class UserData
    {
        internal Guid id;

        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("weight_kg", TypeName = "numeric(5,2)")]
        public decimal WeightKg { get; set; }

        [Column("height_cm", TypeName = "numeric(5,2)")]
        public decimal HeightCm { get; set; }

        [Column("age")]
        public int Age { get; set; }

        // Values: male, female
        [Column("biological_sex")]
        public BiologicalSex BiologicalSex { get; set; }

        [Column("workouts_per_week")]
        public int WorkoutsPerWeek { get; set; }

        // Values: strength, cardio, sports, mixed
        [Column("training_type")]
        public TrainingType TrainingType { get; set; }

        // Values: low, moderate, high
        [Column("training_intensity")]
        public TrainingIntensity TrainingIntensity { get; set; }

        // Values: sedentary, light, moderate, active, very_active
        [Column("daily_activity_level")]
        public DailyActivityLevel DailyActivityLevel { get; set; }

        // Values: maintain, gain_muscle, lose_fat, performance
        [Column("user_goal")]
        public UserGoals UserGoal { get; set; }

        // Scale: 1 a 5
        [Column("sleep_quality")]
        public int? SleepQuality { get; set; }

        // Scale: 1 a 5
        [Column("stress_level")]
        public int? StressLevel { get; set; }

        // Scale: 1 a 5
        [Column("routine_consistency")]
        public int? RoutineConsistency { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
    }
}
