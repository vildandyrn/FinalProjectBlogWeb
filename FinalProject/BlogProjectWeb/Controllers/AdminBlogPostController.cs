using BlogProjectWeb.Data;
using BlogProjectWeb.Models.Domain;
using BlogProjectWeb.Models.ViewModels;
using BlogProjectWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace BlogProjectWeb.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly TagInterface tagInterface;
        private readonly IBlogRepository blogRepository;

        public AdminBlogPostController(TagInterface tagInterface, IBlogRepository blogRepository)
        {
            this.tagInterface = tagInterface;
            this.blogRepository = blogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagInterface.GetAllAsync();

            var model = new AddBlogPostReq
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostReq addBlogPostReq)
        {
            var blogPost = new BlogPost
            {
                Heading = addBlogPostReq.Heading,
                PageTitle = addBlogPostReq.PageTitle,
                Content = addBlogPostReq.Content,
                ShortDescription = addBlogPostReq.ShortDescription,
                FeaturedImageUrl = addBlogPostReq.FeaturedImageUrl,
                UrlHandle = addBlogPostReq.UrlHandle,
                PublishedDate = addBlogPostReq.PublishedDate,
                Author = addBlogPostReq.Author,
                //Visible = addBlogPostReq.Visible,
            };

            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostReq.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagInterface.GetAsync(selectedTagIdAsGuid);
                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            blogPost.Tags = selectedTags;

            await blogRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPosts = await blogRepository.GetAllAsync();
            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost = await blogRepository.GetAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            var tagsDomainModel = await tagInterface.GetAllAsync();
            var model = new EditBlogPostRequest
            {
                Id = blogPost.Id,
                Heading = blogPost.Heading,
                PageTitle = blogPost.PageTitle,
                Content = blogPost.Content,
                Author = blogPost.Author,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                ShortDescription = blogPost.ShortDescription,
                PublishedDate = blogPost.PublishedDate,
                //Visible = blogPost.Visible,
                Tags = tagsDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditBlogPostRequest editBlogPostReq)
        {
            var blogPost = await blogRepository.GetAsync(editBlogPostReq.Id);
            if (blogPost == null)
            {
                return NotFound();
            }

            blogPost.Heading = editBlogPostReq.Heading;
            blogPost.PageTitle = editBlogPostReq.PageTitle;
            blogPost.Content = editBlogPostReq.Content;

            await blogRepository.UpdateAsync(blogPost);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedBlogPost = await blogRepository.DeleteAsync(id);
            if (deletedBlogPost == null)
            {
                return NotFound();
            }

            return RedirectToAction("List");
        }

    }
}