using CRUDApp.IRepository;
using CRUDApp.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUDApp.Repository
{

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            product.DateCreated = DateTime.UtcNow;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(string searchQuery,bool includeDeleted = false)
        {
            var query = _context.Products.Include(x => x.Category)
                                  .Where(c => includeDeleted || !c.IsDeleted); 

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(c => c.Name.Contains(searchQuery) ||
                                          c.Price.ToString().Contains(searchQuery) ||
                                          c.Category.Name.Contains(searchQuery));
            }

            return await query.ToListAsync();
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id && !p.IsDeleted);
            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId; 
            existingProduct.DateModified = DateTime.UtcNow;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            if (product == null)
            {
                return false;
            }

            product.IsDeleted = true;
            product.DateModified = DateTime.UtcNow;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PermanentlyDeleteProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

       
    }
}

