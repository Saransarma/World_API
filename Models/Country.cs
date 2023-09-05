﻿using System.ComponentModel.DataAnnotations;

namespace World_Api.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(10)]
        public string MobileCode { get; set; }
    }
}
