using Microsoft.Extensions.Logging;
using ProductCategory.DAL.Interfaces;
using ProductCategory.Domain.Entities;
using ProductCategory.Service.Interfaces;


namespace ProductCategory.Service.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private IBaseRepository<ProductCategoryEntity> _repository;
        private ILogger<ProductCategoryService> _logger;

        public ProductCategoryService(IBaseRepository<ProductCategoryEntity> productCategoryRepository,
            ILogger<ProductCategoryService> logger)
        {
            _logger = logger;
            _repository = productCategoryRepository;
        }

        public async Task<IEnumerable<ProductCategoryEntity>> GetAll()
        {
            try
            {
                _logger.LogInformation($"Request to get all ProductCategory items...");
                var productCategories = _repository.GetAll();
                _logger.LogInformation($"Request to get all ProductCategory items is successful");
                return productCategories;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error. [ProductCategoryService.GetAll]: {ex.Message}");
                return null;
            }
        }
    }
}
