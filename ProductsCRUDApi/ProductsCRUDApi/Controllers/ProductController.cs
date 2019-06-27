using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsCRUDApi.Entities;
using ProductsCRUDApi.Models;
using ProductsCRUDApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsCRUDApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public List<ProductResponseDTO> Get()
        {
            return _productService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductResponseDTO> Get(int id)
        {
            var data = _productService.GetById(id);
            if (data == null)
                return NotFound();

            return data;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]ProductResponseDTO p)
        {
            _productService.Add(p);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ProductResponseDTO p)
        {
            _productService.Update(id, p);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.Delete(id);
        }
    }
}
