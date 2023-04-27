using Practice.Models;

namespace Practice.ViewModels.Footer
{
    public class FooterVM
    {
        public IEnumerable<Social> Socials { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}
