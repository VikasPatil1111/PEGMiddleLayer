using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Authentication;
using PEGMiddleLayer.Models.Common;
using PEGMiddleLayer.Models.CompanySelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.AccountReceivable
{
    public class AccountReceivableRepository : IAccountReceivableRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AccountReceivableRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<tblMIS_AccRec_Details>> TblMIS_AccRec_Details(string Company,string CustomerCode, int DueFilter, int OverDueFilter)
        {
            IQueryable<tblMIS_AccRec_Details> query = _applicationDbContext.vw_AccountReceivableCustomerList;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            string[] Com = Company.Split(',');
            string[] CustomerList = CustomerCode.Split(',');
            if (!string.IsNullOrEmpty(Company) && Company != "null" && Company != "All" && Company != "undefined")
            {
                query = query.Where(res => Com.Contains(res.Company_Code.Trim()));
            }

            if (!string.IsNullOrEmpty(CustomerCode) && CustomerCode != "null" && CustomerCode != "All" && CustomerCode != "undefined")
            {
                query = query.Where(res => CustomerList.Contains(res.Customer_Code.Trim()));
            }
            if (DueFilter != 0)
            {
                if (DueFilter == 30)
                {
                    query = query.Where(res => res.Cdue_OS0_30 >0 );
                }
                else if (DueFilter == 60)
                {
                    query = query.Where(res => res.Cdue_OS31_60 > 0);
                }
                else if (DueFilter == 90)
                {
                    query = query.Where(res => res.Cdue_OS61_90 > 0);
                }
                else if (DueFilter == 100)
                {
                    query = query.Where(res => res.Cdue_OSAbv90 > 0);
                }
            }
            if (OverDueFilter != 0)
            {
                if (OverDueFilter == 30)
                {
                    query = query.Where(res => res.COd_OS0_30 > 0);
                }
                else if (OverDueFilter == 60)
                {
                    query = query.Where(res => res.COd_OS31_60 > 0);
                }
                else if (OverDueFilter == 90)
                {
                    query = query.Where(res => res.COd_OS61_90 > 0);
                }
                else if (OverDueFilter == 100)
                {
                    query = query.Where(res => res.COd_OSAbv90 > 0);
                }
            }
            query = query.Where(res => res.SrNo != 13);
            query = query.Where(res => res.SrNo != 14);

            var getBrach = await tblBranch_Users(setDatabase.UserId);

            string[] branches = new string[3];
            int index = 0;
            if (getBrach.Any() )
            {
                foreach (var vala in getBrach)
                {
                index++;
                branches[index] = vala.Branch_SrNo.ToString();
                   
                
                }
             
             }
            if (branches.Length > 0 && branches[1] != null)
            {
                query = query.Where(c =>   branches.Contains(c.SrNo.ToString()));
            }
           
            
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                        ).ToListAsync();

            return query1;

            // return await _applicationDbContext.tblMIS_AccRec_Details.ToListAsync();

        }
        public async Task<IEnumerable<tblMIS_AccRec_Details>> TblMIS_AccRec_Details(string Company, string Branch, string CustomerCode, int DueFilter, int OverDueFilter)
        {
            IQueryable<tblMIS_AccRec_Details> query = _applicationDbContext.vw_AccountReceivableCustomerList;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            string[] com = Company.Split(',');
            string[] CustomerList = CustomerCode.Split(',');

            if (!string.IsNullOrEmpty(Company) && Company != "null" && Company != "All" && Company != "undefined")
            {
                query = query.Where(res => com.Contains(res.Company_Code.Trim()));
            }
            if (!string.IsNullOrEmpty(Branch) && Branch != "null" && Branch != "All" && Branch != "undefined")
            {
                query = query.Where(res => res.Branch_Name == Branch);
                //query = query.Where(res => res.Customer_Code == Branch);
            }
            if (!string.IsNullOrEmpty(CustomerCode) && CustomerCode != "null" && CustomerCode != "All" && CustomerCode != "undefined")
            {
                query = query.Where(res => CustomerList.Contains(res.Customer_Code.Trim()));
            }
            if (DueFilter != 0)
            {
                if (DueFilter == 30)
                {
                    query = query.Where(res => res.Cdue_OS0_30 > 0);
                }
                else if (DueFilter == 60)
                {
                    query = query.Where(res => res.Cdue_OS31_60 > 0);
                }
                else if (DueFilter == 90)
                {
                    query = query.Where(res => res.Cdue_OS61_90 > 0);
                }
                else if (DueFilter == 100)
                {
                    query = query.Where(res => res.Cdue_OSAbv90 > 0);
                }
            }
            if (OverDueFilter != 0)
            {
                if (OverDueFilter == 30)
                {
                    query = query.Where(res => res.COd_OS0_30 > 0);
                }
                else if (OverDueFilter == 60)
                {
                    query = query.Where(res => res.COd_OS31_60 > 0);
                }
                else if (OverDueFilter == 90)
                {
                    query = query.Where(res => res.COd_OS61_90 > 0);
                }
                else if (OverDueFilter == 100)
                {
                    query = query.Where(res => res.COd_OSAbv90 > 0);
                }
            }
            query = query.Where(res => res.SrNo != 13);
            query = query.Where(res => res.SrNo != 14);


            var getBrach = await tblBranch_Users(setDatabase.UserId);

            string[] branches = new string[3];
            int index = 0;
            if (getBrach.Any())
            {
                foreach (var vala in getBrach)
                {
                    index++;
                    branches[index] = vala.Branch_SrNo.ToString();


                }

            }
            if (branches.Length > 0 && branches[1] != null)
            {
                query = query.Where(c => branches.Contains(c.SrNo.ToString()));
            }
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                       ).ToListAsync();
            return query1;

        }

        public async Task<IEnumerable<tblBranch_User>> tblBranch_Users(string UserID)
        {
            var result = await _applicationDbContext.tblBranch_User.ToListAsync();
            return result.Where(res => res.UserId == UserID);
        }



    }
}
