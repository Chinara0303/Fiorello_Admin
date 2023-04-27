namespace Practice.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Profession { get; set; }
        public ICollection<Say> Says { get; set; }
    }
}
