using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Backlog
{
    public class vw_BacklogOrder_Tech_Spec_Summary
    {

        public string Company_Code { get; set; }
        public Int16? Order_Year { get; set; }
        public string Order_Type { get; set; }
        public string COrder_Serial_No { get; set; }
        public string Major_No { get; set; }
        public byte? Minor_No { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Name { get; set; }
        public DateTime? Order_Booking_Completed_Date { get; set; }
        public int? Product_Code { get; set; }
        public string Product { get; set; }
        public string Flag { get; set; }
        public int? Month { get; set; }
        public string MonthName { get; set; }
        public int? ProdGrpCd { get; set; }
        public string ProdGrpNm { get; set; }
        public double? Order_Value { get; set; }
        public double? Order_Qty { get; set; }
        public int? ProdGrp_MIS_Id { get; set; }
        public string ProdGrp_MIS_Name { get; set; }
        public double? Filter_Value { get; set; }
        public double? Filter_Qty { get; set; }
        public string Filter_CDD { get; set; }
        public string Filter_PDD { get; set; }
        public string Filter_Stages { get; set; }
        public string CDD_Month { get; set; }
        public int? CDD_Year { get; set; }
        public string PDD_Month { get; set; }
        public int? PDD_Year { get; set; }
        public int? CDD_Month_No { get; set; }
        public int? PDD_Month_No { get; set; }
        /*
         * Column_name	Type
Filter_Value	float
Filter_Qty	float
Filter_CDD	nvarchar
Filter_PDD	nvarchar
Filter_Stages	varchar
CDD_Month	nvarchar
CDD_Year	int
PDD_Month	nvarchar
PDD_Year	int
CDD_Month_No	int
PDD_Month_No	int
         * */
    }
}
