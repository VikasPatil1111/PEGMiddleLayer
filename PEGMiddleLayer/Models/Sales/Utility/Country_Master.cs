using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Sales.Utility
{
    public class Country_Master
    {
        [Key]
        public string Country_Code { get; set; }
        public string Country_Name { get; set; }
        public Int16 Status { get; set; }
    }
}
