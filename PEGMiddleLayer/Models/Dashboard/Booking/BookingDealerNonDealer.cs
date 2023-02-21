using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Booking
{
    public class BookingDealerNonDealer
    {
        public string Company_Code { get; set; }
        public int Year { get; set; }
        public double Direct { get; set; }
        public double Dealer { get; set; }
        public double Inter_Company { get; set; }

    }
}
