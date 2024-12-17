using System;

namespace API.Data.Dtos;

public class AddUserReqDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }

}
