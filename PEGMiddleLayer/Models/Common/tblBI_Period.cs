using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Common
{
    public class tblBI_Period
    {
        [Key]
        public int ID { get; set; }
        public int Year { get; set; }
        public string Module { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string UserId { get; set; }
        public DateTime Created_Date{ get; set; }   

    }
}
