using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using API.Tools.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Tools;

public class SymmetricToken(IConfiguration config) : ITokenTool
{
    public string? CreateToken(AppUser user)
    {
        // varmista, että appSettingsissä on 
        // samanlainen kirjoitusasu TokenKeylle
        var tokenKey = config["TokenKey"];
        if (tokenKey == null)
        {
            return null;
        }

        // varmista myös, että TokenKey on tarpeeksi pitkä
        if (tokenKey.Length < 64)
        {
            return null;
        }

        // String tulee muuttaa byte[] array muotoon
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        // Luodaan JWT:n allekirjoitus HMACSha512-algoritmilla
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Access tokenin data, eli vaatimuksia mitä käyttäjästä väitetään
        // Import on identityModel Token
        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role) // Tämäm ei ole jwt:n speksissä, ja meidän itse luoma vaatimus

        };

        // Luodaan token 
        var _token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: credentials);

        // Palautetaan token, tämä palauttaa varsinaisen merkkijonon tokenista
        return new JwtSecurityTokenHandler().WriteToken(_token);

    }
}