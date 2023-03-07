using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PEGMiddleLayer.Models.Dashboard.Common;

namespace PEGMiddleLayer.Models.Dashboard.Booking
{
    public interface IBookingDashboardRepository
    {
        Task<IEnumerable<BookingTable>> bookingTables(int year,int month,string Customer_Code,string Comapny);

        Task<IEnumerable<BookingTable>> bookingTablesYTD(int year, int month, string Customer_Code, string Comapny);
        Task<IEnumerable<BookingTargetTable>> bookingTargetTables(string Company);
        Task<IEnumerable<BookingDealerNonDealer>> GetBookingDealerNonDealers(int year, int month, string Customer_Code, string Comapny);
        Task<IEnumerable<BookingCompanyBranches>> GetBookingCompanyBranches(int year, int month, string Customer_Code, string Comapny);
        Task<IEnumerable<BookingChartDelayAnalysis>> GetBookingChartDelayAnalyses();
        Task<IEnumerable<tblBI_BFV_Order_TechDtl>> GetBFVProductSpec();

        Task<IEnumerable<tblBooking_Tech_Dtl>> GetTblBooking_Tech_Dtls(int Year, int Product_Code, string Customer, string Comapny, int? Month);

        Task<IEnumerable<tblBooking_Tech_Dtl>> GetTblBooking_Tech_DtlsSP();
        Task<IEnumerable<vw_BookingCustomerList>> GetBookingCustomerList(int Year, string Company, int Month);
    }
}
