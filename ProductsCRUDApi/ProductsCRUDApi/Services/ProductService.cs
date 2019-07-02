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

        public void Add(ProductDTO product)
        {
            Product p = new Product
            {
                productName = product.productName,
                unitPrice = product.unitPrice,
                status = 1
            };
            _productContext.Products.Add(p);
            _productContext.SaveChanges();
        }

        public void Delete(int productId)
        {
            Product p = _productContext.Products.FirstOrDefault(w => w.productId == productId);
            p.status = 0;
            _productContext.SaveChanges();
        }

        public List<ProductDTO> GetAll()
        {
            var plist = _productContext.Products.Where(w =>
                        w.status == 1)
                        .ToList()
                        .Select(x =>
                        new ProductDTO
                        {
                            productName = x.productName,
                            unitPrice = x.unitPrice
                        }).ToList();

            if (plist.Count == 0)
                return null;

            return plist;
        }

        public ProductDTO GetById(int productId)
        {
            Product product = _productContext.Products.FirstOrDefault(w => 
                              w.productId == productId && 
                              w.status == 1
                              );

            if (product == null)
                return null;

            return new ProductDTO
            {
                productName = product.productName,
                unitPrice = product.unitPrice
            };
        }

        public List<ProductDTO> Pagination(int take, int skip)
        {
            List<ProductDTO> pList = _productContext
                                    .Products
                                    .Skip(skip)
                                    .Take(take)
                                    .Select(x =>
                                    new ProductDTO
                                    {
                                        productName = x.productName,
                                        unitPrice = x.unitPrice
                                    }).ToList();

            return pList;
        }

        public void Update(int id, ProductDTO product)
        {
            Product p = _productContext.Products.FirstOrDefault(w => w.productId == id);
            p.productName = product.productName;
            p.unitPrice = product.unitPrice;

            _productContext.Entry(p).State = EntityState.Modified;
            _productContext.SaveChanges();
        }
    }
}
