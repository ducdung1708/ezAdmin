﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityModels;

public partial class Log
{
    [Key]
    public int Id { get; set; }

    public string? Message { get; set; }

    public string? MessageTemplate { get; set; }

    public string? Level { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TimeStamp { get; set; }

    public string? Exception { get; set; }

    public string? Properties { get; set; }
}
