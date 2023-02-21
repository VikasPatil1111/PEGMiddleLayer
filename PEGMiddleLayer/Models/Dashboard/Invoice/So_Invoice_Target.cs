using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Invoice
{
    public class So_Invoice_Target
    {
        public Int16? Year { get; set; }
        public string Order_Type { get; set; }
        public float? Target { get; set; }
        public int? Ord { get; set; }
        public DateTime? Etl_On { get; set; }
        public string Company_Code { get; set; }
    }
}
