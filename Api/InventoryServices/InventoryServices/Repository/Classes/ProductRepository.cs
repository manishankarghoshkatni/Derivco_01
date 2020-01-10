using System;
using System.Linq;
using InventoryServices.Repository.Interfaces;
using System.Threading.Tasks;
using InventoryServices.Shared;
using Newtonsoft.Json;
using System.Data.Entity;

namespace InventoryServices.Repository.Classes
{
    public class ProductRepository : IProductRepository
    {
        private InventoryEntities db = new InventoryEntities();

        public string GetAll()
        {
            string jsonResponse = "";

            try
            {
                var q = from p in db.Products
                        orderby p.ProductName 
                        select new
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            ProductDescription = p.ProductDescription,
                            CategoryId = p.CategoryId,
                            CategoryName=p.Category.CategoryName,
                            CategoryDescription=p.Category.CategoryDescription,
                            Price = p.Price,
                            Currency = p.Currency,
                            UnitId=p.Unit.UnitId,
                            UnitName=p.Unit.UnitName,
                            UnitDescription=p.Unit.UnitDescription 
                        };

                var result = q.ToArray();
                if (result.Count() > 0)
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse(result));
                }
                else
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateNoDataResponse());
                }
            }
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }

            return jsonResponse;
        }

        public async Task<string> GetByIdAsync(int id)
        {
            string jsonResponse = "";

            try
            {
                Product p = await db.Products.FindAsync(id);
                if (p != null)
                {
                    var result = new
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        ProductDescription = p.ProductDescription,
                        CategoryId = p.CategoryId,
                        CategoryName = p.Category.CategoryName,
                        CategoryDescription = p.Category.CategoryDescription,
                        Price = p.Price,
                        Currency = p.Currency,
                        UnitId = p.Unit.UnitId,
                        UnitName = p.Unit.UnitName,
                        UnitDescription = p.Unit.UnitDescription
                    };

                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateDataResponse(result));
                }
                else
                {
                    jsonResponse = JsonConvert.SerializeObject(Helper.CreateNoDataResponse());
                }
            }
            catch (Exception ex)
            {
                jsonResponse = JsonConvert.SerializeObject(Helper.CreateErrorResponse(ex));
            }
            return jsonResponse;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}