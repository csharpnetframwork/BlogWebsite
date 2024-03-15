using BlogWebsite.Repo;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebsite.Controllers
{
    public class Blog:Controller
    {
        private readonly IBlogPostRepro blogPostRepro;

        public Blog(IBlogPostRepro blogPostRepro)
        {
            this.blogPostRepro = blogPostRepro;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogpost =await blogPostRepro.GetByUrlHandleAsync(urlHandle);

            return View(blogpost);
        }
    }
}
