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
        public async Task<IEnumerable<BookingTable>> bookingTables()
        {
            IQueryable<BookingTable> query = _applicationDbContext.tblBI_MISOrderBooking_Dtl;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();
            //  return await _applicationDbContext.tblBI_MISOrderBooking_Dtl.ToListAsync();
            return query1;
        }
        public async Task<IEnumerable<BookingTargetTable>> bookingTargetTables()
        {
            IQueryable<BookingTargetTable> query = _applicationDbContext.tblBI_OrderBooking_Target;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();
           
            return query1;

            // return await _applicationDbContext.tblBI_OrderBooking_Target.ToListAsync();
        }
        public async Task<IEnumerable<BookingDealerNonDealer>> GetBookingDealerNonDealers()
        {
            IQueryable<BookingDealerNonDealer> query = _applicationDbContext.vw_BookingDealerNonDealer;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            var query1 = await (from data in query
                                join user in Userquery
                                on data.Company_Code equals user.CompanyCode
                                where user.User_ID == setDatabase.UserId
                                select data
                         ).ToListAsync();

            return query1;

          //  return await _applicationDbContext.vw_BookingDealerNonDealer.ToListAsync();
        }

        public async Task<IEnumerable<BookingCompanyBranches>> GetBookingCompanyBranches()
        {
            IQueryable<BookingCompanyBranches> query = _applicationDbContext.vw_BookingBranchDetails;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
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
        public async Task<IEnumerable<tblBooking_Tech_Dtl>> GetTblBooking_Tech_Dtls()
        {
            IQueryable<tblBooking_Tech_Dtl> query = _applicationDbContext.vw_Booking_Tech_Dtl;
            IQueryable<tblCompanyUsers> Userquery = _applicationDbContext.TblCompanyUsers;
            //var query1 = await (from data in query
            //                    join user in Userquery
            //                    on data.Company_Code equals user.CompanyCode
            //                    where user.User_ID == setDatabase.UserId
            //                    select data
            //             ).ToListAsync();

          //  return query1;

             return await _applicationDbContext.vw_Booking_Tech_Dtl.ToListAsync();
        }
        public async Task<IEnumerable<tblBooking_Tech_Dtl>> GetTblBooking_Tech_DtlsSP()
        {
            string Year = "[2022]";
            return await _applicationDbContext.vw_Booking_Tech_Dtl.FromSqlRaw("EXECUTE BOOKINGPIVOT @Year", Year).ToListAsync();
        }
    }
}
