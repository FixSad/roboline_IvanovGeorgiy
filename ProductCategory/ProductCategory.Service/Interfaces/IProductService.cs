using ProductCategory.Domain.Entities;
using ProductCategory.Domain.Response;
using ProductCategory.Domain.ViewModels;

namespace ProductCategory.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductEntity>> GetAll();
        Task<IBaseResponse<ProductEntity>> Delete(ProductViewModel productCategory);
        Task<IBaseResponse<ProductEntity>> Update(ProductViewModel oldProduct,
            ProductViewModel newProduct);
        Task<IBaseResponse<ProductEntity>> Create(ProductViewModel productCategory);
    }
}
