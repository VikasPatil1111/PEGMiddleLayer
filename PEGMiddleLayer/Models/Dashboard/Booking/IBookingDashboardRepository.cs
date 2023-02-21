using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PEGMiddleLayer.Models.Dashboard.Common;

namespace PEGMiddleLayer.Models.Dashboard.Booking
{
    public interface IBookingDashboardRepository
    {
        Task<IEnumerable<BookingTable>> bookingTables();
        Task<IEnumerable<BookingTargetTable>> bookingTargetTables();
        Task<IEnumerable<BookingDealerNonDealer>> GetBookingDealerNonDealers();
        Task<IEnumerable<BookingCompanyBranches>> GetBookingCompanyBranches();
        Task<IEnumerable<BookingChartDelayAnalysis>> GetBookingChartDelayAnalyses();
        Task<IEnumerable<tblBI_BFV_Order_TechDtl>> GetBFVProductSpec();

        Task<IEnumerable<tblBooking_Tech_Dtl>> GetTblBooking_Tech_Dtls();

        Task<IEnumerable<tblBooking_Tech_Dtl>> GetTblBooking_Tech_DtlsSP();
    }
}
