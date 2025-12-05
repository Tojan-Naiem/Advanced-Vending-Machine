using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.BLL.Service.Interfaces;

namespace VendingMachine.BLL.Service.Classes
{
    public class FileService : IFileService
    {
        public async Task<string> UploadAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                return fileName;
            }
            throw new Exception("error");
        }
        public async Task<bool> DeleteAsync(string fileName)
        {
            if (fileName == null)
                throw new Exception("error");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
            Console.WriteLine("File Path : " + filePath);
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.IsReadOnly)
            {
                fileInfo.IsReadOnly = false;
                Console.WriteLine("File was read-only, fixed it.");
            }
            try
            {
                File.Delete(filePath);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return true;

        }
    }
}
