using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace VendingMachine.DAL.DTO.RequestDTO
{
    public class ProductRequest
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public IFormFile? MainImage { get; set; }

    }
}
