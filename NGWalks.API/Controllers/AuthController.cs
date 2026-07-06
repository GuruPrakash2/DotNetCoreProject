using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DT;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };
            var identityRes = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityRes.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityRes = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityRes.Succeeded)
                    {
                        return Ok("User registered successfully");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginRequestDto) 
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user != null)
            {
                var isValid = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (isValid)
                {
                    //create Token
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }

            return BadRequest("UserName or Password is incorrect");

        }

    }
}
