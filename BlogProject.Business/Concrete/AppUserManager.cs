using BlogProject.Business.Interfaces;
using BlogProject.DataAccess.Interfaces;
using BlogProject.DTO.DTOs.AppUserDtos;
using BlogProject.Entities.Concrete;
using BlogProject.Entities.Dtos.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser>, IAppUserService
    {
        private readonly IGenericDal<AppUser> _genericDal;
        public AppUserManager(IGenericDal<AppUser> genericDal) : base(genericDal)
        {
            _genericDal = genericDal;
        }


        public async Task<bool> CheckPasswordAsync(AppUserLoginDto userLoginDto)
        {
            var appUser = await _genericDal.GetAsync(p => p.UserName == userLoginDto.UserName);
            return appUser.Password == userLoginDto.Password;
           
        }

        public async Task<AppUser> FindByUserNameAsync(string userName)
        {
            return await _genericDal.GetAsync(p => p.UserName == userName);
        }

        //public async Task<List<AppRole>> GetRolesByUserName(string userName)
        //{
        //    //return await _genericDal.GetRolesByUserName(userName);
        //    return null;
        //}
    }
}
