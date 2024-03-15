using BlogWebsite.Data;
using BlogWebsite.Models.Domains;
using BlogWebsite.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BlogWebsite.Repo
{
    public class TagRepro : ITagRepro

    {
        private readonly BlogDbcontetex blogDb;

        public TagRepro(BlogDbcontetex blogDb)
        {
            this.blogDb = blogDb;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await blogDb.AddAsync(tag);
            await blogDb.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag = await blogDb.Tags.FindAsync(id);
            if (tag != null)
            {
                blogDb.Tags.Remove(tag);
                await blogDb.SaveChangesAsync();
                return tag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
           return await blogDb.Tags.ToListAsync();
            
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
          return  await blogDb.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var exsitingDbrecord = await blogDb.Tags.FindAsync(tag.Id);
            if (exsitingDbrecord != null)
            {
                exsitingDbrecord.Name = tag.Name;
                exsitingDbrecord.DisplayName = tag.DisplayName;
                await blogDb.SaveChangesAsync();
                return exsitingDbrecord;
            }
            return null;

        }
    }
}
