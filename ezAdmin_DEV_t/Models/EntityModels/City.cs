using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

public partial class City
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(300)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string? AirportCode { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? PhoneCode { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? ExtraCode { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? PostalCode { get; set; }

    public double? Latitude { get; set; }

    public double? Longtitude { get; set; }

    public int? SortIndex { get; set; }

    public int? Active { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public int? CountryId { get; set; }
}
