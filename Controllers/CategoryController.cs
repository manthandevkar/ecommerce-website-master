using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CP.API.Data;
using CP.API.Dto;
using CP.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CP.API.Controllers
{
    //mohammad1
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICPRepository _repo;
        private readonly IMapper _mapper;
        private readonly DataContext _data;

        public CategoryController(ICPRepository repo, IMapper mapper, DataContext data)
        {
            _data = data;
            _mapper = mapper;
            _repo = repo;
        }


        //done 
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var categoryFroRepo = await _repo.GetCategory(id);
            var post = _mapper.Map<CategoryReturnDTO>(categoryFroRepo);
            if ((categoryFroRepo == null))
                return NotFound();
            return Ok(post);
        }

        //done 
        [Authorize(Policy = "EstablishAStore")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRegisterDTO categoryRegisterDTO)
        {
            var category = _mapper.Map<Category>(categoryRegisterDTO);
            _repo.Add(category);
            if (await _repo.SaveAll())
            {
                var categoryToReturn = _mapper.Map<CategoryReturnDTO>(category);
                return CreatedAtRoute("GetCategory", new { id = category.CategoryId }, categoryToReturn);
            }
            throw new Exception("حدث مشكلة في حفظ القسم الجديده");
        }
        //done 
        [HttpGet]
        public async Task<IActionResult> GetCategorys()
        {
            var categorys = await _repo.GetCategorys();
            var categorysToReturn = _mapper.Map<IEnumerable<CategoryReturnDTO>>(categorys);
            return Ok(categorysToReturn);
        }


        // done 
        [Authorize(Policy = "EstablishAStore")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryFroRepo = await _repo.GetCategory(id);
            _repo.Delete(categoryFroRepo);
            if (await _repo.SaveAll())
                return Ok();
            return BadRequest("لم يتم حذف القسم");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,CategoryUpdateDTO  categoryUpdate)
        {
            var categoryFromReop = await _repo.GetCategory(id);
            _mapper.Map(categoryUpdate, categoryFromReop);
            if (await _repo.SaveAll())
            {
                var categoryToReturn = _mapper.Map<CategoryReturnDTO>(categoryFromReop);
                return CreatedAtRoute("GetCategory", new { id = categoryFromReop.CategoryId }, categoryToReturn);
            }
            throw new Exception($"حدثت مشكلة في تعديل بيانات القسم  {id}");
        }
    }
}