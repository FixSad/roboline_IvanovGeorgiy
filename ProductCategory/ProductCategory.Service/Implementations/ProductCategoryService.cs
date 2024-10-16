using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductCategory.DAL.Interfaces;
using ProductCategory.Domain.Entities;
using ProductCategory.Domain.Enums;
using ProductCategory.Domain.Response;
using ProductCategory.Domain.ViewModels;
using ProductCategory.Service.Interfaces;

namespace ProductCategory.Service.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private IBaseRepository<ProductCategoryEntity> _categoryRepository;
        private ILogger<ProductCategoryService> _logger;

        public ProductCategoryService(IBaseRepository<ProductCategoryEntity> productCategoryRepository,
            ILogger<ProductCategoryService> logger)
        {
            _logger = logger;
            _categoryRepository = productCategoryRepository;
        }

        // Создание категории
        public async Task<IBaseResponse<ProductCategoryEntity>> Create(ProductCategoryViewModel productCategory)
        {
            try
            {
                _logger.LogInformation($"[LOG] Request to create the ProductCategory...");
                var tmp = await _categoryRepository.GetAll().Where(x => x.Name.Equals(productCategory.Name)
                    && x.Description.Equals(productCategory.Description)).FirstOrDefaultAsync();

                if (tmp != null)
                {
                    _logger.LogInformation($"[LOG] The ProductCategory already exists");
                    return new BaseResponse<ProductCategoryEntity>
                    {
                        Description = $"The ProductCategory already exists",
                        StatusCode = StatusCode.CategoryAlreadyExists
                    };
                }

                var category = new ProductCategoryEntity()
                {
                    Name = productCategory.Name,
                    Description = productCategory.Description
                };

                await _categoryRepository.Create(category);
                _logger.LogInformation($"[LOG] The ProductCategory was successfully created");

                return new BaseResponse<ProductCategoryEntity>
                {
                    Description = $"The ProductCategory was successfully created",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ERROR LOG][ProductCategoryService.Create]: {ex.Message}");
                return new BaseResponse<ProductCategoryEntity>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        // Удаление категории
        public async Task<IBaseResponse<ProductCategoryEntity>> Delete(ProductCategoryViewModel productCategory)
        {
            try
            {
                _logger.LogInformation($"[LOG] Request to delete the ProductCategory...");
                var tmp = await _categoryRepository.GetAll().Where(x => x.Name.Equals(productCategory.Name) 
                    && x.Description.Equals(productCategory.Description)).FirstOrDefaultAsync();

                if (tmp == null) 
                {
                    _logger.LogInformation($"[LOG] The ProductCategory not found");
                    return new BaseResponse<ProductCategoryEntity>
                    {
                        Description = $"The ProductCategory not found",
                        StatusCode = StatusCode.CategoryNotFound
                    };
                }

                await _categoryRepository.Delete(tmp);
                _logger.LogInformation($"[LOG] The ProductCategory was successfully deleted");

                return new BaseResponse<ProductCategoryEntity>
                {
                    Description = $"The ProductCategory was successfully deleted",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ERROR LOG][ProductCategoryService.Delete]: {ex.Message}");
                return new BaseResponse<ProductCategoryEntity>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        // Получение всех категорий
        public async Task<IEnumerable<ProductCategoryEntity>> GetAll()
        {
            try
            {
                _logger.LogInformation($"[LOG] Request to get all ProductCategory items...");
                var productCategories = _categoryRepository.GetAll();
                _logger.LogInformation($"[LOG] Request to get all ProductCategory items has been completed successfully");
                return productCategories;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[LOG Error] [ProductCategoryService.GetAll]: {ex.Message}");
                return null;
            }
        }

        // Изменение категории
        public async Task<IBaseResponse<ProductCategoryEntity>> Update(ProductCategoryViewModel oldProductCategory,
            ProductCategoryViewModel newProductCategory)
        {
            try
            {
                _logger.LogInformation($"[LOG] Request to update the ProductCategory...");
                var tmp = await _categoryRepository.GetAll().Where(x => x.Name.Equals(oldProductCategory.Name)
                    && x.Description.Equals(oldProductCategory.Description)).FirstOrDefaultAsync();

                if (tmp == null)
                {
                    _logger.LogInformation($"[LOG] The ProductCategory not found");
                    return new BaseResponse<ProductCategoryEntity>
                    {
                        Description = $"The ProductCategory not found",
                        StatusCode = StatusCode.CategoryNotFound
                    };
                }

                if(!tmp.Name.Equals(newProductCategory.Name))
                    tmp.Name = newProductCategory.Name;
                if(!tmp.Description.Equals(newProductCategory.Description))
                    tmp.Description = newProductCategory.Description;

                await _categoryRepository.Update(tmp);

                return new BaseResponse<ProductCategoryEntity>
                {
                    Description = $"The ProductCategory was successfully updated",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ERROR LOG][ProductCategoryService.Update]: {ex.Message}");
                return new BaseResponse<ProductCategoryEntity>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
