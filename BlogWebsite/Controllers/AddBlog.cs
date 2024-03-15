using BlogWebsite.Data;
using BlogWebsite.Models.Domains;
using BlogWebsite.Models.ViewModels;
using BlogWebsite.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Controllers
{

    public class AddBlog : Controller
    {
        private readonly ITagRepro tagRepro;

        public AddBlog(ITagRepro tagRepro)
        {
            this.tagRepro = tagRepro;
        }
        [HttpGet]
        public IActionResult ADD()
        {
            return View();
        }
        [Authorize (Roles="admin")]
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> submitTag(TagClass obj)
        {
            var tag = new Tag
            {
                Name = obj.Name,
                DisplayName = obj.DisplayName,
            };
            await tagRepro.AddAsync(tag);
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //get All Tags from Database
            var AllTag = await tagRepro.GetAllAsync();
            return View(AllTag);
        }

        [HttpGet]
        public async Task <IActionResult> Edit(Guid id)
        {
           var tag=await tagRepro.GetAsync(id);
            if (tag != null)
            {
                var editrequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editrequest);
            }
            return View(null);
        }
        public async Task <IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };
           
              var UpdateTag =await tagRepro.UpdateAsync(tag);

          if (UpdateTag != null)
            {
                return RedirectToAction("List");
            }
            else
            {

            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
        public async Task <IActionResult> Delete(EditTagRequest editTagRequest)
        {
           var deletedtag = await tagRepro.DeleteAsync(editTagRequest.Id);
            if (deletedtag != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

    }
}
