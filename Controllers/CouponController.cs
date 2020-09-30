using System;
using System.Collections.Generic;
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
    //mohammad1
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICPRepository _repo;
        private readonly IMapper _mapper;
        private readonly DataContext _data;

        public CouponController(ICPRepository repo, IMapper mapper, DataContext data)
        {
            _data = data;
            _mapper = mapper;
            _repo = repo;
        }


        [HttpGet("{id}", Name = "GetCoupan")]
        public async Task<IActionResult> GetCoupan(int id)
        {

            var coupanFromRepo = await _repo.GetCoupan(id);
            var coupan = _mapper.Map<CouponReturnDTO>(coupanFromRepo);
            if ((coupanFromRepo == null))
                return NotFound();
            return Ok(coupan);
        }


        [HttpGet]
        public async Task<IActionResult> GetCoupans([FromQuery]DiscountParams discountParams)
        {
            var coupans = await _repo.GetCoupans(discountParams);
            var coupansToReturn = _mapper.Map<IEnumerable<CouponReturnDTO>>(coupans);
            Response.AddPagination(coupans.CurrentPage, coupans.PageSize, coupans.TotalCount, coupans.TotalPages);

            return Ok(coupansToReturn);
        }

        [Authorize(Policy = "ModerateSupplierRole")]
        [Authorize(Policy = "DiscountCoupons")]
        [HttpPost]
        public async Task<IActionResult> CreateCoupan(CouponRegisterDTO discountRegisterDTO)
        {
            var coupan = _mapper.Map<Coupon>(discountRegisterDTO);
            _repo.Add(coupan);
            if (await _repo.SaveAll())
            {
                var coupantoReturn = _mapper.Map<CouponReturnDTO>(coupan);
                return CreatedAtRoute("GetCoupan", new { id = coupan.CoupanId }, coupantoReturn);
            }
            throw new Exception("حدث مشكلة في حفظ كود الخصم الجديده");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoupan(int id, CouponUpdateDTO couponUpdateDTO)
        {
            var coupanFromRepo = await _repo.GetCoupan(id);
            _mapper.Map(couponUpdateDTO, coupanFromRepo);
            if (await _repo.SaveAll())
            {
                var coupantoReturn = _mapper.Map<CategoryReturnDTO>(coupanFromRepo);
                return CreatedAtRoute("GetCoupan", new { id = coupanFromRepo.CoupanId }, coupantoReturn);
            }
            throw new Exception($"حدثت مشكلة في تعديل بيانات الخصم  {id}");
        }



        [Authorize(Policy = "ModerateSupplierRole")]
        [Authorize(Policy = "DiscountCoupons")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupan(int id)
        {
            var coupanFromRepo = await _repo.GetCoupan(id);
            _repo.Delete(coupanFromRepo);
            if (await _repo.SaveAll())
                return Ok();
            return BadRequest("لم يتم حذف يرومو كود");
        }




    }
}