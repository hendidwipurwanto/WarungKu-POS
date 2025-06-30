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
    public class PointOfSaleService : IPointOfSaleService
    {
        private readonly IGenericRepository<PointOfSale> _genericRepository;
        private readonly IMapper _mapper;
        public PointOfSaleService(IGenericRepository<PointOfSale> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<PosResponse> CreateAsync(PosRequest request)
        {
            var pos = _mapper.Map<PointOfSale>(request);
            pos.SubTotal = (request.Quantity * request.Price);
            var createdPos = await _genericRepository.AddAsync(pos);
            return _mapper.Map<PosResponse>(createdPos);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _genericRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PosResponse>> GetAllAsync()
        {
            var poses = await _genericRepository.GetAllAsync();

            var list = new List<PosResponse>();

            foreach (var item in poses)
            {
                var temp = new PosResponse()
                {
                    Item = item.Product.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Subtotal = item.SubTotal
                };
                list.Add(temp);
                       
            }

            return list;
        }

        public async Task<PosResponse> GetByIdAsync(int id)
        {
            var pos = await _genericRepository.GetByIdAsync(id);
            return _mapper.Map<PosResponse>(pos);
        }

        public async Task<PosResponse> UpdateAsync(int id, PosRequest request)
        {
            var existingPos = await _genericRepository.GetByIdAsync(id);
            if (existingPos == null) return null;

            _mapper.Map(request, existingPos);
            var updatedPos = await _genericRepository.UpdateAsync(existingPos);
            return _mapper.Map<PosResponse>(updatedPos);
        }
    }
}
