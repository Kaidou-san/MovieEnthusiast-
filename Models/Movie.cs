using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace movieDemo.Models
{
    public class Movie
    {
        [Key]
        public int MovieId {get; set;}
        [Required]
        public string Title {get; set;}
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate {get; set;}
        [Required]
        public string Rating {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;
        public List<Like> UserWhoLiked {get; set;}
        public List<Genre> CatsOfMovie {get; set;}

    }
}