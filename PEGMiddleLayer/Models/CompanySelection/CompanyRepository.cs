
using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PEGMiddleLayer.Models.Menu;
using PEGMiddleLayer.Models.Dashboard;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using PEGMiddleLayer.Models.Common;

namespace PEGMiddleLayer.Models.CompanySelection
{
    public class CompanyRepository : ICompanyMasterRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
       
        //private readonly RoleManager<ApplicationUser> _roleManager;
        public CompanyRepository(ApplicationDbContext applicationDbContext) //, RoleManager<ApplicationUser> roleManager ,
        {
            this._applicationDbContext = applicationDbContext;
          // this._userManager = userManager;
         //   _roleManager = roleManager;
        }
        public async  Task<IEnumerable<tblCompanyMaster>> GetCompany()
        {
            return await _applicationDbContext.TblCompanyMaster
               // .Include(com => com.tblCompanyUsers)
                .ToListAsync();
        }
        public async Task<IEnumerable<tblMainMenu>> GetMenuList()
        {
            // var userId = _userManager.GetUserIdAsync(HttpContext.)
           //  ApplicationUser applicationUser = _userManager.FindByIdAsync(setDatabase.UserId).Result;
          
            return await _applicationDbContext.tblMainMenu
                .Include(main => main.childrens) 
                .ThenInclude(main => main.childrens)
                .ThenInclude(main => main.childrens)
                .ToListAsync();
            //ThenInclude
        }
        public async Task<IEnumerable<tblMainMenu_Access>> tblMainMenu_Accesses()
        {
             
                return await _applicationDbContext.tblMainMenu_Access.ToListAsync();
            
        }
        public async Task<IEnumerable<tblCompanyUsers>> tblCompanyUsers()
        {
            return await _applicationDbContext.TblCompanyUsers
                .Include(main => main.tblCompanyMasters)
                .ToListAsync();
        }
        //public async Task<IEnumerable<PendingOrder>> pendingOrders()
        //{
        //    return await _applicationDbContext.tblBI_Order_Status_Dtl.ToListAsync();
        //}
    }
}
