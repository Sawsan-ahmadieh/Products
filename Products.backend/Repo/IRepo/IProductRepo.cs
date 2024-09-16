using Products.backend.Models;
using Products.Shared.Response;

namespace Products.backend.Repo.IRepo
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Error?> CreateProduct(Product product);
        Task<Error?> UpdateProduct(Product product);
        Task<Error?> DeleteProduct(int id);
        Task<int> SaveChangesAsync();
    }
}
