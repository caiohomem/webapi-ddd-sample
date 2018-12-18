using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hexis.ApplicationLayer;
using Hexis.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Hexis.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("product/{pageSize}/{pageIndex}")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllPaged(int pageSize, int pageIndex)
        {
            return Ok(await _productService.GetAllPagedAsync(pageSize, pageIndex));
        }

        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> Insert(ProductDto product)
        {
            try
            {
                return Ok(await _productService.InsertAsync(product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPut]
        [Route("product")]
        public async Task<IActionResult> Update(ProductDto product)
        {
            try
            {
                return Ok(await _productService.UpdateAsync(product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpDelete]
        [Route("product/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                return Ok(await _productService.RemoveAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("product/increase/{id}/{quantity}")]
        public async Task<IActionResult> IncreaseStock(int id, int quantity)
        {
            try
            {
                return Ok(await _productService.IncreaseStockAsync(id, quantity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPut]
        [Route("product/decrease/{id}/{quantity}")]
        public async Task<IActionResult> DeecreaseStock(int id, int quantity)
        {
            try
            {
                return Ok(await _productService.DecreaseStockAsync(id, quantity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
