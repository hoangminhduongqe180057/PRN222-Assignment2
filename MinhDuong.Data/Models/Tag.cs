namespace MinhDuongMVC.Models
{
    public class Tag
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<NewsTag> NewsTags { get; set; }
    }
}