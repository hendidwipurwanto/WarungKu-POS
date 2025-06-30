using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warungku.Core.Domain.DTOs;

namespace Warungku.Core.Application.Interfaces
{
    public interface IPointOfSaleService
    {
        Task<IEnumerable<PosResponse>> GetAllAsync();
        Task<PosResponse> GetByIdAsync(int id);
        Task<PosResponse> CreateAsync(PosRequest request);
        Task<PosResponse> UpdateAsync(int id, PosRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
