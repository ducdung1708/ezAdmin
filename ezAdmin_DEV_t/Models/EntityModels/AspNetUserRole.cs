using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

[PrimaryKey("UserId", "RoleId")]
public partial class AspNetUserRole
{
    [Key]
    public string UserId { get; set; } = null!;

    [Key]
    public string RoleId { get; set; } = null!;
}
