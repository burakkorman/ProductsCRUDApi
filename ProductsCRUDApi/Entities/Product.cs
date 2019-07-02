using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCRUDApi.Entities
{
    public class Product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public decimal unitPrice { get; set; }
        public int status { get; set; }
    }
}
