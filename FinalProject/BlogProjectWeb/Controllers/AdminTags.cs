using BlogProjectWeb.Data;
using BlogProjectWeb.Models.Domain;
using BlogProjectWeb.Models.ViewModels;
using BlogProjectWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BlogProjectWeb.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly TagInterface tagRepository;

        public AdminTagsController(TagInterface tagRepository)
        {
            this.tagRepository = tagRepository;
        }
            
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> AddPost(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            await tagRepository.AddAsync(tag);    

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id); 
            if (tag != null)
            {
                var editTag = new EditTag
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };

                return View(editTag);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTag editTag)
        {
            var tag = new Tag
            {
                Id = editTag.Id,
                Name = editTag.Name,
                DisplayName = editTag.DisplayName,
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);
            if(updatedTag != null) 
            { 
             
            }
            else
            {
               
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedTag = await tagRepository.DeleteAsync(id); 
            if (deletedTag != null)
            {
                return RedirectToAction("List");
            }
                
            return RedirectToAction("Edit", new { id = id });    
        }
    }
}
