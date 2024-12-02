using System;
using API.Data.Dtos;
using API.Models;

namespace API.Services.Interfaces;

// Tämä vastaa servicen factorya?
// Tämä on vain servicelle mallina mitä sen tulee sisältää
// Tulee vian tietotyyppi metodille ja parametreja se ottaa ja metodin nimi

public interface IUserService
{
    // Kaikkien usereiden hakuun
    Task<List<AppUser>> GetAll();

    // Login
    Task<LoginResDto?> Login(LoginReqDto req);

    // Register
    Task<AppUser?> Register(AddUserReqDto req);
    

}
