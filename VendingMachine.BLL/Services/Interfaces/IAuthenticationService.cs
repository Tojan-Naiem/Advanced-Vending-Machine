using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VendingMachine.DAL.DTO.Response;
using VendingMachine.DAL.DTO.Request;
using VendingMachine.DAL.Model;

namespace VendingMachine.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> ConfirmEmail(string token, string userId);
        Task<UserResponse> LoginAsync(LoginRequest request);

    }
}
