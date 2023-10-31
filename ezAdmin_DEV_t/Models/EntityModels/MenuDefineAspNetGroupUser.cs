using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

[Index("MenuCode", "AspNetGroupUserId", Name = "UC_MenuDefineGroupUsers_MenuCodeGroupUserId", IsUnique = true)]
public partial class MenuDefineAspNetGroupUser
{
    [Key]
    [Column("MenuDefineAspNetGroupUserID")]
    public Guid MenuDefineAspNetGroupUserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string MenuCode { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedByUserId { get; set; }

    public string? WhenAction { get; set; }

    [Column("AspNetGroupUserID")]
    public Guid AspNetGroupUserId { get; set; }

    [ForeignKey("AspNetGroupUserId")]
    [InverseProperty("MenuDefineAspNetGroupUsers")]
    public virtual AspNetGroupUser AspNetGroupUser { get; set; } = null!;

    [ForeignKey("MenuCode")]
    [InverseProperty("MenuDefineAspNetGroupUsers")]
    public virtual MenuDefine MenuCodeNavigation { get; set; } = null!;
}
