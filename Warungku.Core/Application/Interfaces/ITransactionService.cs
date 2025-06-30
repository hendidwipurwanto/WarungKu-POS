using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warungku.Core.Domain.DTOs;

namespace Warungku.Core.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionResponse>> GetAllAsync();

        Task<TransactionResponse> CreateAsync(TransactionRequest request);
    }
}
