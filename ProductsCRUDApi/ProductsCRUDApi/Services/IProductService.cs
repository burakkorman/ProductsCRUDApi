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
        List<ProductResponseDTO> GetAll();
        ProductResponseDTO GetById(int productId);
        void Add(ProductResponseDTO product);
        void Update(int id, ProductResponseDTO product);
        void Delete(int productId);
    }
}
