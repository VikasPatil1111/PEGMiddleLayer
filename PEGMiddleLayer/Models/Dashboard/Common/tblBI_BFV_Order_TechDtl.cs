using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Common
{
    public class tblBI_BFV_Order_TechDtl
    {
        public string Company_Code { get; set; }
        public string Company_Name { get; set; }
        public string Product { get; set; }
        public string Domestic_Export { get; set; }
        public string Branch_Name { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Name { get; set; }
        public int? Booking_Year { get; set; }
        public int? Booking_Month { get; set; }
        public int? Booking_Year_Month { get; set; }
        public Int16? Order_Year { get; set; }
        public string Order_Type { get; set; }
        public string COrder_Serial_No { get; set; }
        public int? Order_Serial_No { get; set; }
        public string Major_No { get; set; }
        public Byte? Minor_No { get; set; }
        public int? Item_Serial_No { get; set; }
        public Byte? ItemSrlNo { get; set; }
        public string Flag { get; set; }
        public Int16? ProdGrpCd { get; set; }
        public string ProdGrpNm { get; set; }
        public string Size { get; set; }
        public string Size_Desc { get; set; }
        public string Rating { get; set; }
        public string Rating_Desc { get; set; }
        public string Flange { get; set; }
        public string FlngStd_Desc { get; set; }
        public string Body { get; set; }
        public string Body_MatlShortDesc { get; set; }
        public string Body_BondingMoc { get; set; }
        public string Disc { get; set; }
        public string Disc_MatlShortDesc { get; set; }
        public string Seat { get; set; }
        public string Seat_MatlShortDesc { get; set; }
        public string Shaft { get; set; }
        public string Shaft_MatlShortDesc { get; set; }
        public string Supplier_Make { get; set; }
        public string OperatorGrpNm { get; set; }
        public string Companion_Flang { get; set; }
        public string Spare_Kit { get; set; }
        public string Mfg_Trading { get; set; }
        public DateTime? Create_Date { get; set; }
        public string User_Name { get; set; }
        public string Body_MatlDesc { get; set; }
        public string Disc_MatlDesc { get; set; }
        public DateTime? Etl_On { get; set; }
    }
}
