using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Authentication;
using PEGMiddleLayer.Models.Common;
using PEGMiddleLayer.Models.CompanySelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.InvoiceMargin
{
    public class InvoiceMarginRepository : IInvoiceMarginRepository
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public InvoiceMarginRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<vw_InvoiceMargin_Tech_Details>> vw_InvoiceMargin_Tech_Details()
        {

            
            IQueryable<vw_InvoiceMargin_Tech_Details> query = _applicationDbContext.vw_InvoiceMargin_Tech_Details;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();

            return query1; 

            // return await _applicationDbContext.vw_InvoiceMargin_Tech_Details.ToListAsync();
        }
        public async Task<IEnumerable<vw_InvoiceMarginCustomerList>> Vw_InvoiceMarginCustomerLists()
        {
            IQueryable<vw_InvoiceMarginCustomerList> query = _applicationDbContext.vw_InvoiceMarginCustomerList;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();

            return query1;
        }
        public async Task<IEnumerable<vw_InvoiceMargin_Tech_Details>> vw_InvoiceMargin_Tech_Details(int year, string company, string Customer)
        {
            IQueryable<vw_InvoiceMargin_Tech_Details> query = _applicationDbContext.vw_InvoiceMargin_Tech_Details;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            string[] Company = company.Split(',');
            string[] customer = Customer.Split(',');

            if (year != 0)
            {
                query = query.Where(res => res.Invoice_Year == year);
            }

            if (!string.IsNullOrEmpty(company) && company != "null" && company != "All" && company != "undefined")
            {
                query = query.Where(res => company.Contains(res.Company_Code));
            }
            if (!string.IsNullOrEmpty(Customer) && Customer != "null" && Customer != "All" && Customer != "undefined")
            {
                query = query.Where(res => Customer.Contains(res.Customer_Code.Trim()));
            }
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();

            return query1;
        }

        public async Task<IEnumerable<vw_InvoiceMargin_Tech_Details>> vw_InvoiceMargin_Tech_Details(int year, string company, string Customer,int Month)
        {
            IQueryable<vw_InvoiceMargin_Tech_Details> query = _applicationDbContext.vw_InvoiceMargin_Tech_Details;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            string[] Company = company.Split(',');
            string[] customer = Customer.Split(',');

            if (year != 0)
            {
                query = query.Where(res => res.Invoice_Year == year);
            }
            if (Month != 0)
            {
                query = query.Where(res => res.Invoice_Date.Value.Month == Month);
            }
            if (!string.IsNullOrEmpty(company) && company != "null" && company != "All" && company != "undefined")
            {
                query = query.Where(res => company.Contains(res.Company_Code));
            }
            if (!string.IsNullOrEmpty(Customer) && Customer != "null" && Customer != "All" && Customer != "undefined")
            {
                query = query.Where(res => Customer.Contains(res.Customer_Code.Trim()));
            }
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();

            return query1;
        }

    }
}
