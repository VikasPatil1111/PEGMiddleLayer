using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Sales.Utility
{
    public class Branch_Master
    {
        [Key]
        public string Branch_Code { get; set; }
        public string Branch_Name { get; set; }
        public Int16 Branch_Group { get; set; }
        public string Rde_Engineer { get; set; }
        public byte Status { get; set; }
        public string Rc_Code { get; set; }
        public string Email_Address { get; set; }
        public string Email_Address1 { get; set; }
        public char? Repo_Flag { get; set; }
        public char? State_Code { get; set; }
        public char? State_Name { get; set; }
        public char? D_E_I { get; set; }

    }
}
