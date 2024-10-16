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

        public HomeController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _productCategoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
