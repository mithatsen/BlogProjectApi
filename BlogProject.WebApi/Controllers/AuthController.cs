using AutoMapper;
using BlogProject.Business.Interfaces;
using BlogProject.Business.Utilities.JWTUtility;
using BlogProject.DTO.DTOs.AppUserDtos;
using BlogProject.Entities.Concrete;
using BlogProject.Entities.Dtos.AppUserDtos;
using JWTProject.Api.CustomFilters;
using JWTProject.Entities.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        public AuthController(IJwtService jwtService, IAppUserService appUserService, IMapper mapper)
        {
            _jwtService = jwtService;
            _appUserService = appUserService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> SignIn(AppUserLoginDto userLoginDto)
        {

            var appUser = await _appUserService.FindByUserNameAsync(userLoginDto.UserName);
            if (appUser == null)
            {
                return BadRequest("Kullanıcı adı veya şifre hatalı");
            }
            else
            {
                if (await _appUserService.CheckPasswordAsync(userLoginDto))
                {                   
                    return Created("", _jwtService.GenerateJWTToken(appUser));
                }
                return BadRequest("Kullanıcı adı veya şifre hatalı");
            }

        }
        [HttpPost("[action]")]
        [ValidModel]

        public async Task<IActionResult> SignUp(AppUserAddDto appUserAddDto)
        {
            var appUser = await _appUserService.FindByUserNameAsync(appUserAddDto.UserName);
            if (appUser != null)
            {
                return BadRequest("Bu kullanıcı adı kullanılmaktadır.Lütfen farklı bir kullanıcı adı deneyiniz");
            }
            await _appUserService.AddAsync(_mapper.Map<AppUser>(appUserAddDto));

            var user = await _appUserService.FindByUserNameAsync(appUserAddDto.UserName);
           
            return Created("", appUserAddDto);
        }
        [HttpGet("[action]")]
        [Authorize]       
        public async Task<IActionResult> ActiveUser()
        {
            var user= await _appUserService.FindByUserNameAsync(User.Identity.Name);
            
            AppUserDto appUserDto = new AppUserDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Id=user.Id
                              
            };
            return Ok(appUserDto);
            
        }
    }
}
