using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VendingMachine.DAL.DTO.ResponseDTO
{
    public class ProductResponse
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public string MainImage { get; set; } = string.Empty;
        public string MainImageUrl => $"https://localhost:7259/Images/{MainImage}";
    }
}
