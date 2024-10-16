using ProductCategory.DAL.Interfaces;
using ProductCategory.Domain.Entities;

namespace ProductCategory.DAL.Repositories
{
    public class ProductRepository : IBaseRepository<ProductEntity>
    {
        private ApplicationDBContext _dbContext;

        public ProductRepository(ApplicationDBContext dBContext) => _dbContext = dBContext;

        public async Task Create(ProductEntity entity)
        {
            await _dbContext.Products.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(ProductEntity entity)
        {
            _dbContext.Products.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<ProductEntity> GetAll()
        {
            return _dbContext.Products;
        }

        public async Task Update(ProductEntity entity)
        {
            _dbContext.Products.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
