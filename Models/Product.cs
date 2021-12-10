using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public class Product
    {
        public int ProductID {get; set;}

        // [StringLength(60, MinimumLength = 3)]
        // [Required]
        // public string Name {get; set;}

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate {get; set;}

        [StringLength(30)]
        [Required]
        public string Description {get; set;}

        [Range(1,100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price {get; set;}

        public List<UserProduct> UserProducts {get;set;}
    }

    public class UserProduct
    {
        public int ProductID {get;set;}
        public int userID {get; set;}
        public Product Product {get; set;}
        public User User{get;set;}
        
           
    }
}