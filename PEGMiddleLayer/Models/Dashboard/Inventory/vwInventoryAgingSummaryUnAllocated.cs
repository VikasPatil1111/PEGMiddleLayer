using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Inventory
{
    public class vwInventoryAgingSummaryUnAllocated
    {
        public int? ID { get; set; }
        public string Company_Code { get; set; }
        public string Inventory_Type { get; set; }
        public double Age_0_30 { get; set; }
        public double Age_31_60 { get; set; }
        public double Age_61_90 { get; set; }
        public double Age_91_180 { get; set; }
        public double Age_Above_180 { get; set; }
        public int? SrNo { get; set; }
    }
}
