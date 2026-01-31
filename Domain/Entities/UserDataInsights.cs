using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPlate.Domain.Entities;

public class UserDataInsights
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_data_id")]
    [Required]
    public Guid UserDataId { get; set; }

    [Column("target_calories", TypeName = "numeric(8,2)")]
    public decimal? TargetCalories { get; set; }

    [Column("protein_target_g", TypeName = "numeric(8,2)")]
    public decimal? ProteinTargetG { get; set; }

    [Column("carbs_target_g", TypeName = "numeric(8,2)")]
    public decimal? CarbsTargetG { get; set; }

    [Column("fat_target_g", TypeName = "numeric(8,2)")]
    public decimal? FatTargetG { get; set; }

    [Column("sleep_hours_target", TypeName = "numeric(4,2)")]
    public decimal? SleepHoursTarget { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey(nameof(UserDataId))]
    public UserData UserData { get; set; } = null!;

}

