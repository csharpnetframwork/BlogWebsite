using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogWebsite.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public String? Heading { get; set; }
        public String? PageTitle { get; set; }
        public String? Content { get; set; }
        public String? ShortDiscription { get; set; }
        public String? FeaturedImageUrl { get; set; }
        public String? URLHandle { get; set; }
        public DateTime PublisedDate { get; set; }
        public String? Author { get; set; }
        public bool Visable { get; set; }
        //display Tag's
        public IEnumerable<SelectListItem> Tags { get; set; }
        public string[] SelectedTags { get; set; }=Array.Empty<string>();

    }
}
