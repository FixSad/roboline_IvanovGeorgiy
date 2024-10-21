using Microsoft.AspNetCore.Mvc;
using ProductCategory.Domain.Entities;
using ProductCategory.Domain.ViewModels;
using ProductCategory.Service.Interfaces;

namespace ProductCategory.Controllers
{
    [Route("api/ProductApiController")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _poductService;

        public ProductApiController(IProductService productService) 
            => _poductService = productService;

        // GET: api/ProductApiController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductEntity>>> Get()
        {
            var products = await _poductService.GetAll();
            return Ok(products);
        }

        // GET: api/ProductApiController/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> Get(int id)
        {
            var response = await _poductService.GetProduct(id);
            if(response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(response.Data);
            return BadRequest(new { description = response.Description });
        }

        // PUT: api/ProductApiController/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductEntity>> Put(int id, ProductViewModel product)
        {
            var response = _poductService.Update(product, id).Result;
            if(response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        // POST: api/ProductApiController
        [HttpPost]
        public async Task<ActionResult<ProductEntity>> Post(ProductViewModel product)
        {
            var response = await _poductService.Create(product);
            if(response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        // DELETE: api/ProductApiController/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductEntity>> Delete(int id)
        {
            var response = _poductService.GetProduct(id).Result;
            if (response.StatusCode == Domain.Enums.StatusCode.Success)
            {
                var finalResponse = await _poductService.Delete(response.Data);
                if (finalResponse.StatusCode == Domain.Enums.StatusCode.Success)
                    return Ok(new { description = finalResponse.Description });
                return BadRequest(new {description = finalResponse.Description });
            }
            return BadRequest(new { description = response.Description });
        }
    }
}
