using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductCategory.DAL.Interfaces;
using ProductCategory.Domain.Entities;
using ProductCategory.Domain.Enums;
using ProductCategory.Domain.Response;
using ProductCategory.Domain.ViewModels;
using ProductCategory.Service.Interfaces;
using System.Diagnostics;
using System.Xml.Linq;

namespace ProductCategory.Service.Implementations
{
    public class ProductService : IProductService
    {
        private IBaseRepository<ProductCategoryEntity> _categoryRepository;
        private IBaseRepository<ProductEntity> _productRepository;
        private ILogger<ProductService> _logger;

        public ProductService(IBaseRepository<ProductEntity> productRepository,
            ILogger<ProductService> logger, IBaseRepository<ProductCategoryEntity> categoryRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
            _categoryRepository = categoryRepository;
        }

        // Создание продукта
        public async Task<IBaseResponse<ProductEntity>> Create(ProductViewModel product)
        {
            try
            {
                _logger.LogInformation($"[LOG] Request to create the Product...");
                var tmp = await _productRepository.GetAll().
                    Where(x => x.Id == product.Id).
                    FirstOrDefaultAsync();

                if (tmp != null)
                {
                    _logger.LogInformation($"[LOG] The Product already exists");
                    return new BaseResponse<ProductEntity>
                    {
                        Description = $"The Product already exists",
                        StatusCode = StatusCode.CategoryAlreadyExists
                    };
                }

                var category = await _categoryRepository.GetAll().
                    Where(x => x.Name.Equals(product.Category)).
                    FirstOrDefaultAsync();

                var tmpProduct = new ProductEntity()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = category
                };

                await _productRepository.Create(tmpProduct);
                _logger.LogInformation($"[LOG] The Product was successfully created");

                return new BaseResponse<ProductEntity>
                {
                    Description = $"The Product was successfully created",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ERROR LOG][ProductService.Create]: {ex.Message}");
                return new BaseResponse<ProductEntity>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        // Удаление продукта
        public async Task<IBaseResponse<ProductEntity>> Delete(ProductEntity product)
        {
            try
            {
                _logger.LogInformation($"[LOG] Request to delete the Product...");

                await _productRepository.Delete(product);
                _logger.LogInformation($"[LOG] The Product was successfully deleted");

                return new BaseResponse<ProductEntity>
                {
                    Description = $"The Product was successfully deleted",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ERROR LOG][Product.Delete]: {ex.Message}");
                return new BaseResponse<ProductEntity>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        // Получение всех продуктов
        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
            try
            {
                _logger.LogInformation($"[LOG] Request to get all Product items...");
                var products = _productRepository.GetAll().Select(x => new ProductEntity()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Category = x.Category
                });
                _logger.LogInformation($"[LOG] Request to get all Product items has been completed successfully");
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ERROR LOG] [Product.GetAll]: {ex.Message}");
                return null;
            }
        }

        // Получение продукта по id
        public async Task<IBaseResponse<ProductEntity>> GetProduct(int id)
        {
            try
            {
                _logger.LogInformation($"[LOG] Request to get Product via id...");
                var product = await _productRepository.GetAll().
                    Where(x => x.Id == id).
                    Select(x => new ProductEntity()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Category = x.Category,
                        Price = x.Price
                    }).FirstOrDefaultAsync();

                _logger.LogInformation($"[LOG] Request to get Product via id" +
                    $" has been completed successfully");

                return new BaseResponse<ProductEntity>
                {
                    Description = $"Product has been received successfully",
                    StatusCode = StatusCode.Success,
                    Data = product
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ERROR LOG] [ProductService.GetProduct]: {ex.Message}");
                return new BaseResponse<ProductEntity>
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        // Изменение продукта
        public async Task<IBaseResponse<ProductEntity>> Update(ProductViewModel newProduct, int id)
        {
            {
                try
                {
                    _logger.LogInformation($"[LOG] Request to update the Product...");

                    var finalCategory = await _categoryRepository.GetAll().
                        Where(x => x.Name == newProduct.Category).
                        Select(x => new ProductCategoryEntity()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description
                        }).FirstOrDefaultAsync();

                    var file = await _productRepository.GetAll().
                        Where(x => x.Name == newProduct.Category).
                        Select(x => new ProductEntity()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description,
                            Price = x.Price,
                            Category = finalCategory
                        }).FirstOrDefaultAsync();

                    if (!file.Name.Equals(newProduct.Name))
                        file.Name = newProduct.Name;
                    if (!file.Description.Equals(newProduct.Description))
                        file.Description = newProduct.Description;
                    if (file.Price != newProduct.Price)
                        file.Price = newProduct.Price;
                    /*if (tmp.Category.Id != newProduct.Category)
                        tmp.Category = category;
                    */
                    await _productRepository.Update(file);

                    return new BaseResponse<ProductEntity>
                    {
                        Description = $"The Product was successfully updated",
                        StatusCode = StatusCode.Success
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"[ERROR LOG][ProductService.Update]: {ex.Message}");
                    return new BaseResponse<ProductEntity>
                    {
                        Description = ex.Message,
                        StatusCode = StatusCode.InternalServerError
                    };
                }
            }
        }
    }
}
