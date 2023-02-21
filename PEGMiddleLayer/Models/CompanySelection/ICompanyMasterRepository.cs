using PEGMiddleLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PEGMiddleLayer.Models.Menu;
using PEGMiddleLayer.Models.Dashboard;

namespace PEGMiddleLayer.Models.CompanySelection
{
    public interface ICompanyMasterRepository
    {
        Task<IEnumerable<tblCompanyMaster>> GetCompany();
        Task<IEnumerable<tblMainMenu>> GetMenuList();
        Task<IEnumerable<tblCompanyUsers>> tblCompanyUsers();
        Task<IEnumerable<tblMainMenu_Access>> tblMainMenu_Accesses();
       // Task<IEnumerable<PendingOrder>> pendingOrders();
    }
}
