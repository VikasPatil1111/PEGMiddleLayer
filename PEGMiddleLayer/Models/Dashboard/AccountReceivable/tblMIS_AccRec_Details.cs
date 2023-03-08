using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.AccountReceivable
{
    public class tblMIS_AccRec_Details
    {
        public string Company_Code { get; set; }
        public int? SrNo { get; set; }
        public string Branch_Name { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Name { get; set; }
        public string Order_Serial_No { get; set; }
        public string Document_Name { get; set; }
        public string Document_No { get; set; }
        public DateTime? Document_Date { get; set; }
        public DateTime? Lorry_Date { get; set; }
        public string Payment_Term { get; set; }
        public int? Payment_Days { get; set; }
        public DateTime? Payment_Due_Date { get; set; }
        public decimal? Document_Amount { get; set; }
        public decimal? Adjusted_Amount { get; set; }
        public decimal? Receivable { get; set; }
        public decimal? Cdue_OS0_30 { get; set; }
        public decimal? Cdue_OS31_60 { get; set; }
        public decimal? Cdue_OS61_90 { get; set; }
        public decimal? Cdue_OSAbv90 { get; set; }
        public decimal? COver_Due { get; set; }
        public decimal? COd_OS0_30 { get; set; }
        public decimal? COd_OS31_60 { get; set; }
        public decimal? COd_OS61_90 { get; set; }

        public decimal? COd_OSAbv90 { get; set; }
        public decimal? Credit_Balance { get; set; }
        public DateTime? Create_Date { get; set; }
        public string User_Name { get; set; }
        public DateTime? Etl_On { get; set; }
        public int ID { get; set; }
        public string GSTIN { get; set; }
        public string PAN_No { get; set; }
        public double? Non_Collectable_Amt { get; set; }
        public double? Collectable_Amt { get; set; }
        public double? PDC_Collected_Amt { get; set; }
        public double? LC_Collected_Amt { get; set; }
        public double? PBG_Amt { get; set; }
        public double? Retention_Amt { get; set; }
        public double? due_OS0_15 { get; set; }
        public double? due_OS16_30 { get; set; }
        public double? due_OS31_60 { get; set; }
        public double? due_OSAbove_60 { get; set; }
        public double? Od_OS0_15 { get; set; }
        public double? Od_OS16_30 { get; set; }
        public double? Od_OS31_60 { get; set; }
        public double? Od_OSAbove_60 { get; set; }
        public double? Total_Due { get; set; }
        public double? Over_Due { get; set; }        
    }
}
