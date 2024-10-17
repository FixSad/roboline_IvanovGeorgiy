using Microsoft.AspNetCore.Mvc;
using ProductCategory.Domain.Entities;
using ProductCategory.Domain.Response;
using ProductCategory.Domain.ViewModels;
using ProductCategory.Service.Interfaces;

namespace ProductCategory.Controllers
{
    [Route("api/ProductCategoryApiController")]
    [ApiController]
    public class ProductCategoryApiController : ControllerBase
    {
        private IProductCategoryService _categoryService;

        public ProductCategoryApiController(IProductCategoryService categoryService) 
            => _categoryService = categoryService;

        // GET: api/ProductCategoryApiController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryEntity>>> Get()
        {
            var caregories = await _categoryService.GetAll();
            return Ok(caregories);
        }

        // GET api/ProductCategoryApiController/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryEntity>> Get(int id)
        {
            var response = _categoryService.GetCategory(id).Result;
            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(response.Data);
            return BadRequest(new { description = response.Description });
        }

        // POST api/ProductCategoryApiController
        [HttpPost]
        public async Task<ActionResult<ProductCategoryEntity>> Post(ProductCategoryViewModel model)
        {
            var response = await _categoryService.Create(model);
            if(response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        // PUT api/ProductCategoryApiController/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductCategoryEntity>> Put(ProductCategoryViewModel model, int id)
        {
            var response = await _categoryService.Update(model, id);
            if (response.StatusCode != Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        // DELETE api/ProductCategoryApiController/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductCategoryEntity>> Delete(int id)
        {
            var response = _categoryService.GetCategory(id).Result;
            if (response.StatusCode == Domain.Enums.StatusCode.Success)
            {
                response = await _categoryService.Delete(response.Data);
                if(response.StatusCode == Domain.Enums.StatusCode.Success)
                    return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }
    }
}
