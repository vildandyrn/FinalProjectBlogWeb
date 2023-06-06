using BlogProjectWeb.Data;
using BlogProjectWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogProjectWeb.Repositories
{
    public class TagRepocs : TagInterface
    {
        private readonly BlogDbContext blogDbContext;
        public TagRepocs(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {

            await blogDbContext.Tags.AddAsync(tag);
            await blogDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {

            var existingTag = await blogDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                blogDbContext.Tags.Remove(existingTag);
                await blogDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await blogDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return blogDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await blogDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;

                if (!string.IsNullOrEmpty(tag.DisplayName))
                {
                    existingTag.DisplayName = tag.DisplayName;
                }
                else
                {
                    blogDbContext.Entry(existingTag).Property(x => x.DisplayName).IsModified = false;
                }

                await blogDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
