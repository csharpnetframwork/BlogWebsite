using BlogWebsite.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class ImagesController : ControllerBase
    {
        private readonly IIamgeRepro iamgeRepro;

        public ImagesController(IIamgeRepro iamgeRepro)
        {
            this.iamgeRepro = iamgeRepro;
        }

        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
          var up=  await iamgeRepro.UploadAsync(file);
            if (up == null) 
            {
                return Problem("Some Thing went Worng", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new {link=up});
        }
    }
}
