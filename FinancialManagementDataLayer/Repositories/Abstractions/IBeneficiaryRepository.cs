using FinancialManagementDataLayer.Entities;

namespace FinancialManagementDataLayer.Repositories
{
    public interface IBeneficiaryRepository
    {
        Task<IEnumerable<BeneficiaryEntity>> GetBeneficiariesByUserId(int userId, CancellationToken cancel);
        Task<BeneficiaryEntity> AddBeneficiary(BeneficiaryEntity beneficiary, CancellationToken cancel);
        Task<BeneficiaryEntity> GetBeneficiaryById(int beneficiaryId, CancellationToken cancel);
        Task<BeneficiaryEntity> UpdateBeneficiary(BeneficiaryEntity beneficiary, CancellationToken cancel);
    }
}
