using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

[Index("Email", Name = "UC_AspNetUserEmail", IsUnique = true)]
public partial class AspNetUser
{
    [Key]
    public string Id { get; set; } = null!;

    public int AccessFailedCount { get; set; }

    public string? ConcurrencyStamp { get; set; }

    [StringLength(256)]
    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool LockoutEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    [StringLength(256)]
    public string? NormalizedEmail { get; set; }

    [StringLength(256)]
    public string? NormalizedUserName { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public string? SecurityStamp { get; set; }

    public bool TwoFactorEnabled { get; set; }

    [StringLength(256)]
    public string? UserName { get; set; }

    [StringLength(256)]
    public string? FullName { get; set; }

    [StringLength(256)]
    public string? Avatar { get; set; }

    [Column("CountryID")]
    public int? CountryId { get; set; }

    public bool IsActive { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? LanguageCode { get; set; }

    public Guid? LastSiteId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserSession> AspNetUserSessions { get; set; } = new List<AspNetUserSession>();

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserSite> AspNetUserSites { get; set; } = new List<AspNetUserSite>();
}
