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
                var tmp = await _categoryRepository.GetAll().
                    Where(x => x.Name.Equals(productCategory.Name)).
                    FirstOrDefaultAsync();


                // Проверка на существование
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
        public async Task<IBaseResponse<ProductCategoryEntity>> Delete(ProductCategoryEntity productCategory)
        {
            try
            {
                _logger.LogInformation($"[LOG] Request to delete the ProductCategory...");

                await _categoryRepository.Delete(productCategory);
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
                _logger.LogError(ex, $"[ERROR LOG] [ProductCategoryService.GetAll]: {ex.Message}");
                return null;
            }
        }

        // Получений категории по id
        public async Task<IBaseResponse<ProductCategoryEntity>> GetCategory(int id)
        {
            try
            {
                _logger.LogInformation($"[LOG] Request to get ProductCategory via id...");
                var productCategory = await _categoryRepository.GetAll().
                    Where(x => x.Id == id).
                    FirstOrDefaultAsync();

                _logger.LogInformation($"[LOG] Request to get ProductCategory via id" +
                    $" has been completed successfully");

                return new BaseResponse<ProductCategoryEntity>
                {
                    Description = $"ProductCategory item has been received successfully",
                    StatusCode = StatusCode.Success,
                    Data = productCategory
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ERROR LOG] [ProductCategoryService.GetCategory]: {ex.Message}");
                return new BaseResponse<ProductCategoryEntity>
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        // Изменение категории
        public async Task<IBaseResponse<ProductCategoryEntity>> Update(ProductCategoryViewModel newProductCategory, int id)
        {
            try
            {
                _logger.LogInformation($"[LOG] Request to update the ProductCategory...");
                var tmp = await _categoryRepository.GetAll().
                    Where(x => x.Id == id).
                    FirstOrDefaultAsync();

                if (tmp == null)
                {
                    _logger.LogInformation($"[LOG] The ProductCategory not found");
                    return new BaseResponse<ProductCategoryEntity>
                    {
                        Description = $"The ProductCategory not found",
                        StatusCode = StatusCode.CategoryNotFound
                    };
                }

                var valid = await _categoryRepository.GetAll().
                    Where(x => x.Name == newProductCategory.Name).
                    FirstOrDefaultAsync();

                if (valid != null)
                {
                    _logger.LogInformation($"[LOG] The ProductCategory not found");
                    return new BaseResponse<ProductCategoryEntity>
                    {
                        Description = $"The ProductCategory already exists",
                        StatusCode = StatusCode.CategoryAlreadyExists
                    };
                }

                if (!tmp.Name.Equals(newProductCategory.Name))
                    tmp.Name = newProductCategory.Name;
                if(!tmp.Description.Equals(newProductCategory.Description))
                    tmp.Description = newProductCategory.Description;

                await _categoryRepository.Update(tmp);
                _logger.LogInformation($"[LOG] Request to update ProductCategory" +
                    $" has been completed successfully");
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
