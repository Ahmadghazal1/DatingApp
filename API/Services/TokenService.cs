﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Data.Migrations;
using API.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }
    public string CreateToken(AppUser appUser)
    {
       var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.NameId , appUser.UserName)
       };
       var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
       var tokenDescriptor = new SecurityTokenDescriptor {
        Subject =  new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(7),
        SigningCredentials = creds
       }; 

       var tokenHandler = new JwtSecurityTokenHandler(); 
       var token = tokenHandler.CreateToken(tokenDescriptor); 

       return tokenHandler.WriteToken(token);
    }
}