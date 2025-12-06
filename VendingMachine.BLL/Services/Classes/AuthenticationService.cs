using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VendingMachine.DAL.DTO.Response;
using VendingMachine.DAL.DTO.Request;
using VendingMachine.DAL.Model;
using VendingMachine.BLL.Services.Interfaces;

namespace VendingMachine.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<Users> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<Users> _signInManager;

        public AuthenticationService(UserManager<Users> userManager, IConfiguration configuration, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }
        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new Exception("Invalid email or password.");
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new Exception("Email is not confirmed.");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid email or password.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
            if (result.Succeeded)
            {
                return new UserResponse
                {
                    Token = await CreateTokenAsync(user),
                };
            }
            else if (result.IsLockedOut)
            {
                throw new Exception("your account is locked");
            }
            else if (result.IsNotAllowed)
            {
                throw new Exception("Please confirm your email");
            }
            else
            {
                throw new Exception("Please confirm your email");
            }


        }

        public async Task<string> ConfirmEmail(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("User not found.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Email confirmed successfully.";
            }
            return "Email confirmation failed. Please try again later.";
        }

        private async Task<string> CreateTokenAsync(Users user)
        {
            var Claims = new List<Claim>()
            {
                new Claim("Email", user.Email),
                new Claim("Id", user.Id),
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim("Role", role));
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("jwt")["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: Claims,
                expires: DateTime.Now.AddDays(15),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
