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
        public ActionResult<List<ProductDTO>> Get()
        {
            var pList = _productService.GetAll();

            if (pList == null)
                return NotFound();

            return pList;
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(int id)
        {
            var data = _productService.GetById(id);
            if (data == null)
                return NotFound();

            return data;
        }

        // GET api/<controller>/pagination?take=10&skip=10
        [HttpGet("pagination")] 
        public List<ProductDTO> Get(int take, int skip)
        {
            return _productService.Pagination(take, skip);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]ProductDTO p)
        {
            _productService.Add(p);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ProductDTO p)
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
