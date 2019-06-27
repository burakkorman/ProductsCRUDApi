using Microsoft.EntityFrameworkCore;
using ProductsCRUDApi.Entities;
using ProductsCRUDApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCRUDApi.Services
{
    public class ProductService : IProductService
    {
        private ProductContext _productContext;

        public ProductService(ProductContext context)
        {
            this._productContext = context;
        }
        public void Add(ProductResponseDTO product)
        {
            //var entity = _productContext.Add()
            Product p = new Product { productName = product.productName, unitPrice = product.unitPrice };
            _productContext.Products.Add(p);
            _productContext.SaveChanges();
        }

        public void Delete(int productId)
        {
            Product p = _productContext.Products.FirstOrDefault(w => w.productId == productId);
            _productContext.Products.Remove(p);
            _productContext.SaveChanges();
        }

        public List<ProductResponseDTO> GetAll()
        {
            var lp = _productContext.Products.Select(x => new ProductResponseDTO { productName = x.productName, unitPrice = x.productId }).ToList();
            //return _productContext.Products.ToList();
            return lp;
        }

        public ProductResponseDTO GetById(int productId)
        {
            Product product = _productContext.Products.FirstOrDefault(w => w.productId == productId);
            if (product == null)
                return null;
            return new ProductResponseDTO
            {
                productName = product.productName,
                unitPrice = product.unitPrice
            };
        }

        public void Update(int id, ProductResponseDTO product)
        {
            Product p = _productContext.Products.FirstOrDefault(w => w.productId == id);
            p.productName = product.productName;
            p.unitPrice = product.unitPrice;

            _productContext.Entry(p).State = EntityState.Modified;
            _productContext.SaveChanges();
        }
    }
}
