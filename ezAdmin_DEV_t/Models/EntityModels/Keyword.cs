using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

[Index("KeywordCode", Name = "UC__KeywordCode", IsUnique = true)]
public partial class Keyword
{
    [Key]
    [Column("KeywordID")]
    public Guid KeywordId { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string KeywordCode { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [InverseProperty("Keyword")]
    public virtual ICollection<LanguageKeyword> LanguageKeywords { get; set; } = new List<LanguageKeyword>();
}
