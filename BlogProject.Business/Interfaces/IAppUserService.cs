using BlogProject.Business.Interfaces;
using BlogProject.DTO.DTOs.AppUserDtos;
using BlogProject.Entities.Concrete;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Business.Interfaces
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> FindByUserNameAsync(string userName);
        Task<bool> CheckPasswordAsync(AppUserLoginDto userLoginDto);
        //Task<List<AppRole>> GetRolesByUserName(string userName);
    }
}
