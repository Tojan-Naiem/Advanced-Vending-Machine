using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Model;

namespace VendingMachine.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<Users>> GetAllAsync();
        public Task<Users> GetByIdAsync(string userId);
    }
}
