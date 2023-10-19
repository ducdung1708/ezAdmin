using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

[Index("UserId", "SiteId", Name = "UC_AspNetUserSite_UserIdSiteId", IsUnique = true)]
public partial class AspNetUserSite
{
    [Key]
    public Guid AspNetUserSiteId { get; set; }

    public string UserId { get; set; } = null!;

    [Column("AspNetGroupUserID")]
    public Guid AspNetGroupUserId { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? InviteConfirmDate { get; set; }

    [StringLength(450)]
    public string InviteByUserId { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime IntiveDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpireDate { get; set; }

    [StringLength(450)]
    public string? LastModifiedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastModifiedDate { get; set; }

    public Guid? SiteId { get; set; }

    [ForeignKey("AspNetGroupUserId")]
    [InverseProperty("AspNetUserSites")]
    public virtual AspNetGroupUser AspNetGroupUser { get; set; } = null!;

    [ForeignKey("SiteId")]
    [InverseProperty("AspNetUserSites")]
    public virtual Company? Site { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserSites")]
    public virtual AspNetUser User { get; set; } = null!;
}
