﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ProductGroup
    {
        [Key]
        public int Id { get; set; }
        public string GroupDescription { get; set; }
        [DisplayName("Product Group Code")]
        public int GroupCode { get; set; }
        public bool Active { get; set; }
    }
}
