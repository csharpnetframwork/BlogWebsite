using BlogWebsite.Models.Domains;
using System.Collections.Generic;

namespace BlogWebsite.Repo
{
    public interface ITagRepro
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetAsync(Guid id);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);

    }
}
