﻿using Newtonsoft.Json;
using System;
using System.Web.Http;
using InventoryServices.Repository.Interfaces;
using System.Threading.Tasks;
using InventoryServices.Shared;

namespace InventoryServices.Controllers
{
    [RoutePrefix("api/Categories")]
    public class CategoriesController : ApiController
    {
        ICategoryRepository CategoryRepository;

        public CategoriesController(ICategoryRepository CategoryRepository)
        {
            this.CategoryRepository = CategoryRepository;
        }

        // GET api/<controller>
        [Route("")]
        public string Get()
        {
            return CategoryRepository.GetAll();
        }

        // GET api/<controller>/5
        [Route("{id:int}")]
        public async Task<string> Get(int id)
        {
            return await CategoryRepository.GetByIdAsync(id);
        }
        
        [Route("{category}")]
        public async Task<string> Get(string category)
        {
            return await CategoryRepository.GetByNameAsync(category);
        }


        [Route("Create")]
        [HttpPost]
        public async Task<string> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                return await CategoryRepository.CreateCategoryAsync(category);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Invalid Model data")));
                return json;
            }           
        }

        [Route("Modify")]
        [HttpPut]
        public async Task<string> ModifyCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                return await CategoryRepository.ModifyCategoryAsync(category);
            }
            else
            {
                string json = JsonConvert.SerializeObject(Helper.CreateErrorResponse(new Exception("Invalid Model data")));
                return json;
            }

        }

        [Route("Delete/{id:int}")]
        [HttpDelete]
        public async Task<string> DeleteCategory(int id)
        {
            return await CategoryRepository.DeleteCategoryAsync(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}