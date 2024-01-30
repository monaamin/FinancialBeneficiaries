﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagementServices.Models;

namespace FinancialManagementServices.UserBeneficialServices
{
    public interface IBeneficiaryManagementService
    {
        Task<BeneficiaryDetails> AddBeneficiaryAsync(BeneficiaryDetails beneficiaryDetails);
    }
}
