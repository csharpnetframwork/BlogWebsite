using BlogWebsite.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Data
{
    public class BlogDbcontetex : DbContext
    {
        public BlogDbcontetex(DbContextOptions<BlogDbcontetex> options) : base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag>Tags { get; set; }
    }
}
