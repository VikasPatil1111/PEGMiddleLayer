using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Invoice
{
    public class vw_Invoice_Order_Category
    {

        public string Company_Code { get; set; }
        public int? Invoice_Year { get; set; }
        public double DEALER { get; set; }
        public double PROJECT { get; set; }
        public double REVENUE { get; set; }
        public double Other { get; set; }
        public double DEALER_Qty { get; set; }
        public double PROJECT_Qty { get; set; }
        public double REVENUE_Qty { get; set; }
        public double Other_Qty { get; set; }
        public string Customer_Code { get; set; }




        //        Column_name Type
        //Company_Code varchar
        //Invoice_Year int
        //DEALER  float
        //PROJECT float
        //REVENUE float
        //NotFound    float
    }
}
