using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Data.Dtos;
using API.Models;
using API.Services.Interfaces;
using API.Tools.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;
// TÄMÄ ON SERVICE
public class UserService(DataContext context, ITokenTool tokenCreator) : IUserService // Import implement interface
{

    // Kaikkien usereiden hakuun
    public async Task<List<AppUser>> GetAll()
    {
        var users = await context.Users.ToListAsync();
        return users;
    }

    public async Task<LoginResDto?> Login(LoginReqDto req)
    {
        // Haetaan user
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == req.UserName.ToLower());

        // Varmistetaan ettei ole null
        if(user == null) {
            return null;
        }
    
        // Encodataan saltti
        using var hmac = new HMACSHA512(user.PasswordSalt);

        // Lähetetty salasana muutetaan samaan muotoon hashatun salasanan kanssa
        var computedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(req.Password));

        // Tarkistetaan että käyttäjän salasana täsmää tähän loopilla
        for (int i = 0; i < computedPassword.Length; i++) {
            // Jos ei täsmää
            if(computedPassword[i] != user.HashedPassword[i]) {
                return null;
            }
        }

        // Jos täsmää, luodaan JWT token
        var token = tokenCreator.CreateToken(user);
        // Voi tulla nullable, joten tarkistetaan tämä vielä
        if(token == null) {
            return null;
        }

        // Tulee palauttaa LoginResDto
        return new LoginResDto
        {
            Token = token
        };
    }

    public async Task<AppUser?> Register(AddUserReqDto req)
    {
                // Salasanan hashaus tapahtuu täällä
        // using tarkoittaa, että se on vain tässä käytössä
        using var hmac = new HMACSHA512();
        // Luodaan käyttäjä
        var user = new AppUser
        {
            UserName = req.UserName,
            Role = "admin",
            PasswordSalt = hmac.Key,
            HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(req.Password))
        };

        // Tietokantaan lisäys
        context.Users.Add(user);
        // Commit
        await context.SaveChangesAsync();

        return user;
    }
}
