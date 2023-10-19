using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
    public class CountryUpdateRequest
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Code { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string? CurrencyName { get; set; }

        [MaxLength(50)]
        public string? CurrencyFormat { get; set; }

        public int? DecimalSeparator { get; set; }

        [MaxLength(30)]
        public string? Symbol { get; set; }
    }
}

