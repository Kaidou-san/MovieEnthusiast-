using System;
using System.ComponentModel.DataAnnotations;

namespace movieDemo.Models
{
    public class Like
    {
        [Key]
        public int LikeId {get; set;}
        //One to many coming from User
        public int UserId {get; set;}
        //One to many coming from Movie
        public int MovieId {get; set;}
        //working with entitty framework, we need navigations properties
        public User UserWhoLiked {get; set;}
        public Movie MovieLiked {get; set;}
    }
}