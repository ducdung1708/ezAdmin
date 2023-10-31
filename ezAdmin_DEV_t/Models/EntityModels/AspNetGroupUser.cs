using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

[Table("AspNetGroupUser")]
public partial class AspNetGroupUser
{
    [Key]
    [Column("AspNetGroupUserID")]
    public Guid AspNetGroupUserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string GroupUserCode { get; set; } = null!;

    [StringLength(200)]
    public string Keyword { get; set; } = null!;

    public string? GroupUserDescription { get; set; }

    public Guid? SiteId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedByUserId { get; set; }

    public bool? IsSystem { get; set; }

    public int? RoleLevel { get; set; }

    [InverseProperty("AspNetGroupUser")]
    public virtual ICollection<AspNetUserSite> AspNetUserSites { get; set; } = new List<AspNetUserSite>();

    [InverseProperty("AspNetGroupUser")]
    public virtual ICollection<MenuDefineAspNetGroupUser> MenuDefineAspNetGroupUsers { get; set; } = new List<MenuDefineAspNetGroupUser>();
}
