using Products.backend.Models;
using Products.Shared.Response;

namespace Products.backend.Repo.IRepo
{
    public interface ICategoryRepo
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category?> GetCategoryById(int CategoryId);
        Task<Category?> GetCategoryByName(string CategoryName);
        Task<Category> FindCategory(int? categoryid,string categoryName);
        Task<Error?> CreateCategory(Category category); 
        Task<Error?> UpdateCategory(Category category);
        Task<Error?> DeleteCategory(int CategoryId);
        Task<int> SaveChangesAsync();
    }
}
