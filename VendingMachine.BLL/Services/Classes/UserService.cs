using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using VendingMachine.BLL.Services.Interfaces;
using VendingMachine.DAL.DTO.Response;
using VendingMachine.DAL.Model;
using VendingMachine.DAL.Repositories.Interfaces;

namespace VendingMachine.BLL.Services.Classes
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<Users> _userManager;

        public UserService(IUserRepository userRepository, UserManager<Users> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    RoleName = role.FirstOrDefault()
                });
            }
            return userDtos;
        }

        public async Task<UserDto> GetByIdAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user.Adapt<UserDto>();
        }
    }
}
