using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.DTO.ResponseDTO;

namespace VendingMachine.BLL.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> ConfirmEmail(string token, string userId);
        Task<UserResponse> LoginAsync(LoginRequest request);

    }
}
