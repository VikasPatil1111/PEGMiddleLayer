using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Authentication;
using PEGMiddleLayer.Models.Common;
using PEGMiddleLayer.Models.CompanySelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Inventory
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public InventoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<tblTotal_Inventory_Details>> tblTotal_Inventory_Details(string Companycode)
        {
            // var result = await _applicationDbContext.tblTotal_Inventory_Details.ToListAsync();

            

            IQueryable<tblTotal_Inventory_Details> query = _applicationDbContext.vwtblTotal_Inventory_Details;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            string[] Company = Companycode.Split(',');

            if (!string.IsNullOrEmpty(Companycode) && Companycode != "null" && Companycode != "All" && Companycode != "undefined")
            {


                query = query.Where(res => Company.Contains(res.Company_Code));


            }

            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                       ).ToListAsync();

            return query1;
        }
        public async Task<IEnumerable<vwInventoryAgingSummary>> vwInventoryAgingSummaries(string Companycode)
        {
            IQueryable<vwInventoryAgingSummary> query = _applicationDbContext.vwInventoryAgingSummary;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            string[] Company = Companycode.Split(',');

            if (!string.IsNullOrEmpty(Companycode) && Companycode != "null" && Companycode != "All" && Companycode != "undefined")
            {


                query = query.Where(res => Company.Contains(res.Company_Code));


            }

            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                       ).ToListAsync();

            return query1;
        }
        public async Task<IEnumerable<vwInventotyTypeDetails>> vwInventotyTypeDetails(string Companycode)
        {
            IQueryable<vwInventotyTypeDetails> query = _applicationDbContext.vwInventotyTypeDetails;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            string[] Company = Companycode.Split(',');

            if (!string.IsNullOrEmpty(Companycode) && Companycode != "null" && Companycode != "All" && Companycode != "undefined")
            {


                query = query.Where(res => Company.Contains(res.Company_Code));


            }

            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                       ).ToListAsync();

            return query1;

        }

        public async Task<IEnumerable<vwInventoryAgingSummaryAllocated>> vwInventoryAgingSummaryAllocated(string Companycode)
        {
            // var result = _applicationDbContext.vwInventoryAgingSummaryAllocated.ToListAsync();

            IQueryable<vwInventoryAgingSummaryAllocated> query = _applicationDbContext.vwInventoryAgingSummaryAllocated;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            string[] Company = Companycode.Split(',');

            if (!string.IsNullOrEmpty(Companycode) && Companycode != "null" && Companycode != "All" && Companycode != "undefined")
            {


                query = query.Where(res => Company.Contains(res.Company_Code));


            }

            var query1 = await (
                    from data in query
                    join user in Userquery
                    on data.Company_Code equals user.CompanyCode
                    where user.User_ID == setDatabase.UserId
                    select data
                ).ToListAsync();

            return query1;
        }
        public async Task<IEnumerable<vwInventoryAgingSummaryUnAllocated>> vwInventoryAgingSummaryUnAllocated(string Companycode)
        {
            IQueryable<vwInventoryAgingSummaryUnAllocated> result = _applicationDbContext.vwInventoryAgingSummaryUnAllocated;
            IQueryable<tblCompanyUsers> QueryUser = _applicationDbContext.TblCompanyUsers;

            string[] Company = Companycode.Split(',');

            if (!string.IsNullOrEmpty(Companycode) && Companycode != "null" && Companycode != "All" && Companycode != "undefined")
            {
                result = result.Where(res => Company.Contains(res.Company_Code));
            }

            var query = await (
                    from data in result
                    join user in QueryUser
                    on data.Company_Code equals user.CompanyCode
                    where user.User_ID == setDatabase.UserId
                    select data
                ).ToListAsync();

            return query;
        }

    }
}
