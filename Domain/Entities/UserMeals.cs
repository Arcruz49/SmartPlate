using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPlate.Domain.Entities;

[Table("user_meals")]
public class UserMeal
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Required]
    [Column("meal_name")]
    public string MealName { get; set; } = string.Empty;
    
    [Column("meal_description")]
    public string MealDescription { get; set; } = string.Empty;

    [Required]
    [Column("meal_date", TypeName = "date")]
    public DateTime MealDate { get; set; }
    [Required]
    [Column("meal_time", TypeName = "time")]
    public TimeSpan MealTime { get; set; }

    [Required]
    [Column("calories")]
    public int Calories { get; set; }

    [Required]
    [Column("protein_g")]
    public decimal ProteinG { get; set; }

    [Required]
    [Column("carbs_g")]
    public decimal CarbsG { get; set; }

    [Required]
    [Column("fat_g")]
    public decimal FatG { get; set; }

    [Required]
    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}
