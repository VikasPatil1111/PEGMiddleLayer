using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Common
{
   public interface ItblBI_PeriodRepository
    {
        public Task<IEnumerable<tblBI_Period>> gettblBI_Periods();
    }
}
