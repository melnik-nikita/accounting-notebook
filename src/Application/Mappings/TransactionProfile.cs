using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionBody>();
            CreateMap<TransactionBody, Transaction>();
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
