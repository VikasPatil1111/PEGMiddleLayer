using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Backlog
{
    public class vw_Backlog_Order_Summary
    {
/*
Column_name	Type
Company_Code	varchar
ButterFlyValve	float
Actuator	float
GGCValve	float
BallValve	float
Filter_CDD	nvarchar
Filter_PDD	nvarchar
Filter_Stages	varchar
CDD_Month	nvarchar
CDD_Year	int
PDD_Month	nvarchar
PDD_Year	int
*/
        public string Company_Code { get; set; }
        public double? ButterFlyValve { get; set; }
        public double? Actuator { get; set; }
        public double? GGCValve { get; set; }
        public double? BallValve { get; set; }
        public string Filter_CDD { get; set; }
        public string Filter_PDD { get; set; }
        public string Filter_Stages { get; set; }
        public string CDD_Month { get; set; }
        public int? CDD_Year { get; set; }
        public string PDD_Month { get; set; }
        public int? PDD_Year { get; set; }
        public int? CDD_Month_No { get; set; }
        public int? PDD_Month_No { get; set; }
        public double? ButterFlyValveQty { get; set; }
        public double? ActuatorQty { get; set; }
        public double? GGCValveQty { get; set; }
        public double? BallValveQty { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Name { get; set; }
        public string COrder_Serial_No { get; set; }






    }
}
