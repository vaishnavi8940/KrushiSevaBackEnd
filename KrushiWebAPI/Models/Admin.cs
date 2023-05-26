using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KrushiWebAPI.Models;

[Table("admins")]
public partial class Admin
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string? Username { get; set; }

    [Column("password")]
    [StringLength(50)]
    public string? Password { get; set; }

    [InverseProperty("Admin")]
    public virtual ICollection<Recommendation>? Recommendations { get; set; } = new List<Recommendation>();
}
