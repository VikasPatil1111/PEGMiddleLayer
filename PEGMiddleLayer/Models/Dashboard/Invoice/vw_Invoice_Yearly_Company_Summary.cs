using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Invoice
{
    public class vw_Invoice_Yearly_Company_Summary
    {
        public string Company_Code { get; set; }
        public int? Invoice_Year { get; set; }
        public DateTime? Invoice_Date { get; set; }
        public double IV1 { get; set; }
        public double IV2 { get; set; }
        public double EIPL { get; set; }

        public double IV1_Qty { get; set; }
        public double IV2_Qty { get; set; }
        public double EIPL_Qty { get; set; }
        public string Order_Category { get; set; }
        public int Branch_Serial_No { get; set; }
        public string Branch_Name { get; set; }
        public string Customer_Code { get; set; }
    }
}
