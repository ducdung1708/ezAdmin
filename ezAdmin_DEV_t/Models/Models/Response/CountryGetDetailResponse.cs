using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models.Response
{
    public class CountryGetDetailResponse
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public string? CurrencyName { get; set; }

        public string? CurrencyFormat { get; set; }

        public int? DecimalSeparator { get; set; }

        public string? Symbol { get; set; }
    }
}

