using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

public partial class Company
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(200)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TaxCode { get; set; }

    public int? Partner { get; set; }

    [StringLength(300)]
    public string? PartnerUrl { get; set; }

    [StringLength(100)]
    public string? Username { get; set; }

    [StringLength(100)]
    public string? Password { get; set; }

    [StringLength(100)]
    public string? Username2 { get; set; }

    [StringLength(100)]
    public string? Password2 { get; set; }

    [StringLength(300)]
    public string? PartnerUrl2 { get; set; }

    [StringLength(300)]
    public string? Avatar { get; set; }

    [StringLength(50)]
    public string CountryCode { get; set; } = null!;

    [StringLength(50)]
    public string Status { get; set; } = null!;

    [StringLength(50)]
    public string? PartnerConnectStatus { get; set; }

    [InverseProperty("Site")]
    public virtual ICollection<AspNetUserSite> AspNetUserSites { get; set; } = new List<AspNetUserSite>();
}
