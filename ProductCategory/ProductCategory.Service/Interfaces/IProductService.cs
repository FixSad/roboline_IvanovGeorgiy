using ProductCategory.Domain.Entities;
using ProductCategory.Domain.Response;
using ProductCategory.Domain.ViewModels;

namespace ProductCategory.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductEntity>> GetAll();
        Task<IBaseResponse<ProductEntity>> Delete(ProductEntity productCategory);
        Task<IBaseResponse<ProductEntity>> Update(ProductViewModel newProduct, int id);
        Task<IBaseResponse<ProductEntity>> Create(ProductViewModel productCategory);
        Task<IBaseResponse<ProductEntity>> GetProduct(int id);
    }
}
