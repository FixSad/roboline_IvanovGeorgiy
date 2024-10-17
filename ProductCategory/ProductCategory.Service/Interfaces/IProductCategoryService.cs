using ProductCategory.Domain.Entities;
using ProductCategory.Domain.Response;
using ProductCategory.Domain.ViewModels;

namespace ProductCategory.Service.Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryEntity>> GetAll();
        Task<IBaseResponse<ProductCategoryEntity>> Delete(ProductCategoryEntity productCategory);
        Task<IBaseResponse<ProductCategoryEntity>> Update(ProductCategoryViewModel newProductCategory, int id);
        Task<IBaseResponse<ProductCategoryEntity>> Create(ProductCategoryViewModel productCategory);
        Task<IBaseResponse<ProductCategoryEntity>> GetCategory(int id);
    }
}
