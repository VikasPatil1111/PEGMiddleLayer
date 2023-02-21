using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Booking
{
    public class BookingTargetTable
    {
        public string Company_Code { get; set; }
        public Int16? SrNo { get; set; }
        public string Domestic_Export { get; set; }
        public string Product { get; set; }
        public Int16? Year { get; set; }
        public string Order_Type { get; set; }
        public string Rc_Code { get; set; }
        public string Branch { get; set; }
        public string Branch_Name { get; set; }
        public double? Target { get; set; }
        public DateTime? Create_Date { get; set; }
        public string User_Name { get; set; }
    }
}
