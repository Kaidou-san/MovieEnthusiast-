using Microsoft.EntityFrameworkCore;

namespace movieDemo.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) {}
        // dont forget to add ur DBSet here!!
        public DbSet<User> Users {get; set;}
        public DbSet<Movie> Movies {get; set;}
        public DbSet<Like> Likes {get; set;}
        public DbSet<Genre> CatsOfMovies {get; set;}
        public DbSet<Category> Categories {get; set;}
    }
}