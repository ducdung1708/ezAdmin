using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models.Response
{
	public class CompanyGetListResponse
	{
        
        public Guid Id { get; set; }
      
        public string? Name { get; set; }
       
        public string? Address { get; set; }
        
        public string? Email { get; set; }

    }
}

