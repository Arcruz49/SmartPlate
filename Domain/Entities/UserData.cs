using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartPlate.Domain.Enums;

namespace SmartPlate.Domain.Entities;

public class UserData
{
    [Key]
    public Guid id { get; set; }
    public Guid user_id { get; set; }
    public decimal weight_kg { get; set; }
    public decimal height_cm { get; set; }
    public int age { get; set; }
    public BiologicalSex biological_sex { get; set; }
    public int workouts_per_week { get; set; }
    public TrainingType training_type { get; set; }
    public TrainingIntensity training_intensity { get; set; }
    public DailyActivityLevel daily_activity_level { get; set; }
    public UserGoals user_goal { get; set; }
    public int? sleep_quality { get; set; }
    public int? stress_level { get; set; }
    public int? routine_consistency { get; set; }
    public DateTime created_at { get; set; }
    public DateTime? updated_at { get; set; }

    [ForeignKey(nameof(user_id))]
    public User User { get; set; } = null!;

}
