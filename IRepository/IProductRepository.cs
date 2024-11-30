using CRUDApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync(string searchQuery, bool includeDeleted = false);

    Task<Product> GetProductByIdAsync(int id);

    Task<Product> AddProductAsync(Product product);

    Task<bool> UpdateProductAsync(Product product);

    Task<bool> DeleteProductAsync(int id);

    Task<bool> PermanentlyDeleteProductAsync(int id);
}
