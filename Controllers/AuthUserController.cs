using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
//using Amazon.Runtime.Internal.Util;
using AutoMapper;
using CP.API.Data;
using CP.API.Dto;
using CP.API.Model;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit.Core;
//using Serilog.Core;

namespace CP.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
       
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        public AuthUserController( UserManager<User> userManager,  SignInManager<User> signInManager, IConfiguration config, IMapper mapper,
          IEmailService emailService )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _mapper = mapper;
            _emailService = emailService;
        }

    [Route("ss")]
    public  IActionResult Sin()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO supplierRegisterDTO)
        {
            var userToCreate = _mapper.Map<User>(supplierRegisterDTO);
            var reslut = await _userManager.CreateAsync(userToCreate, supplierRegisterDTO.Password);
            var supplierToReturn = _mapper.Map<UserReturnDTO>(userToCreate); 
            if (reslut.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(userToCreate);
                var link = Url.Action(nameof(VerifyEmail), "AuthUser" , new { userId = userToCreate.Id,code },Request.Scheme,Request.Host.ToString());
                await _emailService.SendAsync(userToCreate.Email, "Email Verify", $"<a href=\"{link}\">Verify Email</a>" , true);
                return CreatedAtRoute("GetUser", new { controller = "User", id = userToCreate.Id }, supplierToReturn);
            }
            return BadRequest(reslut.Errors);
        }

        public async Task<IActionResult> VerifyEmail(string userId, string code )
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest();
            var result =  await _userManager.ConfirmEmailAsync(user, code);
            if(result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();  
        }
 

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDTO.Password, false);
            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.FirstOrDefaultAsync(
                    s => s.Email == userLoginDTO.Email.ToUpper()
                );
                var supplierToReturn = _mapper.Map<UserReturnDTO>(appUser);
                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,
                    supplier = supplierToReturn
                });
            }
            return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim (ClaimTypes.Role,role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}