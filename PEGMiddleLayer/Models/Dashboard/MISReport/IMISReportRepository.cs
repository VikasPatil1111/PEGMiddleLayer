using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.MISReport
{
    public interface IMISReportRepository 
    {
        Task<IEnumerable<USP_BIRepo_PEG_MIS_SS>> GetMISReprotDaily();
    }
}
