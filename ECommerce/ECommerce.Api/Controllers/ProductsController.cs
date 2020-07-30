using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ECommerce.Api.Helpers.File;
using ECommerce.Api.Models;
using ECommerce.Business.Abstract;
using ECommerce.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Controllers
{
    /// <summary>
    /// Product API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;


        /// <summary>
        /// Product API Controller
        /// </summary>
        /// <param name="productService"></param>
        public ProductsController(IProductService productService, IWebHostEnvironment env)
        {
            _productService = productService;
            _env = env;
        }
        /// <summary>
        /// Getting products. If 0 is sended as a parameter all product will come
        /// </summary>
        /// <remarks>
        /// Example URL : https://localhost:44309/api/products/0?page=1
        /// </remarks>
        /// <param name="category">CategoryId</param>
        /// <param name="page">Page ındex</param>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("{category}")]
        public IActionResult GetProducts([FromRoute] int? category, [FromQuery] int page = 1)
        {
            const int pageSize = 3;

            var products = _productService.GetProductsByCategory((int)category, page, pageSize)
                .Select(x => new Product()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price,
                    Description = x.Description,
                    Stock = x.Stock
                })
                .ToList();
            int TotalItems = _productService.GetCountByCategory((int)category);
            return Ok(new ProductListModel()
            {
                PageInfo = new PageInfo()
                {
                    CurrentCategoryId = (int)category,
                    TotalItems = TotalItems,
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalPages = (int)Math.Ceiling((decimal)TotalItems / pageSize)
                },
                Products = products
            });
        }

        /// <summary>
        /// Getting All Products whitout any pagination
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public IActionResult GetProdcuts([FromQuery] string name)
        {
            var products = _productService.Quarayable.OrderByDescending(x => x.Id);

            if (!String.IsNullOrEmpty(name))
                products.Where(x => x.Name.ToLower().Contains(name.Trim().ToLower()));
            return Ok(products.ToList());
        }


        /// <summary>
        /// Insert Product
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks>
        /// Example Json :{
        /// "name": "string",
        /// "imageUrl": "string",
        /// "price": 0,
        /// "description": "string",
        /// "stock": 0,}
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] ProductModel entity)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (!String.IsNullOrEmpty(entity.file))
            {
                string imagePath = UploudFile.Base64ToImage(entity.file.Split(",")[1], _env);
                entity.Product.ImageUrl = imagePath;
            }


            await _productService.CreateAsync(entity.Product);

            return Ok(entity);
        }

        /// <summary>
        /// Getting Single Product 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getById/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id == 0)
                return BadRequest(new ResponseModel
                {
                    Message = "Please Provide a id",
                    Result = false,
                    Status = 404
                });

            var product = _productService.Quarayable
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .Where(x => x.IsActive && x.Id == id).Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.ImageUrl,
                    x.Description,
                    x.Price,
                    x.Stock,
                    ProductCategories = x.ProductCategories.Select(w => new
                    {
                        w.ProductId,
                        w.CategoryId,
                        w.Category.Name
                    }).ToList()

                }).FirstOrDefault();

            if (product == null)
                return BadRequest(new ResponseModel
                {
                    Status = 404,
                    Result = false,
                    Message = "Product Not Found"
                });




            return Ok(product);
        }

        /// <summary>
        /// Delete Product Service
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpDelete("{key}")]
        public IActionResult Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (key == 0)
                return BadRequest();

            var product = _productService.GetById(key);

            if (product == null)
                return BadRequest(new ResponseModel()
                {
                    Message = "Product Not Found",
                    Result = false,
                    Status = 400
                });

            if (System.IO.File.Exists(_env.WebRootPath + "/img/" + product.ImageUrl))
            {
                System.IO.File.Delete(_env.WebRootPath + "/img/" + product.ImageUrl);
            }

            _productService.Delete(product);

            return Ok(new ResponseModel()
            {
                Message = "Product Deleted",
                Result = true,
                Status = 200
            });
        }

        /// <summary>
        /// Product Edit
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{key}")]
        public IActionResult Put([FromRoute] int key, ProductModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (key != model.Product.Id)
                return BadRequest();

            var product = _productService.GetById(key);

            if (product == null)
                return BadRequest(new ResponseModel()
                {
                    Message = "Product Not Found",
                    Result = false,
                    Status = 400
                });

            if (!String.IsNullOrEmpty(model.file))
            {
                string oldPath = product.ImageUrl;

                string imagePath = UploudFile.Base64ToImage(model.file.Split(",")[1], _env);
                model.Product.ImageUrl = imagePath;

                if (System.IO.File.Exists(_env.WebRootPath + "/img/" + oldPath))
                {
                    System.IO.File.Delete(_env.WebRootPath + "/img/" + oldPath);
                }
            }

            List<int> categoryIds = new List<int>();

            foreach (var item in model.Product?.ProductCategories)
            {
                categoryIds.Add(item.CategoryId);
            }

            _productService.Update(model.Product, categoryIds);
            return Ok();
        }

    }
}
