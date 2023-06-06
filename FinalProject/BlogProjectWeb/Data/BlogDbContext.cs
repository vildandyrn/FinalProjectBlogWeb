using BlogProjectWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogProjectWeb.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        { 
        }  

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Tag> Tags { get; set; }    
    }
}
