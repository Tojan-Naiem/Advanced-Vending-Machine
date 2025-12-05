using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.DTO.ResponseDTO;

namespace VendingMachine.BLL.Service.Interfaces
{
    public interface IProductService
    {
        public Task<long> CreateFile(ProductRequest request);
        public Task<bool> DeleteFile(long id);
        public Task<bool> UpdateProductAsync(long id, ProductRequest request);
        public Task<List<ProductResponse>> GetAll();
        public ProductResponse? GetById(long id);
        public Task<bool> ToggleStatus(long id);
    }
}
