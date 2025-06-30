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
    public class TransactionService : ITransactionService
    {
        private readonly IGenericRepository<Transaction> _genericRepository;
        private readonly IMapper _mapper;

        public TransactionService(IGenericRepository<Transaction> genericRepository, IMapper mapper)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }
        public async Task<TransactionResponse> CreateAsync(TransactionRequest request)
        {
            var transaction = _mapper.Map<Transaction>(request);
            var createdTransaction = await _genericRepository.AddAsync(transaction);
            return _mapper.Map<TransactionResponse>(createdTransaction);
        }

        public async Task<IEnumerable<TransactionResponse>> GetAllAsync()
        {
            var transactions = await _genericRepository.GetAllAsync();
            var list = new List<TransactionResponse>();
            foreach (var item in transactions)
            {
                var temp = new TransactionResponse()
                {
                    Date = item.Date.ToString("dd-MMMM-yyyy"),
                    Discount = item.Discount,
                    GrandTotal = item.GrandTotal,
                    Total = item.Total,
                    User = item.User,
                    Voucher = item.Voucher
                };
                list.Add(temp);

            }



            return list;
        }
    }
}
