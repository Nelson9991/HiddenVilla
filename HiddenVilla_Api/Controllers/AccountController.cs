using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess.Data;
using HiddenVilla_Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly APISettings _apiSettings;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager, IOptions<APISettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _apiSettings = options.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SingUp([FromBody] RegisterRequestDto registerRequestDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerRequestDto.Email,
                Email = registerRequestDto.Email,
                Name = registerRequestDto.Name,
                PhoneNumber = registerRequestDto.PhoneNo,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return BadRequest(new RegisterResponseDto
                {
                    IsRegisterationSuccessful = false,
                    Errors = errors
                });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, Sd.RoleCustomer);

            if (!roleResult.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return BadRequest(new RegisterResponseDto
                {
                    IsRegisterationSuccessful = false,
                    Errors = errors
                });
            }

            return StatusCode(201);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationDto authenticationDto)
        {
            var result =
                await _signInManager.PasswordSignInAsync(authenticationDto.UserName, authenticationDto.Password, true,
                    false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(authenticationDto.UserName);
                if (user is null)
                {
                    return Unauthorized(new AuthenticationResponseDto
                    {
                        IsAuthSuccessful = false,
                        Error = "Invalid Authentication"
                    });
                }

                // everything is valid and we need to login the user
                var sigInCreds = GetSigningCredentials();
                var claims = await GetUserClaims(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _apiSettings.ValidIssuer,
                    audience: _apiSettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: sigInCreds
                );

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new AuthenticationResponseDto
                {
                    IsAuthSuccessful = true,
                    Token = token,
                    UserDto = new UserDto()
                    {
                        Name = user.Name,
                        Id = user.Id,
                        Email = user.Email,
                        PhoneNo = user.PhoneNumber
                    }
                });
            }
            else
            {
                return Unauthorized(new AuthenticationResponseDto()
                {
                    IsAuthSuccessful = false,
                    Error = "Invalid Authentication"
                });
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha512);
        }

        private async Task<List<Claim>> GetUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            return claims;
        }
    }
}
