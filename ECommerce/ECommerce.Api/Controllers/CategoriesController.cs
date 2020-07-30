using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Models;
using ECommerce.Business.Abstract;
using ECommerce.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerce.Api.Controllers
{
    /// <summary>
    /// Category API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        /// <summary>
        /// Category API
        /// </summary>
        /// <param name="categoryService"></param>
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        /// <summary>
        /// Getting All Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public IQueryable<Category> GetCategories() => _categoryService.Quarayable;

        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (string.IsNullOrEmpty(category.Name))
                return BadRequest(new ResponseModel()
                {
                    Message = "Category can not be empty",
                    Result = false,
                    Status = 404
                });

            await _categoryService.CreateAsync(category);

            return Ok(new
            {
                Message = "Category Created",
                Result = true,
                Status = 200,
                data = category
            });
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{key}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromRoute] int key, Category model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (string.IsNullOrEmpty(model.Name))
                return BadRequest(new ResponseModel()
                {
                    Message = "Category can not be empty",
                    Result = false,
                    Status = 404
                });

            if(key != model.Id)
                return BadRequest(new ResponseModel()
                {
                    Message = "Category can not be empty",
                    Result = false,
                    Status = 404
                });

            var entity = _categoryService.GetById(key);

            entity.Name = model.Name;

            await _categoryService.UpdateAsync(entity);

            return Ok(new
            {
                Message = "Category Updated",
                Result = true,
                Status = 200,
                data = entity
            });
        }


        /// <summary>
        /// Delete category with product categories
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete("{key}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _categoryService.Delete(_categoryService.GetById(key));

            return Ok();
        }

    }
}
