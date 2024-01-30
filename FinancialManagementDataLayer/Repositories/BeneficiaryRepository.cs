using FinancialManagementDataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementDataLayer.Repositories
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly FinancialManagementContext _context;

        public BeneficiaryRepository(FinancialManagementContext context)
        {
            _context = context;
        }
        public async Task<BeneficiaryEntity> AddBeneficiary(BeneficiaryEntity beneficiary, CancellationToken cancel)
        {
            var result = _context.Beneficiaries.AddAsync(beneficiary, cancel);
            await _context.SaveChangesAsync(cancel);
            return result.Result.Entity;
        }

        public async Task<IEnumerable<BeneficiaryEntity>> GetBeneficiariesByUserId(int userId, CancellationToken cancel)
        {
            return await _context.Beneficiaries.Where(t => t.UserId == userId).ToListAsync(cancel);
        }

        public async Task<BeneficiaryEntity> GetBeneficiaryById(int beneficiaryId, CancellationToken cancel)
        {
            return await _context.Beneficiaries.FirstOrDefaultAsync(x => x.BeneficiaryId == beneficiaryId, cancel);
        }

        public async Task<BeneficiaryEntity> UpdateBeneficiary(BeneficiaryEntity beneficiary, CancellationToken cancel)
        {
            var result = _context.Beneficiaries.Update(beneficiary);
            await _context.SaveChangesAsync(cancel);
            return result.Entity;
        }
    }
}
