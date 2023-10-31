using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

[Index("KeywordId", "LanguageCode", Name = "UC__KeywordID_LanguageCode", IsUnique = true)]
public partial class LanguageKeyword
{
    [Key]
    [Column("TranslateKeywordID")]
    public Guid TranslateKeywordId { get; set; }

    public string TranslateKeyword { get; set; } = null!;

    [Column("KeywordID")]
    public Guid KeywordId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string LanguageCode { get; set; } = null!;

    [ForeignKey("KeywordId")]
    [InverseProperty("LanguageKeywords")]
    public virtual Keyword Keyword { get; set; } = null!;

    [ForeignKey("LanguageCode")]
    [InverseProperty("LanguageKeywords")]
    public virtual Language LanguageCodeNavigation { get; set; } = null!;
}
