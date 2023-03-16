using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Sales.Utility
{
    public interface IBranchMasterRepository
    {
        public  Task<IEnumerable<Branch_Master>> GetBranch_Masters();
    }
}
