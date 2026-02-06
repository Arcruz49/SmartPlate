using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPlate.Domain.Entities;

[Table("user_body_metrics")]
public class UserBodyMetrics
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("metric_date")]
    public DateTime? MetricDate { get; set; } = DateTime.Now;

    [Column("weight_kg")]
    public decimal? WeightKg { get; set; }

    [Column("body_fat_percent")]
    public decimal? BodyFatPercent { get; set; }

    [Column("muscle_mass_kg")]
    public decimal? MuscleMassKg { get; set; }

    [Column("note")]
    public string Note { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}

