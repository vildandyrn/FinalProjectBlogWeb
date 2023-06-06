using BlogProjectWeb.Models.Domain;
namespace BlogProjectWeb.Repositories
{
    public interface TagInterface
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetAsync(Guid id);
        Task<Tag> AddAsync(Tag tag);
       
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);    
    }
}
