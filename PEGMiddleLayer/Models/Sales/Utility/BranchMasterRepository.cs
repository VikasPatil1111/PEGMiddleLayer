using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Sales.Utility
{
    public class BranchMasterRepository : IBranchMasterRepository
    {
        public readonly CompanyDbContext _companyDbContext;
        public BranchMasterRepository(CompanyDbContext companyDbContext)
        {
            _companyDbContext = companyDbContext;    
        }
        public async Task<IEnumerable<Branch_Master>> GetBranch_Masters()
        {
            return await _companyDbContext.Branch_Master.ToListAsync();
        }
    }
}
