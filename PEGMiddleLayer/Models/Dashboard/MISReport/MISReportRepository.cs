using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.MISReport
{
    public class MISReportRepository : IMISReportRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public MISReportRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<USP_BIRepo_PEG_MIS_SS>> GetMISReprotDaily()
        {
            // return await _applicationDbContext.USP_BIRepo_PEG_MIS_SS.ToListAsync();     

            return await _applicationDbContext.USP_BIRepo_PEG_MIS_SS.FromSqlRaw("EXECUTE USP_BIRepo_PEG_MIS_SS").ToListAsync();
        }
    }
}
