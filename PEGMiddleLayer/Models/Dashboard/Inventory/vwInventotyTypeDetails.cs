using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Inventory
{
    public class vwInventotyTypeDetails
    {
        public int? ID { get; set; }
        public string Company_Code { get; set; }
        public double RAW_STORE { get; set; }
        public double WIP_STORE { get; set; }

    }
}
