using ProductsCRUDApi.Entities;
using ProductsCRUDApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCRUDApi.Services
{
    public interface IProductService
    {
        List<ProductDTO> GetAll();
        ProductDTO GetById(int productId);
        List<ProductDTO> Pagination(int take, int skip);
        void Add(ProductDTO product);
        void Update(int id, ProductDTO product);
        void Delete(int productId);
    }
}
