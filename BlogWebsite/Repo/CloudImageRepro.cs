
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BlogWebsite.Repo
{
    public class CloudImageRepro : IIamgeRepro
    {
        public async Task<string> UploadAsync(IFormFile file)
        {
            Account account = new Account(
                          "dbx4zb5mr",
                          "667786594938519",
                         "ki5ds-cetWDCYoKWDSXYVbg3qoA");

            Cloudinary cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName,file.OpenReadStream()),
                DisplayName = file.FileName
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            if(uploadResult != null &&uploadResult.StatusCode==System.Net.HttpStatusCode.OK) 
            {
             return uploadResult.SecureUri.ToString();
            }
            return null;
        }
    }
}
