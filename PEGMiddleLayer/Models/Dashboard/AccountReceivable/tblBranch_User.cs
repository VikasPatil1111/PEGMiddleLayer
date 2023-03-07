using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.AccountReceivable
{
    public class tblBranch_User
    {
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        public string Branch_Code { get; set; }
        public int Branch_SrNo { get; set; }
    }
}
