using Products.backend.Data;
using Products.backend.Models;
using Products.backend.Repo.IRepo;
using Products.Shared.Response;
using Serilog.Data;
using System.Numerics;

namespace Products.backend.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext dbContext;
        private readonly ILogger<ProductRepo> logger;

        public ProductRepo(AppDbContext dbContext,ILogger<ProductRepo> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await Task.FromResult(dbContext.Products.ToList());
        }

        public async Task<int> SaveChangesAsync()
        {            
            return await dbContext.SaveChangesAsync();
        }

        public async Task<Error?> CreateProduct(Product product)
        {
            var ProductExist = dbContext.Products.FirstOrDefault(c => c.ID == product.ID);
            
            if (ProductExist != null)
            {
                return ErrorList<Product>.Duplicates(product.Name);
            }            
            
            Product NewProduct = new Product
            {               
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Stock = product.Stock,
                Rating = product.Rating
            };

            var result = await dbContext.Products.AddAsync(NewProduct);
            if (result.Entity != null)                
            {
                return null; 
            }

            return ErrorList<Product>.CreationFailed(product.Name);

        }

        public async Task<Error?> DeleteProduct(int id)
        {
            var DeleteProduct = await Task.FromResult(dbContext.Products.FirstOrDefault(c => c.ID == id));
            if (DeleteProduct == null)
            {
                return ErrorList<Product>.NotFound(id); 
            }

            var result = dbContext.Products.Remove(DeleteProduct);
            if(result.Entity == null) 
            {
                return ErrorList<Product>.DeleteFailed(id);
            }

            return null;
        }
        public async Task<Error?> UpdateProduct(Product product)
        {
            var UpdateProduct = dbContext.Products.FirstOrDefault(c => c.ID == product.ID);
            if (UpdateProduct == null)
            {
                return ErrorList<Product>.NotFound(product.ID); 
            }

            UpdateProduct.Name = product.Name;
            UpdateProduct.Price = product.Price;
            UpdateProduct.Stock = product.Stock;
            UpdateProduct.Description = product.Description;
            UpdateProduct.Rating = product.Rating;
            UpdateProduct.CategoryId = product.CategoryId;

            var result = await Task.Run(()=>dbContext.Update(UpdateProduct));
            if (result.Entity == null)
                return ErrorList<Product>.UpdateFailed(product.Name);

            return null;
        }

        public async Task<Product?> GetProductById(int id)
        {
            var productResult = await Task.FromResult(dbContext.Products.FirstOrDefault(c => c.ID == id));
            return productResult;
        }
    }
}
