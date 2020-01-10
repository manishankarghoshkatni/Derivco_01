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

