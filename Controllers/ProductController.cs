using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CP.API.Data;
using CP.API.Dto;
using CP.API.Helpers;
using CP.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAMMAPP.API.Helpers;

namespace CP.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICPRepository _repo;
        private readonly IMapper _mapper;
        private readonly DataContext _data;

        public ProductController(ICPRepository repo, IMapper mapper, DataContext data)
        {
            _data = data;
            _mapper = mapper;
            _repo = repo;
        }


        [HttpGet("{id}", Name = "GetProduct")]//done
        public async Task<IActionResult> GetProduct(int id)
        {


            var categoryFroRepo = await _repo.GetProduct(id);

            var category = _mapper.Map<ProductReturnDTO>(categoryFroRepo);

            return Ok(category);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParams productParams)
        {
            var products = await _repo.GetProducts(productParams);
            var productsToReturn = _mapper.Map<IEnumerable<ProductReturnDTO>>(products);
            Response.AddPagination(products.CurrentPage, products.PageSize, products.TotalCount, products.TotalPages);
            return Ok(productsToReturn);
        }
        [AllowAnonymous]
        [HttpGet("GetAllProduct")]//done
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _repo.GetAllProducts();
            var productsToReturn = _mapper.Map<IEnumerable<ProductReturnDTO>>(products);
            return Ok(productsToReturn);
        }
        [Authorize(Policy = "EstablishAStore")]
        [Authorize(Policy = "Products")]
        [HttpPost("{VendorId}/{SubCategoryId}")]//done
        public async Task<IActionResult> CreateProduct(int VendorId, int subCategoryId, ProductRegisterDTO productRegisterDTO)
        {
            var sectionFromRepo = await _repo.GetSubCategory(subCategoryId);
            productRegisterDTO.SubCategoryId = sectionFromRepo.subCategoryID;
            var vendorFromRepo = await _repo.GetVendor(VendorId);
            productRegisterDTO.VendorId= vendorFromRepo.Id;

            var product = _mapper.Map<Product>(productRegisterDTO);
            _repo.Add(product);
            if (await _repo.SaveAll())
            {
                var productToReturn = _mapper.Map<ProductReturnDTO>(product);
                return CreatedAtRoute("GetProduct", new { id = product.ProductId }, productToReturn);
            }
            throw new Exception("حدث مشكلة في حفظ Product");
        }

        [Authorize(Policy = "EstablishAStore")]
        [Authorize(Policy = "Products")]
        [HttpDelete("{id}", Name = "DeleteProduct")]  //why id for user => //for check if the user Authorize or not before write asp role
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var categoryFroRepo = await _repo.GetProduct(id);
            _repo.Delete(categoryFroRepo);
            if (await _repo.SaveAll())
                return Ok();
            return BadRequest("لم يتم حذف Product");
        }

        [HttpPut("{supplierId}/{id}")]//done 
        public async Task<IActionResult> UpdateProduct(int supplierId, int id, ProductForUpdateDTO productForUpdateDTO)
        {
            if (supplierId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var productFromRepo = await _repo.GetProduct(id);
            var product = _mapper.Map(productForUpdateDTO, productFromRepo);
            if (await _repo.SaveAll())
            {
                var productToReturn = _mapper.Map<ProductReturnDTO>(product);
                return CreatedAtRoute("GetProduct", new { id = product.ProductId }, productToReturn);
            }

            throw new Exception($"حدثت مشكلة في تعديل بيانات المنتج رقم {id}");

        }

        [HttpGet("GetCountProduct")]//Done
        public async Task<IActionResult> GetCountProduct()
        {
            var count = await _repo.GetProductCount();
            return Ok(count);
        }



    }
}