using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Common
{
    public class tblBI_PeriodRepository : ItblBI_PeriodRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public tblBI_PeriodRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<tblBI_Period>> gettblBI_Periods()
        {
            return await _applicationDbContext.tblBI_Period.ToListAsync();
        }
    }
}
