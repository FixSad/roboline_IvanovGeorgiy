using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCategory.Domain.ViewModels;
using ProductCategory.Models;
using ProductCategory.Service.Interfaces;
using System.Diagnostics;

namespace ProductCategory.Controllers
{
    public class ProductController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;


        public ProductController(IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _productCategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }
        
        public async Task<IActionResult> CreateCategory(ProductCategoryViewModel category)
        {
            var response = await _productCategoryService.Create(category);

            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

    }
}
