using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.ViewModel
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public SelectList ProductGroups { get; set; }
    }
}
