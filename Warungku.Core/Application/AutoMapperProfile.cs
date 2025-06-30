using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warungku.Core.Domain.DTOs;
using Warungku.Core.Domain.Entities;

namespace Warungku.Core.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //transaction mapping
            CreateMap<TransactionRequest, Transaction>();
            CreateMap<Transaction, TransactionResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            // pos mapping
            CreateMap<PosRequest, PointOfSale>();
            CreateMap<PointOfSale, PosResponse>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));

            // Category mappings
            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products.Count));

            // Product mappings
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
