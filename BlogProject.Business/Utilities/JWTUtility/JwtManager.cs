using BlogProject.Business.StringInfos;
using BlogProject.Entities.Concrete;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogProject.Business.Utilities.JWTUtility
{
    public class JWTManager : IJwtService
    {
        private readonly IOptions<JWTInfo> _optionsJwt;
        public JWTManager(IOptions<JWTInfo> optionsJwt)
        {
            _optionsJwt = optionsJwt;
        }
        public JwtToken GenerateJWTToken(AppUser appUser)
        {
            var jwtInfo = _optionsJwt.Value;
            var bytes = Encoding.UTF8.GetBytes(jwtInfo.SecurityKey); //startup la aynı olmalı 
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken  jwtSecurityToken = new JwtSecurityToken(issuer: jwtInfo.Issuer, audience: jwtInfo.Audience,
                notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(jwtInfo.TokenExpiration), signingCredentials: credentials, claims: GetClaims(appUser)) ;

            JwtToken jwtToken = new JwtToken();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            jwtToken.Token = handler.WriteToken(jwtSecurityToken);
            return jwtToken; 

        }


        private List<Claim> GetClaims(AppUser appUser)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, appUser.UserName));


            return claims;

        }
    }
}
