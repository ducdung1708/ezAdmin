using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models.Request
{
    public class CountryGetListRequest
    {
        public string? NameOrCode { get; set; }        

        [Required]
        public int? PageSize { get; set; }

        [Required]
        public int? PageIndex { get; set; }

    }
}

