using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

public partial class AspNetUserSession
{
    [Key]
    public Guid AspNetUserSessionId { get; set; }

    [StringLength(450)]
    public string UserId { get; set; } = null!;

    [StringLength(1024)]
    public string? AuthToken { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpirationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastActiveDate { get; set; }

    public Guid? SiteId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserSessions")]
    public virtual AspNetUser User { get; set; } = null!;
}
