using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Invoice
{
    public class vw_InvoiceCustomerName
    {
        public string Company_Code { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Name { get; set; }
        public string GSTIN { get; set; }
        public string PAN_No { get; set; }
    }
}
