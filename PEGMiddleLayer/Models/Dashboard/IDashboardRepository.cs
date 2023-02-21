using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard
{
   public interface IDashboardRepository
    {
        public Task<IEnumerable<PendingOrder>> GetPendingOrders();
        public Task<IEnumerable<ProductWisePendingOrder>> productWisePendingOrders();
        public Task<IEnumerable<PendingOrderDetailsBarChart>> pendingOrderDetailsBarCharts();

        public Task<IEnumerable<PendingOrderQtyDetailsBarChart>> pendingOrderQtyDetailsBarCharts();
        public Task<IEnumerable<PendingOrder_Tech_Dtl>> GetPendingOrder_Tech_Dtls();
    }
}
