using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Sales.Utility
{
    public class State_Master
    {
        [Key]
        public string State_Code { get; set; }
        public string State_Name { get; set; }
        public byte Status { get; set; }
        public string GST_State_Code { get; set; }
        public string Old_State_Code { get; set; }
        public string VEH_State_Code { get; set; }
    }
}
