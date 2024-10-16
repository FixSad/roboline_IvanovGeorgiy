using Microsoft.EntityFrameworkCore;
using ProductCategory.DAL.Interfaces;
using ProductCategory.Domain.Entities;

namespace ProductCategory.DAL.Repositories
{
    public class ProductCategoryRepository : IBaseRepository<ProductCategoryEntity>
    {
        private ApplicationDBContext _dbContext;

        public ProductCategoryRepository(ApplicationDBContext dBContext) => _dbContext = dBContext;

        public async Task Create(ProductCategoryEntity entity)
        {
            await _dbContext.ProductCategories.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(ProductCategoryEntity entity)
        {
            _dbContext.ProductCategories.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<ProductCategoryEntity> GetAll()
        {
            return _dbContext.ProductCategories;
        }

        public async Task Update(ProductCategoryEntity entity)
        {
            _dbContext.ProductCategories.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
