using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCategory.Models;
using ProductCategory.Service.Interfaces;
using System.Diagnostics;

namespace ProductCategory.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;


        public HomeController(IProductCategoryService productCategoryService, IProductService productService)
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
    }
}
