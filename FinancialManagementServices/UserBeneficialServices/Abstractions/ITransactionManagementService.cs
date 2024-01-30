using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using FinancialManagementServices.Models;

namespace FinancialManagementServices.UserBeneficialServices
{
    internal interface ITransactionManagementService
    {
        Task<TransactionTopUpInformation> AddTopUpTransactionAsync(TransactionTopUpInformation transactionInformation);
    }
}
