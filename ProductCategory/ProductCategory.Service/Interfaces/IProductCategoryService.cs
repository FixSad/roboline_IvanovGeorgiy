using ProductCategory.Domain.Entities;
using ProductCategory.Domain.Response;
using ProductCategory.Domain.ViewModels;

namespace ProductCategory.Service.Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryEntity>> GetAll();
        Task<IBaseResponse<ProductCategoryEntity>> Delete(ProductCategoryViewModel productCategory);
        Task<IBaseResponse<ProductCategoryEntity>> Update(ProductCategoryViewModel oldProductCategory,
            ProductCategoryViewModel newProductCategory);
        Task<IBaseResponse<ProductCategoryEntity>> Create(ProductCategoryViewModel productCategory);
    }
}
