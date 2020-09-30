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
using Microsoft.Extensions.Options;
using SAMMAPP.API.Helpers;
using Stripe;

namespace CP.API.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICPRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<StripeSettings> _stripeSettings;

        public UserController(ICPRepository repo, IMapper mapper,IOptions<StripeSettings> stripeSettings)
        {
            _repo = repo;
            _mapper = mapper;
            _stripeSettings = stripeSettings;
        }
       

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var userFroRepo = await _repo.GetUser(id);
            var user = _mapper.Map<UserReturnDTO>(userFroRepo);
            return Ok(user);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]//FromReturnWithSearch
        public async Task<IActionResult> GetUsers([FromQuery] SupplierParams supplierParams)
        { 
            var users = await _repo.GetUsers(supplierParams);
            var usersToReturn = _mapper.Map<IEnumerable<UserReturnDTO>>(users);
            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(usersToReturn);
        }



        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)

        {
            var userFromRepo = await _repo.GetUser(id);
            _repo.Delete(userFromRepo);
            if (await _repo.SaveAll())
                return Ok();
            return BadRequest("فشل في حذف");
        }



        [AllowAnonymous]//fromAdmin
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRegisterDTO userRegisterDTO)
        {
            var user = _mapper.Map<User>(userRegisterDTO);
            _repo.Add(user);
            if (await _repo.SaveAll())
            {
                var userToReturn = _mapper.Map<UserReturnDTO>(user);
                return CreatedAtRoute("GetUser", new { id = user.Id }, userToReturn);
            }
            throw new Exception("حدث مشكلة في حفظ الرسالة الجديده");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDTO userForUpdateDTO)
        {
            var userFromRepo = await _repo.GetUser(id);
            _mapper.Map(userForUpdateDTO, userFromRepo);
            if (await _repo.SaveAll())
                return Ok();
            throw new Exception($"حدثت مشكلة في تعديل بيانات التاجر رقم {id}");
        }


       

    }
}