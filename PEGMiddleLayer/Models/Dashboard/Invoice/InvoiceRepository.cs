using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Authentication;
using PEGMiddleLayer.Models.Common;
using PEGMiddleLayer.Models.CompanySelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Invoice
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public InvoiceRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<tblBI_Invoice_Details>> tblBI_Invoice_Details()
        {
            IQueryable<tblBI_Invoice_Details> query = _applicationDbContext.tblBI_Invoice_Details;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();
            return query1;

            //return await _applicationDbContext.tblBI_Invoice_Details.ToListAsync();
        }
        public async Task<IEnumerable<vw_InvoiceCustomerName>> vw_InvoiceCustomerNames()
        {
            IQueryable<vw_InvoiceCustomerName> query = _applicationDbContext.vw_InvoiceCustomerName;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();
            return query1;

           // return await _applicationDbContext.vw_InvoiceCustomerName.ToListAsync();
        }

        public async Task<IEnumerable<vw_Invoice_Tech_Deatils>> vw_Invoice_Tech_Deatils(int year, string Company_Code, string Product, string Customer_Code)
        {
            IQueryable<vw_Invoice_Tech_Deatils> queryable =   _applicationDbContext.vw_Invoice_Tech_Deatils;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            string[] product = Product.Split(',');
            
            string[] company = Company_Code.Split(',');
            if (year != 0)
            {
                queryable = queryable.Where(res => res.Invoice_Year == year);
               
            }
           
            if (!string.IsNullOrEmpty(Company_Code) && Company_Code != "null" && Company_Code != "All" && Company_Code != "undefined")
            {
               

                queryable = queryable.Where(res => company.Contains(res.Company_Code));
              

            }

            if (Customer_Code != null && Customer_Code !="All")
            {              

                queryable = queryable.Where(res => res.Customer_Code == Customer_Code);
               
            }

            queryable = from data in queryable
                        join user in Userquery
                        on data.Company_Code equals user.CompanyCode
                        where user.User_ID == setDatabase.UserId 
                        select data;

            return await queryable.ToListAsync();
        }
        public async Task<IEnumerable<So_Invoice_Target>> So_Invoice_Target(int year, string Company_Code)
        {
            IQueryable<So_Invoice_Target> queryable = _applicationDbContext.So_Invoice_Target;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            string[] company = Company_Code.Split(',');
            if (year != 0)
            {
                queryable = queryable.Where(res => res.Year == year);
               
            }
            if (!string.IsNullOrEmpty(Company_Code) && Company_Code != "null" && Company_Code != "All" && Company_Code != "undefined")
            {
                queryable = queryable.Where(res => company.Contains(res.Company_Code));
                 
            }

            queryable = from data in queryable
                        join user in Userquery
                        on data.Company_Code equals user.CompanyCode
                        where user.User_ID == setDatabase.UserId
                        select data;
            return await queryable.ToListAsync();

          //  return await _applicationDbContext.So_Invoice_Target.ToListAsync();
        }
        public async Task<IEnumerable<vw_Invoice_Order_Category>> InvoiceDetailsOrderCategory(int year, string Company_Code, string Product, string Customer_Code)
        {
            IQueryable<vw_Invoice_Order_Category> queryable = _applicationDbContext.vw_Invoice_Order_Category;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            string[] product = Product.Split(',');

            string[] company = Company_Code.Split(',');
            //if (year != 0)
            //{
            //    queryable = queryable.Where(res => res.Invoice_Year == year);
            //}
            //if (!string.IsNullOrEmpty(Product) && Product != "null" && Product != "undefined")
            //{
            //    queryable = queryable.Where(res => product.Contains(res.ProdGrp_MIS_Name));
            //}
            if (!string.IsNullOrEmpty(Company_Code) && Company_Code != "null" && Company_Code != "All" && Company_Code != "undefined")
            {
                queryable = queryable.Where(res => company.Contains(res.Company_Code));
               

            }

            if (Customer_Code != null && Customer_Code != "All")
            {
                queryable = queryable.Where(res => res.Customer_Code == Customer_Code);
                

            }
            queryable = from data in queryable
                        join user in Userquery
                        on data.Company_Code equals user.CompanyCode
                        where user.User_ID == setDatabase.UserId
                        select data;

            return await queryable.ToListAsync();


           // return await _applicationDbContext.vw_Invoice_Order_Category.ToListAsync();
        }

        //InvoiceCompanywiseCategorySummary
        public async Task<IEnumerable<vw_Invoice_Yearly_Company_Summary>> InvoiceCompanywiseYearlySummary(int year, string Company_Code, string Product, string Customer_Code)
        {
            IQueryable<vw_Invoice_Yearly_Company_Summary> queryable = _applicationDbContext.vw_Invoice_Yearly_Company_Summary;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;


            string[] product = Product.Split(',');

            string[] company = Company_Code.Split(',');
            //if (year != 0)
            //{
            //    queryable = queryable.Where(res => res.Invoice_Year == year);
            //}
            //if (!string.IsNullOrEmpty(Product) && Product != "null" && Product != "undefined")
            //{
            //    queryable = queryable.Where(res => product.Contains(res.ProdGrp_MIS_Name));
            //}
            if (!string.IsNullOrEmpty(Company_Code) && Company_Code != "null" && Company_Code != "All" && Company_Code != "undefined")
            {
                

                queryable = queryable.Where(res => company.Contains(res.Company_Code));
            }

            if (Customer_Code != null && Customer_Code != "All")
            {
                
                queryable = queryable.Where(res => res.Customer_Code == Customer_Code);
            }

            queryable = from data in queryable
                        join user in Userquery
                        on data.Company_Code equals user.CompanyCode
                        where user.User_ID == setDatabase.UserId
                        select data;
            return await queryable.ToListAsync();

           // return await _applicationDbContext.vw_Invoice_Yearly_Company_Summary.ToListAsync();
        }

        //InvoiceCompanywiseCategorySummary
        public async Task<IEnumerable<vw_Invoice_Yearly_Company_Summary>> InvoiceCompanywiseCategorySummary(int year, string Company_Code, string Product, string Customer_Code)
        {
            IQueryable<vw_Invoice_Yearly_Company_Summary> queryable = _applicationDbContext.vw_Invoice_Yearly_Company_Summary;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            string[] product = Product.Split(',');

            string[] company = Company_Code.Split(',');
            if (year != 0)
            {
               

                queryable = queryable.Where(res => res.Invoice_Year == year);
            }
            //if (!string.IsNullOrEmpty(Product) && Product != "null" && Product != "undefined")
            //{
            //    queryable = queryable.Where(res => product.Contains(res.ProdGrp_MIS_Name));
            //}
            if (!string.IsNullOrEmpty(Company_Code) && Company_Code != "null" && Company_Code != "All" && Company_Code != "undefined")
            {
                 

                queryable = queryable.Where(res => company.Contains(res.Company_Code));
            }

            if (Customer_Code != null && Customer_Code != "All")
            {
                

                queryable = queryable.Where(res => res.Customer_Code == Customer_Code);
            }
            queryable = from data in queryable
                        join user in Userquery
                        on data.Company_Code equals user.CompanyCode
                        where user.User_ID == setDatabase.UserId
                        select data;

            return await queryable.ToListAsync();

            // return await _applicationDbContext.vw_Invoice_Yearly_Company_Summary.ToListAsync();
        }   
        public async Task<IEnumerable<vw_Invoice_Yearly_Company_Summary>> BranchwiseYearlySummary(int year, string Company_Code, string Product, string Customer_Code)
        {
            IQueryable<vw_Invoice_Yearly_Company_Summary> queryable = _applicationDbContext.vw_Invoice_Yearly_Company_Summary;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            string[] product = Product.Split(',');

            string[] company = Company_Code.Split(',');
            if (year != 0)
            {
              

                queryable = queryable.Where(res => res.Invoice_Year == year);
            }
            //if (!string.IsNullOrEmpty(Product) && Product != "null" && Product != "undefined")
            //{
            //    queryable = queryable.Where(res => product.Contains(res.ProdGrp_MIS_Name));
            //}
            if (!string.IsNullOrEmpty(Company_Code) && Company_Code != "null" && Company_Code != "All" && Company_Code != "undefined")
            {
                 

                queryable = queryable.Where(res => company.Contains(res.Company_Code));
            }

            if (Customer_Code != null && Customer_Code != "All")
            {
               

                queryable = queryable.Where(res => res.Customer_Code == Customer_Code);
            }
            queryable = from data in queryable
                        join user in Userquery
                        on data.Company_Code equals user.CompanyCode
                        where user.User_ID == setDatabase.UserId
                        select data;

            return await queryable.ToListAsync();

            // return await _applicationDbContext.vw_Invoice_Yearly_Company_Summary.ToListAsync();
        }

    }
}
