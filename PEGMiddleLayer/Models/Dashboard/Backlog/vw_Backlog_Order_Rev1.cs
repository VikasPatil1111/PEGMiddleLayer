using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Backlog
{
    public class vw_Backlog_Order_Rev1
    {
        public string Company_Code
        {
            get;
            set;
        }
        public string Branch_Name
        {
            get;
            set;
        }
        public string Customer_Code
        {
            get;
            set;
        }
        public string Customer_Name
        {
            get;
            set;
        }
        public string Order_Category { get; set; }
        [Key]
        public string IV { get; set; }

        public string IV_No { get; set; }
        public Int16 Order_Year { get; set; }
        public string Order_Type
        {
            get;
            set;
        }
        public int Order_Serial_No { get; set; }
        public string Major_No { get; set; }
        public Byte Minor_No { get; set; }
        public int? Elo_Order_Item_Serial_No { get; set; }
        public DateTime? Order_Date { get; set; }
        public DateTime? Order_Booking_Completed_Date { get; set; }
        public string Customer_Po_No { get; set; }
        public string Customer_Po_Serial_No { get; set; }
        public DateTime? Customer_Po_Date { get; set; }
        public DateTime? Customer_Req_Date { get; set; }
        public string LD { get; set; }
        public DateTime? Liquid_Damage_Date { get; set; }
        public string Inspection_Reqd { get; set; }
        public DateTime? Planning_Commit_Dt { get; set; }
        public string Drawing_Required { get; set; }
        public string DrawingApprovalRecd { get; set; }
        public DateTime? Contract_Review_By_Ho_Date { get; set; }
        public DateTime? GADrg_Send_To_Branch_Customer { get; set; }
        public string QAP_Flag { get; set; }
        public string QAP_Approved { get; set; }
        public DateTime? Mkt_QAP_Clr_Receipt_Dt { get; set; }
        public DateTime? Qap_Approval_Receipt_Date { get; set; }
        public string Internal_QA_Appr_Flag { get; set; }
        public DateTime? Internal_QA_Appr_Date { get; set; }
        public DateTime? Mkt_Clarification_Receipt_Dt { get; set; }
        public DateTime? GADrg_Approval_Receipt_Date { get; set; }
        public DateTime? Mkt_Other_Clr_Receipt_Dt { get; set; }
        public DateTime? Oth_Approval_Receipt_Date { get; set; }
        public int? Lead_Time_In_Weeks { get; set; }
        public int? Order_Exe_Start_Date { get; set; }
        public string Mktg_Released_Flag { get; set; }
        public string Bom_Released_Flag { get; set; }
        public DateTime? Bom_Released_Date { get; set; }
        public string Costed_Flag { get; set; }
        public DateTime? Costed_Date { get; set; }
        public string Mfg_Costed_Flag { get; set; }

        public DateTime? Mfg_Costed_Date { get; set; }
        public string Costing_Fdbk_Remark { get; set; }
        public string Costing_Fdbk_Reason { get; set; }
        public int? Prod_Sch_Generation_Date { get; set; }
        public int? Simulation_Date { get; set; }
        public int? Indent_Genration_Date { get; set; }
        public int? Purchase_Order_Genration_Date { get; set; }
        public int? Material_Receipt_Comp_Date { get; set; }
        public int? Prod_Complition_Date
        {
            get;
            set;
        }

        public double? Rate
        {
            get;
            set;
        }
        public double? Conversion_Factor
        {
            get;
            set;
        }
        public double? Discounted_Rate
        {
            get;
            set;
        }
        public string Action_Code
        {
            get;
            set;
        }
        public string Action_Description
        {
            get;
            set;
        }
        public Int16? Order_Qty { get; set; }
        public Int16? Produced_Qty { get; set; }

        public double? Invoiced_Qty
        {
            get;
            set;
        }
        public double? Fg_Qty
        {
            get;
            set;
        }
        public double? Fg_Qty_MtcIssue
        {
            get;
            set;
        }
        public double? Fg_Qty_MtcNotIssue
        {
            get;
            set;
        }
        public double? Dismantle_Qty
        {
            get;
            set;
        }
        public double? Customer_Rejction_Qty
        {
            get;
            set;
        }
        public double? Pending_For_Prod_Qty
        {
            get;
            set;
        }
        public int? MCH_Total_Qty { get; set; }
        public int? MCH_Factory_Qty { get; set; }
        public int? MCH_Sales_Qty { get; set; }
        public double? Order_Qty_Clear_For_Prod
        {
            get;
            set;
        }
        public double? Order_Qty_Pend_For_GA_Prep
        {
            get;
            set;
        }
        public double? Order_Qty_Pend_For_QAP_Prep
        {
            get;
            set;
        }
        public double? Order_Qty_Pend_For_IntQAP_App
        {
            get;
            set;
        }
        public double? Order_Qty_Pend_For_GAAcept_Eng
        {
            get;
            set;
        }
        public double? Order_Qty_Pend_For_Bom_Release
        {
            get;
            set;
        }
        public double? Order_Qty_Pend_For_Cont_Review
        {
            get;
            set;
        }
        public double? Order_Qty_Pend_For_Tech_Clar
        {
            get;
            set;
        }
        public double? Order_Qty_Pend_ForGAAppFrCust
        {
            get;
            set;
        }
        public double? Order_Qty_Pend_ForQAPAppFrCust
        {
            get;
            set;
        }
        public double? Order_Qty_Pend_For_Cst_Clr
        {
            get;
            set;
        }
        public double? Order_Qty_Pend_ForCstClrCMDApp
        {
            get;
            set;
        }
        public double? Order_Qty_Pend_For_Mktg_Hold
        {
            get;
            set;
        }
        public double? Order_Value
        {
            get;
            set;
        }
        public double? Invoiced_Val
        {
            get;
            set;
        }
        public double? Dismantle_Val
        {
            get;
            set;
        }
        public double? Customer_Rejction_Val
        {
            get;
            set;
        }
        public double? Pending_For_Invoice_Val
        {
            get;
            set;
        }
        public double? Fg_Val
        {
            get;
            set;
        }
        public double? Fg_Val_MtcIssue
        {
            get;
            set;
        }
        public double? Fg_Val_MtcNotIssue
        {
            get;
            set;
        }
        public double? MCH_Total_Val
        {
            get;
            set;
        }
        public double? MCH_Factory_Val
        {
            get;
            set;
        }
        public double? MCH_Sales_Val
        {
            get;
            set;
        }
        public double? Order_Val_Clear_For_Prod
        {
            get;
            set;
        }
        public double? Order_Val_Pend_For_GA_Prep
        {
            get;
            set;
        }
        public double? Order_Val_Pend_For_QAP_Prep
        {
            get;
            set;
        }
        public double? Order_Val_Pend_For_IntQAP_App
        {
            get;

            set;
        }
        public double? Order_Val_Pend_For_GAAcept_Eng
        {
            get;
            set;
        }
        public double? Order_Val_Pend_For_Bom_Release
        {
            get;
            set;
        }
        public double? Order_Val_Pend_For_Cont_Review
        {
            get;
            set;
        }
        public double? Order_Val_Pend_For_Tech_Clar
        {
            get;
            set;
        }
        public double? Order_Val_Pend_ForGAAppFrCust
        {
            get;
            set;
        }
        public double? Order_Val_Pend_ForQAPAppFrCust
        {
            get;
            set;
        }
        public double? Order_Val_Pend_For_Cst_Clr
        {
            get;
            set;
        }
        public double? Order_Val_Pend_ForCstClrCMDApp
        {
            get;
            set;
        }
        public double? Order_Val_Pend_For_Mktg_Hold
        {
            get;
            set;
        }
        public double? Diff
        {
            get;
            set;
        }
        public int? Diff_1 { get; set; }
        public int? Diff_Fg_Qty { get; set; }
        public int? Diff_Fg_Val { get; set; }
        public DateTime? Create_Date { get; set; }
        public string User_Name { get; set; }
        public string D_E_I { get; set; }
        public string COrder_Serial_No { get; set; }
        public double? FgQty { get; set; }
        public double? Prod_NReady_For_Ship { get; set; }
        public double? FgQtyVal { get; set; }
        public double? Prod_NReady_For_ShipVal { get; set; }
        public string Country { get; set; }
        public double? Contract_Review_Done { get; set; }
        public string Hold { get; set; }
        public int? BookingYear { get; set; }
        public DateTime? Order_Reinitiate_Date { get; set; }
        public string Dealer_NonDealer { get; set; }
        public double? Backlog_Order_Qty
        {
            get;
            set;
        }
        public double? Backlog_Order_Value
        {
            get;
            set;
        }
        public int Year_Month
        {
            get;
            set;
        }
        public string Product { get; set; }
        public DateTime? ETL_On { get; set; }
        public int? Product_Code { get; set; }
        public double? Filter_Value { get; set; }
        public double? Filter_Qty { get; set; }
        public string Filter_CDD { get; set; }
        public string Filter_PDD { get; set; }
        public string Filter_Stages { get; set; }
        public int? CDD_Month_No { get; set; }
        public int? PDD_Month_No { get; set; }
    }
}
