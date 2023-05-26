using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KrushiWebAPI.Models;

[Table("recommendations")]
public partial class Recommendation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("rdate", TypeName = "date")]
    public DateTime? Rdate { get; set; }

    [Column("farmerid")]
    public int? Farmerid { get; set; }

    [Column("cropid")]
    public int? Cropid { get; set; }

    [Column("adminid")]
    public int? Adminid { get; set; }

    [Column("message")]
    public string? Message { get; set; }

    [ForeignKey("Adminid")]
    [InverseProperty("Recommendations")]
    public virtual Admin? Admin { get; set; } = null!;

    [ForeignKey("Cropid")]
    [InverseProperty("Recommendations")]
    public virtual Crop? Crop { get; set; } = null!;

    [ForeignKey("Farmerid")]
    [InverseProperty("Recommendations")]
    public virtual Farmer? Farmer { get; set; } = null!;
}
