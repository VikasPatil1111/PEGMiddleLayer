using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard
{
    public class ProductWisePendingOrder
    {
        [Key]
        public string Product { get; set; }
        public string Company_Code { get; set; }
       
        public double OrderQty { get; set; }
        public double OrderValue { get; set; }
        public Int64 ID { get; set; }


    }
}
