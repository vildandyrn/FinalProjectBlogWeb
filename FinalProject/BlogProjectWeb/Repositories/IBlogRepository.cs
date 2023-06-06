using BlogProjectWeb.Models.Domain;

namespace BlogProjectWeb.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetAsync(Guid id);

        Task<BlogPost> AddAsync(BlogPost blogpost);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);

        Task<BlogPost?> DeleteAsync(Guid id);
        Task DeleteAsync(BlogPost blogPost);
    }
}
