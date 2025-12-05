using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.BLL.Service.Interfaces
{
    public interface IFileService
    {
        public Task<string> UploadAsync(IFormFile file);
        public Task<bool> DeleteAsync(string fileName);
    }
}
