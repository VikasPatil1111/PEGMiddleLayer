using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Booking
{
    public class tblBooking_Tech_Dtl
    {
        public string Company_Code { get; set; }
        public int? Order_Year { get; set; }
        //public string Order_Type { get; set; }
        //public string Order_Serial_No { get; set; }
        //public string Major_No { get; set; }
        //public int? Minor_No { get; set; }
        //public string Customer_Code { get; set; }
        //public string Customer_Name { get; set; }
        //public DateTime? Order_Booking_Completed_Date { get; set; }
        public int? Product_Code { get; set; }
        public string Product { get; set; }
        public string Flag { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public string Month_Name { get; set; }
        public int? ProdGrpCd { get; set; }
        public string ProdGrpNm { get; set; }
        public double Order_Value { get; set; }
        public int? ProdGrp_MIS_Id { get; set; }
        public string ProdGrp_MIS_Name { get; set; }
        public string Customer_Code { get; set; }

    }
}
