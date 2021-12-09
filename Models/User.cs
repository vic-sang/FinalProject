using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public class User
    {
        public int userID {get; set;}

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name {get; set;}

        
        public List<UserProduct> UserProducts{get;set;}

        

       // public List<Review> Reviews {get; set;} // Navigation Property. One movie can have many reviews        
    }
}