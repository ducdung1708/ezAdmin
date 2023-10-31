using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

public partial class Country
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string Code { get; set; } = null!;

    [StringLength(250)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? CurrencyName { get; set; }

    [StringLength(50)]
    public string? CurrencyFormat { get; set; }

    public int? DecimalSeparator { get; set; }

    [StringLength(30)]
    public string? Symbol { get; set; }
}
