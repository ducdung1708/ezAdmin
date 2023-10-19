using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

[Table("MenuDefine")]
public partial class MenuDefine
{
    [Key]
    [StringLength(100)]
    [Unicode(false)]
    public string MenuCode { get; set; } = null!;

    [StringLength(255)]
    public string? Keyword { get; set; }

    public string? MenuDescription { get; set; }

    [StringLength(255)]
    public string? MenuLink { get; set; }

    [StringLength(255)]
    public string? IconName { get; set; }

    public int? MenuOrder { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? MenuParentCode { get; set; }

    public bool? IsActive { get; set; }

    [StringLength(500)]
    public string? RouteName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedByUserId { get; set; }

    /// <summary>
    /// Miền giá trị: PAGE, ACTION
    /// </summary>
    [StringLength(100)]
    public string? Type { get; set; }

    [StringLength(255)]
    public string? ObjectCode { get; set; }

    public bool? IsAdminView { get; set; }

    public string? WhenAction { get; set; }

    public bool? IsPublic { get; set; }

    [InverseProperty("MenuCodeNavigation")]
    public virtual ICollection<MenuDefineAspNetGroupUser> MenuDefineAspNetGroupUsers { get; set; } = new List<MenuDefineAspNetGroupUser>();
}
