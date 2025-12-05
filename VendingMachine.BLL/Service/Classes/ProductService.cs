using Azure;
using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VendingMachine.BLL.Service.Interfaces;
using VendingMachine.DAL.DTO.RequestDTO;
using VendingMachine.DAL.DTO.ResponseDTO;
using VendingMachine.DAL.Model;
using VendingMachine.DAL.Repository.Classes;
using VendingMachine.DAL.Repository.Interfaces;

namespace VendingMachine.BLL.Service.Classes
{
    public class ProductService : IProductService
    {
        private readonly IFileService _fileService;
        private readonly IProductRepository _productRepository;
        private readonly IDistributedCache _cache;

        public ProductService(
            IProductRepository productRepository,
            IFileService fileService,
             IDistributedCache cache
            ) 
        {
            _productRepository = productRepository;
            _fileService = fileService;
            _cache = cache;

        }
        public async Task<long> CreateFile(ProductRequest request)
        {
            var entity = request.Adapt<Product>();
            if (request.MainImage is not null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                entity.MainImage = imagePath;
            }
            await _productRepository.SaveAsync(entity);
            return entity.Id;
        }
        public async Task<bool> DeleteFile(long id)
        {
            var entity = await _productRepository.GetProductAsync(id);
            bool successDelete = await _fileService.DeleteAsync(entity!.MainImage);
            if (successDelete is false)
                throw new Exception("Error");
            await _productRepository.RemoveAsync(entity);
            return true;
        }
        public async Task<bool> UpdateProductAsync(long id,ProductRequest request)
        {
            var entity =await _productRepository.GetProductAsync(id);
            if (entity is null) return false;
            entity.Name = request.Name!;
            entity.Price = (decimal)request.Price!;
            entity.Quantity = request.Quantity;
            if (request.MainImage is not null)
            {
                if (!string.IsNullOrEmpty(entity.MainImage))
                {
                    await _fileService.DeleteAsync(entity.MainImage);
                }
                string newImage = await _fileService.UploadAsync(request.MainImage);
                entity.MainImage = newImage;

            }
            await _productRepository.SaveChangesInDatabase();
            return true;
        }
        public async Task<List<ProductResponse>> GetAllAsync()
        {
            var cacheKey = "all_products";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                Console.WriteLine("Hello from redis");
                return JsonSerializer.Deserialize<List<ProductResponse>>(cachedData)!;
            }
            var entities = await _productRepository.GetProductsAsync();

            var options = new DistributedCacheEntryOptions()
          .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(entities), options);


            return entities.Adapt<List<ProductResponse>>();
        }
    
        public async Task<ProductResponse?> GetByIdAsync(long id)
        {
            var entity =await _productRepository.GetProductAsync(id);
            return entity is null ? null : entity.Adapt<ProductResponse>();
        }
        public async Task<bool> ToggleStatusAsync(long id)
        {
            var entity = await _productRepository.GetProductAsync(id);
            if (entity is null) return false;
            entity.Status = (entity.Status == Status.Active) ? Status.In_Active : Status.Active;
            await _productRepository.SaveChangesInDatabase();
            return true;
        }

    

     
    }
}
