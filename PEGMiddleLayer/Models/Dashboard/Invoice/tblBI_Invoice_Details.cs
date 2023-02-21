using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Invoice
{
    public class tblBI_Invoice_Details
    {
        public string Company_Code { get; set; }
        public string Company_Name { get; set; }
        public string Product { get; set; }
        public string Domestic_Export { get; set; }
        public string Branch_Name { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Name { get; set; }
        public string Dealer_NonDealer { get; set; }
        public int? Invoice_Year { get; set; }
        public int? Invoice_Month { get; set; }
        public int? Invoice_Year_Month { get; set; }
        public int? Booking_Year { get; set; }
        public int? Booking_Month { get; set; }
        public int? Booking_Year_Month { get; set; }
        public string Doc_Type { get; set; }
        public string Invoice_No { get; set; }
        public DateTime? Invoice_Date { get; set; }
        public string Customer_Po_No { get; set; }
        public DateTime? Customer_Po_Date { get; set; }
        public Int16? Order_Year { get; set; }
        public string Order_Type { get; set; }
        public string Order_Serial_No { get; set; }
        public string Order_Category { get; set; }
        public string Major_No { get; set; }
        public int? Minor_No { get; set; }
        public int? Item_Serial_No { get; set; }
        public DateTime? Customer_Req_Date { get; set; }
        public double? Invoice_Quantity { get; set; }
        public double? Item_Value { get; set; }
        public double? Item_ValueIn_Lakhs { get; set; }
        public string Lorry_Number { get; set; }
        public DateTime? Lorry_Date { get; set; }
        public DateTime? Create_Date { get; set; }
        public string User_Name { get; set; }
        public int? SrNo { get; set; }
        public DateTime? Etl_On { get; set; }
        public int? Product_Code { get; set; }
    }
}
