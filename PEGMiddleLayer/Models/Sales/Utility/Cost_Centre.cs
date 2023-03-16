using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Sales.Utility
{
    public class Cost_Centre
    {
        [Key]
        public string Cost_Centre_Code { get; set; }
        public string Cost_Centre_Name { get; set; }
        public string Branch_Manager { get; set; }
        public DateTime Create_Date { get; set; }
    }
}
