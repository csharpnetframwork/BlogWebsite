using BlogWebsite.Data;
using BlogWebsite.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Repo
{
    public class BlogPostRepro : IBlogPostRepro
    {
        private readonly BlogDbcontetex blogDbcontetex;

        public BlogPostRepro(BlogDbcontetex blogDbcontetex)
        {
            this.blogDbcontetex = blogDbcontetex;
        }
        public async Task<BlogPost> AddAsync(BlogPost BlogPost)
        {
          await blogDbcontetex.AddAsync(BlogPost);
            await blogDbcontetex.SaveChangesAsync(); 
            return BlogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var exitBlog = await blogDbcontetex.BlogPosts.FindAsync(id);
            if (exitBlog != null)
            {
                blogDbcontetex.Remove(exitBlog);
                await blogDbcontetex.SaveChangesAsync();
                return exitBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await blogDbcontetex.BlogPosts.Include(x=>x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await blogDbcontetex.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(t => t.ID==id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
           return await blogDbcontetex.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.URLHandle==urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost BlogPost)
        {
            var exisitingBlog = await blogDbcontetex.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x=>x.ID==BlogPost.ID);
            if (exisitingBlog != null)
            {
                exisitingBlog.ID=BlogPost.ID;
                exisitingBlog.Heading=BlogPost.Heading;
                exisitingBlog.PageTitle=BlogPost.PageTitle;
                exisitingBlog.Content=BlogPost.Content;
                exisitingBlog.ShortDiscription=BlogPost.ShortDiscription;
                exisitingBlog.FeaturedImageUrl=BlogPost.FeaturedImageUrl;
                exisitingBlog.PublisedDate=BlogPost.PublisedDate;
                exisitingBlog.URLHandle=BlogPost.URLHandle;
                exisitingBlog.Visable=BlogPost.Visable;
                exisitingBlog.Author=BlogPost.Author;
                exisitingBlog.Tags =BlogPost.Tags;

                await blogDbcontetex.SaveChangesAsync();
                return exisitingBlog;
            }
            return null;
        }
    }
}
