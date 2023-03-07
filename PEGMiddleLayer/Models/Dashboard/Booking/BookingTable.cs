using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Booking
{
    public class BookingTable
    {
        public string Company_Code { get; set; }
        public int? SrNo { get; set; }
        public string Domestic_Export { get; set; }
        public string Dealer_NonDealer { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Order_Year { get; set; }
        public string Order_Type { get; set; }
        public string Order_Serial_No { get; set; }
        public string Major_No { get; set; }
        public int? Minor_No { get; set; }
        public int? Serial_No { get; set; }
        public string Branch_Name { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Name { get; set; }
        public string Po_No { get; set; }
        public DateTime? Po_Date { get; set; }
        public string Amendment_Flag { get; set; }
        public double? Order_Value { get; set; }
        public string Cancelled_Abv_6Mth { get; set; }
        public DateTime? Create_Date { get; set; }
        public string User_Name { get; set; }
        public string Month_Name { get; set; }
        public DateTime? Order_Booking_Completed_Date { get; set; }
        public string Product { get; set; }
        public DateTime? Etl_On { get; set; }
        public int Product_Code { get; set; }
        public int ID { get; set; }
        public DateTime? Order_Date { get; set; }

    }
}
