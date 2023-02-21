using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.DIPattern
{
    public interface ICommanRepository
    {
        public int CalculateFinancialMonthDifference(DateTime FromDate, DateTime ToDate,int Year);

        public string getMonthName(int Month);
    }
}
