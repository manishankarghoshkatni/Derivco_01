
using System.Threading.Tasks;

namespace InventoryServices.Repository.Interfaces
{
    public interface IProductRepository
    {
        string GetAll();
        Task<string> GetByIdAsync(int id);
        Task<string> GetByNameAsync(string productName);
        Task<string> GetByCategoryAsync(int categoryId);
        Task<string> CreateProductAsync(Product product);
        Task<string> ModifyProductAsync(Product product);
        Task<string> DeleteProductAsync(int productId);
        void Dispose();
    }
}