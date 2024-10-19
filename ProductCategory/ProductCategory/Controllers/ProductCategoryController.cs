using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCategory.Domain.ViewModels;
using ProductCategory.Models;
using ProductCategory.Service.Interfaces;
using System.Diagnostics;

namespace ProductCategory.Controllers
{
    public class ProductCategoryController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;


        public ProductCategoryController(IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _productCategoryService.GetAll();
            var products = await _productService.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(categories);
        }
        
        public async Task<IActionResult> CreateCategory(ProductCategoryViewModel category)
        {
            var response = await _productCategoryService.Create(category);

            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        public async Task<IActionResult> DeleteCategory(string Id)
        {
            var category = _productCategoryService.GetCategory(int.Parse(Id)).Result.Data;
            var response = await _productCategoryService.Delete(category);

            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        public async Task<IActionResult> changeCategory(string id, string name, string description)
        {
            ProductCategoryViewModel newModel = new ProductCategoryViewModel()
            {
                Id = int.Parse(id),
                Name = name,
                Description = description
            };

            var response = await _productCategoryService.Update(newModel, newModel.Id);

            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

    }
}
