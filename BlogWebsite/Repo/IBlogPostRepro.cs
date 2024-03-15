using BlogWebsite.Models.Domains;

namespace BlogWebsite.Repo
{
    public interface IBlogPostRepro
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetAsync(Guid id);
        Task<BlogPost> AddAsync(BlogPost BlogPost);
        Task<BlogPost?> UpdateAsync(BlogPost BlogPost);
        Task<BlogPost?> DeleteAsync(Guid id);
        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
       
    }
}
