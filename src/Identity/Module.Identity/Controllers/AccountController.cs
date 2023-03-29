using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Module.Identity.Core.DataTransferObjects;
using Module.Identity.Core.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Module.Identity.Controllers {

    [Route("[module]/[controller]")]
    [ApiController]
    [Authorize]

    internal class AccountController : ControllerBase {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost, Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto) {
            try {
                if (string.IsNullOrEmpty(loginUserDto.UserName) ||
                string.IsNullOrEmpty(loginUserDto.Password))
                    return BadRequest("Username and/or Password not specified");

                var rs = await _accountService.CheckPasswordAsync(loginUserDto);
                if (rs.Succeeded) {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));

                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var claims = new[] {
                        new Claim(ClaimTypes.Name, loginUserDto.UserName),
                        new Claim(ClaimTypes.NameIdentifier, rs.Data.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    var jwtSecurityToken = new JwtSecurityToken(

                        issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                        audience: _configuration.GetValue<string>("Jwt:Audience"),
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(100),
                        signingCredentials: signinCredentials
                    );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
                }
            }
            catch {
                return BadRequest
                ("An error occurred in generating the token");
            }
            return Unauthorized();
        }

        [HttpGet, Route("hello")]
        public IActionResult Hello() {
            return Ok("Hello from Aurhorized Account Controller");
        }
    }
}
