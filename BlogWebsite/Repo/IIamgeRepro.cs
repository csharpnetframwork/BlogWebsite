using System;

namespace BlogWebsite.Repo
{
    public interface IIamgeRepro
    {
        Task<string> UploadAsync(IFormFile formFile);
       
    }
}
