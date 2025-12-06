using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.DTO.ResponseDTO;

namespace VendingMachine.BLL.Service.Interfaces
{
    public interface ICheckOutService
    {
        public Task<CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, HttpRequest httpRequest);

    }
}
