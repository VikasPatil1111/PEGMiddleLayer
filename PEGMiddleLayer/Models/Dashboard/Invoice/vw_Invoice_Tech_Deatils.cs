using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Invoice
{
    public class vw_Invoice_Tech_Deatils
    {
        public int ID { get; set; }
        public string Company_Code { get; set; }
        public Int16? Order_Year { get; set; }
        public int Product_Code { get; set; }
        public string Product { get; set; }
        public string Flag { get; set; }
        public int? Invoice_Year { get; set; }
        public int? Invoice_Month { get; set; }
        public int ProdGrpCd { get; set; }
        public string ProdGrpNm { get; set; }
        public double? Item_ValueIn_Lakhs { get; set; }
        public double? Invoice_Quantity { get; set; }
        public int ProdGrp_MIS_Id { get; set; }
        public string ProdGrp_MIS_Name { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Name { get; set; }
        public int? Branch_Serial_No { get; set; }
        public string Branch_Name { get; set; }
        public string Dealer_NonDealer { get; set; }
        public string Order_Category { get; set; }
        public string Domestic_Export { get; set; }
        public string ProdGrp_MIS_Name_L2 { get; set; }
        public int ProdGrp_MIS_Name_L2_SrNo { get; set; }

    }
}
