using AutoMapper;
using Nibo.ExtractBank.Domain.Dto;
using Nibo.ExtractBank.Domain.Entitie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nibo.ExtractBank.Api.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<TransactionBank, TransactionBankDto>().ReverseMap();

            CreateMap<ExtractBankTransaction, ExtractBankTransactionDto>().ReverseMap();

        }
    }
}
