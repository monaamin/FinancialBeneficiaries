using FinancialManagementServices.Models;

namespace FinancialManagementServices.UserBeneficialServices
{
    public interface IBeneficiaryManagementService
    {
        Task<BeneficiaryDetails> AddBeneficiaryAsync(BeneficiaryDetails beneficiaryDetails);
    }
}
