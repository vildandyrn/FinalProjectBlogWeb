using BlogProjectWeb.Data;
using BlogProjectWeb.Models.Domain;
using BlogProjectWeb.Repositories;
using Microsoft.EntityFrameworkCore;

public class BlogRepository : IBlogRepository
{
    private readonly BlogDbContext blogDbContext;

    public BlogRepository(BlogDbContext blogDbContext)
    {
        this.blogDbContext = blogDbContext;
    }

    public async Task<BlogPost> AddAsync(BlogPost blogpost)
    {
        await blogDbContext.AddAsync(blogpost);
        await blogDbContext.SaveChangesAsync();
        return blogpost;
    }

    public async Task<BlogPost?> DeleteAsync(Guid id)
    {
        var blogPost = await blogDbContext.BlogPosts.FindAsync(id);
        if (blogPost != null)
        {
            blogDbContext.BlogPosts.Remove(blogPost);
            await blogDbContext.SaveChangesAsync();
        }
        return blogPost;
    }

    public Task DeleteAsync(BlogPost blogPost)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return await blogDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
    }

    public async Task<BlogPost> GetAsync(Guid id)
    {
        return await blogDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
    {
        blogDbContext.Entry(blogPost).State = EntityState.Modified;
        await blogDbContext.SaveChangesAsync();
        return blogPost;
    }
}