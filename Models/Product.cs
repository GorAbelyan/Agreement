using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public int ProductGroupID { get; set; }
        [ForeignKey("ProductGroupID")]
        public ProductGroup ProdGroup { get; set; }

        public string ProductDescription { get; set; }
        public int ProductNumber { get; set; }
        public float Price { get; set; }
        public bool Active { get; set; }
        public ICollection<Agreement> Agreements { get; set; }
    }
}
