namespace Practice.Models
{
    public class Say : BaseEntity
    {
        public string Image { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public Author  Author { get; set; }
    }
}
