namespace BlogWebsite.Models.Domains
{
    public class BlogPost
    {
        public Guid ID { get; set; }
        public String? Heading { get; set; }
        public String? PageTitle { get; set; }
        public String? Content { get; set; }
        public String? ShortDiscription { get; set; }
        public String? FeaturedImageUrl { get; set; }
        public String? URLHandle { get; set; }
        public DateTime PublisedDate { get; set; }
        public String? Author { get; set; }
        public bool Visable { get; set; }

        public ICollection<Tag>Tags { get; set; }
    }
}
