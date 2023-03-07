using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Inventory
{
   public interface IInventoryRepository
    {
        public Task<IEnumerable<tblTotal_Inventory_Details>> tblTotal_Inventory_Details(string Companycode);
        public Task<IEnumerable<vwInventoryAgingSummary>> vwInventoryAgingSummaries(string Companycode);
        public Task<IEnumerable<vwInventotyTypeDetails>> vwInventotyTypeDetails(string Companycode);
        public Task<IEnumerable<vwInventoryAgingSummaryAllocated>> vwInventoryAgingSummaryAllocated(string Companycode);
        public Task<IEnumerable<vwInventoryAgingSummaryUnAllocated>> vwInventoryAgingSummaryUnAllocated(string Companycode);
    }
}
