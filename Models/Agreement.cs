using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Agreement
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int ProductGroupId { get; set; }
        [ForeignKey("ProductGroupId")]
        public ProductGroup ProductGroup { get; set; }

        [DataType(DataType.Date)]
        public DateTime EffectiveDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }
        public float ProductPrice { get; set; }

        [DataType(DataType.Currency)]
        public float NewPrice { get; set; }
    }
}
