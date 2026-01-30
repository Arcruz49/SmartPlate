using System.ComponentModel.DataAnnotations;

namespace SmartPlate.Domain.Entities;

public class Users
{
    [Key]
    public Guid id { get; set; }
    public string name { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;

}

