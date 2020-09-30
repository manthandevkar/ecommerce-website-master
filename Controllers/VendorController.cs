using System;
using System.Collections.Generic;
using System.Linq;
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
    public class VendorController : Controller
    {
        private readonly ICPRepository _repo;
        private readonly IMapper _mapper;
        private readonly DataContext _data;

        public VendorController(ICPRepository repo, IMapper mapper, DataContext data)
        {
            _data = data;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}", Name = "GetVendor")]
        public async Task<IActionResult> GetVendor(int id)
        {
            var vendorFromRepo = await _repo.GetVendor(id);
            var vendor = _mapper.Map<VendorReturnDTO>(vendorFromRepo);
            if ((vendorFromRepo == null))
                return NotFound();
            return Ok(vendor);
        }

        [HttpGet]
        public async Task<IActionResult> GetVendors()
        {
            var vendors = await _repo.GetVendors();
            var vendorsToReturn = _mapper.Map<IEnumerable<VendorReturnDTO>>(vendors);
            return Ok(vendorsToReturn);
        }

        [Authorize(Policy = "EstablishAStore")]
        [HttpPost]
        public async Task<IActionResult> CreateVendor(VendorRegisterDTO vendorRegisterDTO)
        {
            var vendor = _mapper.Map<Vendor>(vendorRegisterDTO);
            _repo.Add(vendor);
            if (await _repo.SaveAll())
            {
                var vendorToReturn = _mapper.Map<VendorReturnDTO>(vendor);
                return CreatedAtRoute("GetVendor", new { id = vendor.Id }, vendorToReturn);
            }
            throw new Exception("حدث مشكلة في حفظ Vendor");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, VendorUpdateDTO vendorUpdateDTO)
        {
            var vendorFromRepo = await _repo.GetVendor(id);
            _mapper.Map(vendorUpdateDTO, vendorFromRepo);
            if (await _repo.SaveAll())
            {
                var vendorToReturn = _mapper.Map<VendorReturnDTO>(vendorFromRepo);
                return CreatedAtRoute("GetVendor", new { id = vendorFromRepo.Id }, vendorToReturn);
            }
            throw new Exception($"حدثت مشكلة في تعديل بيانات Vendor  {id}");
        }


        [Authorize(Policy = "EstablishAStore")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var vendorFromRepo = await _repo.GetVendor(id);
            _repo.Delete(vendorFromRepo);
            if (await _repo.SaveAll())
                return Ok();
            return BadRequest("لم يتم حذف Vendor");
        }

    }
}
