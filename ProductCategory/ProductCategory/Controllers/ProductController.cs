using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCategory.Domain.Entities;
using ProductCategory.Domain.ViewModels;
using ProductCategory.Service.Interfaces;

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
            var products = await _productService.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(products);
        }

        public async Task<IActionResult> CreateProduct(ProductViewModel product)
        {
            var response = await _productService.Create(product);

            if(response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description =  response.Description });
            return BadRequest(new { description = response.Description });
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _productService.GetProduct(id);
            response = await _productService.Delete(response.Data);
            if(response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description =  response.Description });
            return BadRequest(new { description = response.Description });
        }

        public async Task<IActionResult> ChangeProduct(string name, string description, 
            string price, int id, string category)
        {
            var response = await _productService.GetProduct(id);

            if(response.StatusCode != Domain.Enums.StatusCode.Success)
                return BadRequest(new { description = response.Description });

            try
            {
                decimal decimal_price = decimal.Parse(price);
            }
            catch (Exception ex)
            {
                return BadRequest(new { description = "Enter the correct data type(decimal, int) for Price" });
            }
            

            ProductViewModel product = new ProductViewModel()
            {
                Name = name,
                Description = description,
                Id = id,
                Price = decimal.Parse(price),
                Category = response.Data.Name
            };
            var finalResponse = await _productService.Update(product, id);

            if(finalResponse.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = finalResponse.Description });
            return BadRequest(new { description = finalResponse.Description });
        }
    }
}
