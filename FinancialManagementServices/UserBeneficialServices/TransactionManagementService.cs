using FinancialManagementServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementServices.UserBeneficialServices
{
    internal class TransactionManagementService : ITransactionManagementService
    {
        public Task<TransactionTopUpInformation> AddTopUpTransactionAsync(TransactionTopUpInformation transactionInformation)
        {
            throw new NotImplementedException();
        }
    }
}
