using CRUDApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync(bool includeDeleted = false);

    Task<Category> GetCategoryByIdAsync(int id);

    Task<Category> AddCategoryAsync(Category category);

    Task<bool> UpdateCategoryAsync(Category category);

    Task<bool> DeleteCategoryAsync(int id);

    Task<bool> PermanentlyDeleteCategoryAsync(int id);
}
