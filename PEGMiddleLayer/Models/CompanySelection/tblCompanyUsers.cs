using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace PEGMiddleLayer.Models.CompanySelection
{
    public class tblCompanyUsers
    {
        [Key]
        public int ID { get; set; }

        // [ForeignKey("tblCompanyMaster")]
      

        [ForeignKey("IdentityUser")]
        public string User_ID { get; set; }
        public Boolean Allow { get; set; }
        public DateTime ? Created_Date { get; set; }
        public DateTime ? Last_Modified_Date { get; set; }

//#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string CompanyCode { get; set; }
        [ForeignKey("CompanyCode")]
        public  tblCompanyMaster tblCompanyMasters { get; set; }
//#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}
