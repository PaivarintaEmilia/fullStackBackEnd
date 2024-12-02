using API.Data.Dtos;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService service, IMapper mapper) : ControllerBase
    {

        // Kaikkien käyttäjien hakuun
        [HttpGet] // Kertoo, että kyseessä on route_handler
        public async Task<ActionResult<List<UserResDto>>> GetAllUsers()
        {
            var users = await service.GetAll();
            return Ok(
                mapper.Map<List<UserResDto>>(users)
            );
        }

        // Login
        [HttpPost("login")]
        public async Task<ActionResult<LoginResDto>> Login(LoginReqDto req)
        {
            var token = await service.Login(req);
            return Ok(token);
        }

        // Register
        [HttpPost("register")]
        public async Task<ActionResult<UserResDto>> Register(AddUserReqDto req)
        {
            var user = await service.Register(req);

            if (user == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Detail = "Couldn't create new user"
                });
            }


            return Ok(
                mapper.Map<UserResDto>(user)
            );
        }

    }
}
