using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Products.backend.Data;
using Products.backend.Models;
using Products.backend.Repo.IRepo;
using Products.Shared.Response;

namespace Products.backend.Repo
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext dbContext;
        private readonly ILogger<CategoryRepo> logger;

        public CategoryRepo(AppDbContext dbContext, ILogger<CategoryRepo> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<Error?> CreateCategory(Category category)
        {
            var CategoryExist = await dbContext.Categories.FirstOrDefaultAsync(c => (category.CategoryId!=null && c.CategoryId == category.CategoryId) || (!string.IsNullOrEmpty(category.CategoryName) && c.CategoryName == category.CategoryName));

            if (CategoryExist != null)
                return ErrorList<Category>.Duplicates(category.CategoryName);

            int maxValue = 1;

            if (dbContext.Categories.ToList().Count != 0)
                maxValue = await dbContext.Categories.MaxAsync(c => c.CategoryId) + 1;

            Category NewCategory = new Category
            {               
                CategoryName = category.CategoryName,
            };

            dbContext.Categories.Add(NewCategory);

            return null;
        }

        public async Task<Error?> DeleteCategory(int CategoryId)
        {
            var deleteCat = dbContext.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);
            if (deleteCat == null)
                return ErrorList<Category>.NotFound(CategoryId);

            var result = dbContext.Categories.Remove(deleteCat);
            if (result == null)
                return ErrorList<Category>.DeleteFailed(CategoryId);

            return null;

        }

        public async Task<Category> FindCategory(int? categoryid, string? categoryName)
        {
            Category resultCat = new Category();
            if (categoryid == null && string.IsNullOrEmpty(categoryName))
                categoryName = "NON";

            if (categoryid != null)
            {
                resultCat = await GetCategoryById(categoryid.Value);
                if (resultCat == null)
                {
                    resultCat = await GetCategoryByName(categoryName);
                    if (resultCat == null)
                    {
                        await CreateCategory(new Category { CategoryId = 0, CategoryName = categoryName });
                        await SaveChangesAsync();
                        resultCat = await GetCategoryByName(categoryName);
                    }
                }
            }
            else
            {
                resultCat = await GetCategoryByName(categoryName);
                if (resultCat == null)
                {
                    await CreateCategory(new Category { CategoryId = 0, CategoryName = categoryName });
                    resultCat = await GetCategoryByName(categoryName);
                }
            }
            return resultCat!;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await Task.FromResult(dbContext.Categories.ToList());
        }

        public async Task<Category?> GetCategoryById(int CategoryId)
        {
            return await Task.FromResult(dbContext.Categories.FirstOrDefault(c => c.CategoryId == CategoryId));
        }

        public async Task<Category?> GetCategoryByName(string CategoryName)
        {
            return await Task.FromResult(dbContext.Categories.FirstOrDefault(c => c.CategoryName == CategoryName));
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<Error?> UpdateCategory(Category category)
        {
            var updateCategory = dbContext.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (updateCategory == null) { return ErrorList<Category>.NotFound(category.CategoryId); }

            updateCategory.CategoryName = category.CategoryName;

            return null;
        }
    }
}
