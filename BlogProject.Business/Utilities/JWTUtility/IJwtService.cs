using BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.Utilities.JWTUtility
{
    public  interface IJwtService
    {
        JwtToken GenerateJWTToken(AppUser appUser);
    }
}
