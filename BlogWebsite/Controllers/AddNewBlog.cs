using BlogWebsite.Models.ViewModels;
using BlogWebsite.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlogWebsite.Models.Domains;
namespace BlogWebsite.Controllers
{
    public class AddNewBlog : Controller
    {
        private readonly ITagRepro tagRepro;
        private readonly IBlogPostRepro blogPostRepro;

        public AddNewBlog(ITagRepro tagRepro,IBlogPostRepro blogPostRepro)
        {
            this.tagRepro = tagRepro;
            this.blogPostRepro = blogPostRepro;
        }
        [HttpGet]
        public async Task <IActionResult> Add()
        {
            var tags =await tagRepro.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            var blogpost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                Author = addBlogPostRequest.Author,
                PublisedDate = addBlogPostRequest.PublisedDate,
                Visable = addBlogPostRequest.Visable,
                ShortDiscription = addBlogPostRequest.ShortDiscription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                URLHandle = addBlogPostRequest.URLHandle,

            };
            var selectedTags = new List <Tag>();
            foreach(var selectedTagID in addBlogPostRequest.SelectedTags)
            {
                var selectedTagASGuid =Guid.Parse(selectedTagID.ToString());
                var existingtag = await tagRepro.GetAsync(selectedTagASGuid);
                if (existingtag != null) 
                { 
                    selectedTags.Add(existingtag);
                }
            }
            blogpost.Tags = selectedTags;
            await blogPostRepro.AddAsync(blogpost);
            return RedirectToAction ("Add");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var Blogpost= await blogPostRepro.GetAllAsync();

            return View(Blogpost);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

           var blogpost= await blogPostRepro.GetAsync(id);
            var tagsDomainModel = await tagRepro.GetAllAsync();
            if(blogpost != null)
            {
                var model = new EditBlogRequest
                {
                    ID = blogpost.ID,
                    Heading = blogpost.Heading,
                    PageTitle = blogpost.PageTitle,
                    Content = blogpost.Content,
                    Author = blogpost.Author,
                    FeaturedImageUrl = blogpost.FeaturedImageUrl,
                    URLHandle = blogpost.URLHandle,
                    ShortDiscription = blogpost.ShortDiscription,
                    PublisedDate = blogpost.PublisedDate,
                    Visable = blogpost.Visable,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogpost.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }
            
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(EditBlogRequest editBlogRequest)
        {
            var blogPost = new BlogPost
            {
                ID=editBlogRequest.ID,
                Heading=editBlogRequest.Heading,
                PageTitle=editBlogRequest.PageTitle,    
                Content = editBlogRequest.Content,
                Author = editBlogRequest.Author,
                ShortDiscription=editBlogRequest.ShortDiscription,
                FeaturedImageUrl=editBlogRequest.FeaturedImageUrl,
                URLHandle = editBlogRequest.URLHandle,
                PublisedDate=editBlogRequest.PublisedDate,
                Visable = editBlogRequest.Visable,
            };
            var selecttags= new List<Tag>();
            foreach(var tag in editBlogRequest.SelectedTags)
            {
                    if (Guid.TryParse(tag, out var tags))
                    {
                    var foundtag = await tagRepro.GetAsync(tags);
                    if(foundtag != null) 
                    {
                        selecttags.Add(foundtag);
                    }

                    }
               
            }
            blogPost.Tags = selecttags;
          var update=  await blogPostRepro.UpdateAsync(blogPost);
            if(update != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editBlogRequest.ID });
        }
        public async Task<IActionResult> Delete(EditBlogRequest editBlogRequest)
        {
            var exsitblog = await blogPostRepro.DeleteAsync(editBlogRequest.ID);
            if (exsitblog != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editBlogRequest.ID });

        }
    }
}
