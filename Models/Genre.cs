using System;
using System.ComponentModel.DataAnnotations;

namespace movieDemo.Models
{
    public class Genre
    {
        [Key]
        public int GenreId {get; set;}
        
        public int CategoryId {get; set;}
        
        public int MovieId {get; set;}
        //working with entitty framework, we need navigations properties
        public Category CategoryOfMovie {get; set;}
        public Movie MovieWithCategory {get; set;}
    }
}