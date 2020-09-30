using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CP.API.Data;
using CP.API.Dto;
using CP.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CP.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ICPRepository _repo;
        private readonly IMapper _mapper;
        private readonly DataContext _data;

        public SubCategoryController(ICPRepository repo, IMapper mapper, DataContext data)
        {
            _data = data;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}", Name = "GetSubCategory")]
        public async Task<IActionResult> GetSubCategory(int id)
        {
            var sectionFroRepo = await _repo.GetSubCategory(id);
            var sections = _mapper.Map<SubCategoryReturnDTO>(sectionFroRepo);
            if ((sectionFroRepo == null))
                return NotFound();
            return Ok(sectionFroRepo);
        }

        [HttpGet("{CatgeoryId}", Name = "GetSubCategoryWhereCatgeoryId")]
        public async Task<IActionResult> GetSubCategoryWhereCatgeoryId(int CatgeoryId)
        {
            var sectionFroRepo = await _repo.GetSubCategoryWhereCatgeoryId(CatgeoryId);
            var sections = _mapper.Map <IEnumerable<SubCategoryReturnDTO>>(sectionFroRepo);
            if ((sectionFroRepo == null))
                return NotFound();
            return Ok(sections);
        }


        [HttpGet]
        public async Task<IActionResult> GetSubCategories()
        {
            var sections = await _repo.GetSubCategorys();
            var sectionsToReturn = _mapper.Map<IEnumerable<SubCategoryReturnDTO>>(sections);
            return Ok(sectionsToReturn);
        }

        [Authorize(Policy = "EstablishAStore")]
        [HttpPost("{categoryId}")]
        public async Task<IActionResult> CreateSubCategory(int categoryId, SubCategoryRegisterDTO SubCategoryRegisterDTO)
        {
            var getCategory = await _repo.GetCategory(categoryId);
            SubCategoryRegisterDTO.CategoryId = getCategory.CategoryId;
            var subCategory = _mapper.Map<SubCategory>(SubCategoryRegisterDTO);
            _repo.Add(subCategory);
            if (await _repo.SaveAll())
            {
                var SubCategoryToReturn = _mapper.Map<SubCategoryReturnDTO>(subCategory);
                return CreatedAtRoute("GetSubCategory", new { id = subCategory.subCategoryID }, SubCategoryToReturn);
            }
            throw new Exception("حدث مشكلة في حفظ الفئة الجديده");
        }

        [Authorize(Policy = "EstablishAStore")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var subCategoryFroRepo = await _repo.GetSubCategory(id);
            _repo.Delete(subCategoryFroRepo);
            if (await _repo.SaveAll())
                return Ok();
            return BadRequest("لم يتم حذف الفئة");
        }


        [HttpPut("{id}/{categoryId}")]
        public async Task<IActionResult> UpdateSubCategory(int id, int categoryId, SubCategoryUpdateDTO subCategoryUpdateDTO)
        {
            var subCategoryFroRepo = await _repo.GetSubCategory(id);
            var getCategory = await _repo.GetCategory(categoryId);
            subCategoryUpdateDTO.CategoryId = getCategory.CategoryId;
            _mapper.Map(subCategoryUpdateDTO, subCategoryFroRepo);
            if (await _repo.SaveAll()) //supplier=null
            {
                var SubCategoryToReturn = _mapper.Map<SubCategoryReturnDTO>(subCategoryFroRepo);
                return CreatedAtRoute("GetSubCategory", new { id = subCategoryFroRepo.subCategoryID }, SubCategoryToReturn);
            }
            throw new Exception($"حدثت مشكلة في تعديل بيانات الفئة  {id}");
        }
    }
}