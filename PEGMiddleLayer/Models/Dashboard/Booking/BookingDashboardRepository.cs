using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Authentication;
using PEGMiddleLayer.Models.Common;
using PEGMiddleLayer.Models.CompanySelection;
using PEGMiddleLayer.Models.Dashboard.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Booking
{
    public class BookingDashboardRepository : IBookingDashboardRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BookingDashboardRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<BookingTable>> bookingTables(int year, int month, string Customer_Code, string Comapny)
        {
            IQueryable<BookingTable> query = _applicationDbContext.vwtblBI_MISOrderBooking_Dtl;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            string[] cust = Customer_Code.Split(',');
            string[] com = Comapny.Split(',');
           // string[] company = Company_Code.Split(',');
            if (year != 0)
            {
                query = query.Where(res => res.Year == year);

            }
            if (month != 0)
            {
                query = query.Where(res => res.Month == month);

            }
            if (!string.IsNullOrEmpty(Customer_Code) && Customer_Code != "null" && Customer_Code != "All" && Customer_Code != "undefined")
            {
                query = query.Where(res => cust.Contains(res.Customer_Code.Trim()));
            }
            if (!string.IsNullOrEmpty(Comapny) && Comapny != "null" && Comapny != "All" && Comapny != "undefined")
            {
                query = query.Where(res => com.Contains(res.Company_Code.Trim()));
            }
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();
            //  return await _applicationDbContext.tblBI_MISOrderBooking_Dtl.ToListAsync();
            return query1;
        }
        public async Task<IEnumerable<BookingTable>> bookingTablesYTD(int year, int month, string Customer_Code, string Comapny)
        {
            IQueryable<BookingTable> query = _applicationDbContext.vwtblBI_MISOrderBooking_Dtl;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;

            string[] cust = Customer_Code.Split(',');
            string[] com = Comapny.Split(',');
            // string[] company = Company_Code.Split(',');
            if (year != 0)
            {
                query = query.Where(res => res.Year == year);

            }
            //if (month != 0)
            //{
            //    query = query.Where(res => res.Month == month);

            //}
            if (!string.IsNullOrEmpty(Customer_Code) && Customer_Code != "null" && Customer_Code != "All" && Customer_Code != "undefined")
            {
                query = query.Where(res => cust.Contains(res.Customer_Code.Trim()));
            }
            if (!string.IsNullOrEmpty(Comapny) && Comapny != "null" && Comapny != "All" && Comapny != "undefined")
            {
                query = query.Where(res => com.Contains(res.Company_Code.Trim()));
            }
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();
            //  return await _applicationDbContext.tblBI_MISOrderBooking_Dtl.ToListAsync();
            return query1;
        }
        public async Task<IEnumerable<BookingTargetTable>> bookingTargetTables(string Company)
        {
            IQueryable<BookingTargetTable> query = _applicationDbContext.tblBI_OrderBooking_Target;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            string[] com = Company.Split(',');
            if (!string.IsNullOrEmpty(Company) && Company != "null" && Company != "All" && Company != "undefined")
            {
                query = query.Where(res => com.Contains(res.Company_Code.Trim()));
            }
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();
           
            return query1;

            // return await _applicationDbContext.tblBI_OrderBooking_Target.ToListAsync();
        }
        public async Task<IEnumerable<BookingDealerNonDealer>> GetBookingDealerNonDealers(int year, int month, string Customer_Code, string Comapny)
        {
            IQueryable<BookingDealerNonDealer> query = _applicationDbContext.vw_BookingDealerNonDealer;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            string[] cust = Customer_Code.Split(',');
            string[] com = Comapny.Split(',');

            if (year != 0)
            {
                query = query.Where(res => res.Year == year);

            }
            if (month != 0)
            {
                query = query.Where(res => res.Month == month);

            }

            if (!string.IsNullOrEmpty(Customer_Code) && Customer_Code != "null" && Customer_Code != "All" && Customer_Code != "undefined")
            {
                query = query.Where(res => cust.Contains(res.Customer_Code.Trim()));
            }
            if (!string.IsNullOrEmpty(Comapny) && Comapny != "null" && Comapny != "All" && Comapny != "undefined")
            {
                query = query.Where(res => com.Contains(res.Company_Code.Trim()));
            }
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();

            return query1;

          //  return await _applicationDbContext.vw_BookingDealerNonDealer.ToListAsync();
        }

        public async Task<IEnumerable<BookingCompanyBranches>> GetBookingCompanyBranches(int year, int month, string Customer_Code, string Comapny)
        {
            IQueryable<BookingCompanyBranches> query = _applicationDbContext.vw_BookingBranchDetails;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            string[] cust = Customer_Code.Split(',');
            string[] com = Comapny.Split(',');

            if (year != 0)
            {
                query = query.Where(res => res.Year == year);

            }
            if (month != 0)
            {
                query = query.Where(res => res.Month == month);

            }
            if (!string.IsNullOrEmpty(Customer_Code) && Customer_Code != "null" && Customer_Code != "All" && Customer_Code != "undefined")
            {
                query = query.Where(res => cust.Contains(res.Customer_Code.Trim()));
            }
            if (!string.IsNullOrEmpty(Comapny) && Comapny != "null" && Comapny != "All" && Comapny != "undefined")
            {
                query = query.Where(res => com.Contains(res.Company_Code.Trim()));
            }
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();

            return query1;

           // return await _applicationDbContext.vw_BookingBranchDetails.ToListAsync();
        }
        public async Task<IEnumerable<BookingChartDelayAnalysis>> GetBookingChartDelayAnalyses()
        {
            IQueryable<BookingChartDelayAnalysis> query = _applicationDbContext.vw_BookingBarCharDelayAnalysis;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();

            return query1;

            // return await _applicationDbContext.vw_BookingBarCharDelayAnalysis.ToListAsync();
        }
        public async Task<IEnumerable<tblBI_BFV_Order_TechDtl>> GetBFVProductSpec()
        {
            IQueryable<tblBI_BFV_Order_TechDtl> query = _applicationDbContext.tblBI_BFV_Order_TechDtl;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();

            return query1;

           // return await _applicationDbContext.tblBI_BFV_Order_TechDtl.ToListAsync(); 
        }
        public async Task<IEnumerable<tblBooking_Tech_Dtl>> GetTblBooking_Tech_Dtls(int Year, int Product_Code, string Customer, string Comapny, int? Month)
        {
            IQueryable<tblBooking_Tech_Dtl> query = _applicationDbContext.vw_Booking_Tech_Dtl;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            string[] cust = Customer.Split(',');
            string[] com = Comapny.Split(',');

            if (Year != 0)
            {
                query = query.Where(res => res.Year == Year);

            }
            if (Month != 0 && Month != null)
            {
                query = query.Where(res => res.Month == Month);

            }
            if (Product_Code != 0 )
            {
                query = query.Where(res => res.Product_Code == Product_Code);

            }
            if (!string.IsNullOrEmpty(Customer) && Customer != "null" && Customer != "All" && Customer != "undefined")
            {
                query = query.Where(res => cust.Contains(res.Customer_Code.Trim()));
            }
            if (!string.IsNullOrEmpty(Comapny) && Comapny != "null" && Comapny != "All" && Comapny != "undefined")
            {
                query = query.Where(res => com.Contains(res.Company_Code.Trim()));
            }

            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();

              return query1;

          //  return await _applicationDbContext.vw_Booking_Tech_Dtl.ToListAsync();
        }
        public async Task<IEnumerable<tblBooking_Tech_Dtl>> GetTblBooking_Tech_DtlsSP()
        {
            string Year = "[2022]";
            return await _applicationDbContext.vw_Booking_Tech_Dtl.FromSqlRaw("EXECUTE BOOKINGPIVOT @Year", Year).ToListAsync();
        }
        public async Task<IEnumerable<vw_BookingCustomerList>> GetBookingCustomerList(int Year, string Company, int Month)
        {
            IQueryable<vw_BookingCustomerList> query = _applicationDbContext.vw_BookingCustomerList;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            string[] com = Company.Split(',');

            if (Year != 0)
            {
                query = query.Where(res => res.Year == Year);

            }
            if (Month != 0 )
            {
                query = query.Where(res => res.Month == Month);

            }
            if (!string.IsNullOrEmpty(Company) && Company != "null" && Company != "All" && Company != "undefined")
            {
                query = query.Where(res => com.Contains(res.Company_Code.Trim()));
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
