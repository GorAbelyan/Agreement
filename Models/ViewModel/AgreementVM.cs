using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.ViewModel
{
    public class AgreementVM
    {
        public int ModelId { get; set; }
        public Agreement Agreement { get; set; }
        public IEnumerable<SelectListItem> ProductGroups { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }

    }
}
