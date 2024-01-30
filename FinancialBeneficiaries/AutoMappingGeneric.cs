using AutoMapper;
using FinancialManagementDataLayer.Entities;
using FinancialManagementServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }
    }
}
