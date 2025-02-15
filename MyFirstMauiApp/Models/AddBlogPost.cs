using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMauiApp.Models
{
    public class AddBlogPost
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public bool IsVisible { get; set; }
        public List<string> CategoryIds { get; set; }
    }
}
