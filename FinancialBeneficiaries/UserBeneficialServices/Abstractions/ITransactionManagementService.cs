using FinancialManagementServices.Models;

namespace FinancialManagementServices.UserBeneficialServices
{
    public interface ITransactionManagementService
    {
        Task<TransactionTopUpInformation> AddTopUpTransactionAsync(TransactionTopUpInformation transactionInformation);
    }
}
