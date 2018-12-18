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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("category/{pageSize}/{pageIndex}")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllPaged(int pageSize, int pageIndex)
        {
            return Ok(await _categoryService.GetAllPagedAsync(pageSize, pageIndex));
        }

        [HttpPost]
        [Route("category")]
        public async Task<IActionResult> Insert([FromBody]CategoryDto category)
        {
            try
            {
                return Ok(await _categoryService.InsertAsync(category));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("category")]
        public async Task<IActionResult> Update(CategoryDto category)
        {
            try
            {
                return Ok(await _categoryService.UpdateAsync(category));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("category/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                return Ok(await _categoryService.RemoveAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}
