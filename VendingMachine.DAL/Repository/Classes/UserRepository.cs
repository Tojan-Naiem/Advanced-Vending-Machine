using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VendingMachine.DAL.Model;
using VendingMachine.DAL.Repository.Interfaces;

namespace VendingMachine.DAL.Repository.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<Users> _userManager;

        public UserRepository(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<Users>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<Users> GetByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}
