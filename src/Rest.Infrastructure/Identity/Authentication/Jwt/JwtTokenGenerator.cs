﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rest.Infrastrucutre.Identity.Authetication.Jwt;

public static class JwtTokenGenerator
{
    public static string Generate(JwtSettings jwtSettings, IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
        {
            Issuer = jwtSettings.Issuer,
            Subject = new ClaimsIdentity(claims),
            Audience = jwtSettings.Audience,
            Expires = DateTime.Now.AddHours(jwtSettings.ExpirationInHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });
        return tokenHandler.WriteToken(token);
    }
}