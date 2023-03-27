using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Security.Claims;
using TextEditor.Domain.Accounts.Entities;

public class JwtUtil
{
    public static string GetToken(IConfiguration Configuration, Account req)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            Configuration["Jwt:Issuer"],
            Configuration["Jwt:Audience"],
            new[]
            {
                        new Claim(ClaimTypes.Name, req.IdCard),
                        new Claim(ClaimTypes.Role, req.Role.Name)
            },
            expires: DateTime.Now.AddDays(15),
            signingCredentials: credentials
         );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}