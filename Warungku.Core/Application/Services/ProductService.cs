using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warungku.Core.Application.Interfaces;
using Warungku.Core.Domain.DTOs;
using Warungku.Core.Domain.Entities;
using Warungku.Core.Infrastructure.Interfaces;

namespace Warungku.Core.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<ProductResponse> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse> CreateAsync(ProductRequest request)
        {   
            var product = _mapper.Map<Product>(request);
            var createdProduct = await _productRepository.AddAsync(product);
            return _mapper.Map<ProductResponse>(createdProduct);  
        }

        public async Task<ProductResponse> UpdateAsync(int id, ProductRequest request)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null) return null;

            _mapper.Map(request, existingProduct);
            var updatedProduct = await _productRepository.UpdateAsync(existingProduct);
            return _mapper.Map<ProductResponse>(updatedProduct);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductResponse>> GetByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.FindAsync(p => p.CategoryId == categoryId);
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }
    }
}
