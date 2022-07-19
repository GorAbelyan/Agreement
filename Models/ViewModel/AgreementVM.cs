using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.ViewModel
{
    public class AgreementVM
    {
        public int ModelId { get; set; }
        public Agreement Agreement { get; set; }
        public SelectList ProductGroups { get; set; }
        public SelectList Products { get; set; }
    }
}
