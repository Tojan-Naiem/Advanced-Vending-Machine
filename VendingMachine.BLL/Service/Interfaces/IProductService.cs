using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.DTO.RequestDTO;

namespace VendingMachine.BLL.Service.Interfaces
{
    public interface IProductService
    {
        public Task<long> CreateFile(ProductRequest request);
        public Task<bool> DeleteFile(long id);
    }
}
