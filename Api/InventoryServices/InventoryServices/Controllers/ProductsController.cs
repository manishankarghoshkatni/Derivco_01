using Newtonsoft.Json;
using System;
using System.Web.Http;
using InventoryServices.Repository.Interfaces;
using System.Threading.Tasks;
using InventoryServices.Shared;

namespace InventoryServices.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [Route("")]
        public string Get()
        {
            return productRepository.GetAll();
        }

        // GET api/<controller>/5
        [Route("{id:int}")]
        public async Task<string> Get(int id)
        {
            return await productRepository.GetByIdAsync(id);
        }

        [Route("{productName}")]
        public async Task<string> Get(string productName)
        {
            return await productRepository.GetByNameAsync(productName);
        }

        [Route("Category/{id:int}")]
        public async Task<string> GetForCategoryId(int id)
        {
            return await productRepository.GetByCategoryAsync(id);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<string> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                return await productRepository.CreateProductAsync(product);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Invalid Model data")));
                return json;
            }

        }

        [Route("Modify")]
        [HttpPut]
        public async Task<string> ModifyProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                return await productRepository.ModifyProductAsync(product);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Invalid Model data")));
                return json;
            }

        }

        [Route("Delete/{id:int}")]
        [HttpDelete]
        public async Task<string> DeleteProduct(int id)
        {
            return await productRepository.DeleteProductAsync(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                productRepository.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}

