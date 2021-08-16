using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace movieDemo.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {get; set;}
        [Required]
        public string CategoryName {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
        
        List<Genre> MoviesInCat{get; set;}

    
    }
}