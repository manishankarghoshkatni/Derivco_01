using Newtonsoft.Json;
using System;
using System.Web.Http;
using InventoryServices.Repository.Interfaces;
using System.Threading.Tasks;
using InventoryServices.Shared;

namespace InventoryServices.Controllers
{
    [RoutePrefix("api/Units")]
    public class UnitsController : ApiController
    {

        IUnitRepository UnitRepository;

        public UnitsController(IUnitRepository UnitRepository)
        {
            this.UnitRepository = UnitRepository;
        }

        // GET api/<controller>
        [Route("")]
        public string Get()
        {
            return UnitRepository.GetAll();
        }

        // GET api/<controller>/5
        [Route("{id:int}")]
        public async Task<string> Get(int id)
        {
            return await UnitRepository.GetByIdAsync(id);
        }

        [Route("{unit}")]
        public async Task<string> Get(string unit)
        {
            return await UnitRepository.GetByNameAsync(unit);
        }


        [Route("Create")]
        [HttpPost]
        public async Task<string> CreateUnit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                return await UnitRepository.CreateUnitAsync(unit);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Invalid Model data")));
                return json;
            }

        }

        [Route("Modify")]
        [HttpPut]
        public async Task<string> ModifyUnit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                return await UnitRepository.ModifyUnitAsync(unit);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Invalid Model data")));
                return json;
            }

        }

        [Route("Delete/{id:int}")]
        [HttpDelete]
        public async Task<string> DeleteUnit(int id)
        {
            return await UnitRepository.DeleteUnitAsync(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
