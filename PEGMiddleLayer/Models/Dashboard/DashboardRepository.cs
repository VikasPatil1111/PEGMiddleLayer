using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public DashboardRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<PendingOrder>> GetPendingOrders()
        {
            try
            {
                return await _applicationDbContext.tblBI_Order_Status_Dtl.ToListAsync();
            }
            catch (Exception ex) {
                return await _applicationDbContext.tblBI_Order_Status_Dtl.ToListAsync();
            }
        }

        public async Task<IEnumerable<ProductWisePendingOrder>> productWisePendingOrders()
        {
            return await _applicationDbContext.vw_ProductWisePendingOrderSummary.ToListAsync();
        }
        public async Task<IEnumerable<PendingOrderDetailsBarChart>> pendingOrderDetailsBarCharts()
        {
            return await _applicationDbContext.vw_PendingOrderBarSummary.ToListAsync();
        }

        public async Task<IEnumerable<PendingOrderQtyDetailsBarChart>> pendingOrderQtyDetailsBarCharts()
        {
            return await _applicationDbContext.vw_PendingOrderBarQtySummary.ToListAsync();
        }

        public async Task<IEnumerable<PendingOrder_Tech_Dtl>> GetPendingOrder_Tech_Dtls()
        {
            return await _applicationDbContext.vw_PendingOrder_Tech_Dtl.ToListAsync();
        }
    }
}
