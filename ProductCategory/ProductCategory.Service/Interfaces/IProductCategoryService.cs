using ProductCategory.Domain.Entities;
using ProductCategory.Domain.Response;

namespace ProductCategory.Service.Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryEntity>> GetAll(); 
    }
}
