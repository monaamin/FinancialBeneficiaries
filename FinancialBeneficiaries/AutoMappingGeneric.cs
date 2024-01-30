using AutoMapper;
using FinancialManagementDataLayer.Entities;
using FinancialManagementServices.Models;

namespace FinancialManagementServices
{
    public class AutoMappingGeneric : Profile
    {
        public AutoMappingGeneric()
        {
            CreateMap<BeneficiaryEntity, BeneficiaryDetails>();
            CreateMap<BeneficiaryDetails, BeneficiaryEntity>();
            CreateMap<UserEntity, UserDetails>()
                .ForMember(o=>o.Beneficiaries, b=>b.MapFrom(z=> z.Beneficiaries));
            CreateMap<UserDetails, UserEntity>();
            CreateMap<TopUpOptions, TopUpOptionsEntity>();
            CreateMap<TopUpOptionsEntity, TopUpOptions>();
            CreateMap<TopUpTransactionEntity, TransactionTopUpInformation>();
            CreateMap<TransactionTopUpInformation, TopUpTransactionEntity>();
        }
    }
}
