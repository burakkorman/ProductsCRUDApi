using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCRUDApi.Models
{
    public class ProductDTO
    {
        public string productName { get; set; }
        public decimal unitPrice { get; set; }
    }
}
