using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

public partial class Language
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string LanguageCode { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? KeywordTranslate { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }

    public bool? Active { get; set; }

    public int? DisplayOrder { get; set; }

    [InverseProperty("LanguageCodeNavigation")]
    public virtual ICollection<LanguageKeyword> LanguageKeywords { get; set; } = new List<LanguageKeyword>();
}
