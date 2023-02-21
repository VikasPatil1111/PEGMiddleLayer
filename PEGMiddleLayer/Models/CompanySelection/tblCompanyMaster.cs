using PEGMiddleLayer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.CompanySelection
{
    public class tblCompanyMaster 
    {        
        public int ID { get; set; }

        [Key]
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string DB_Name { get; set; }
        public string DB_Password { get; set; }
        public string Server_Name { get; set; }
        public string GST_NO { get; set; }
        public string Address { get; set; }
        public string Theme_Color { get; set; }
        public string SideBar_Color { get; set; }
        public string Company_Selection_Color { get; set; }
        public byte[] CompanyLogo { get; set; }
        public byte[] SmallLogo { get; set; }
        public DateTime? Created_Date { get; set; }
        public string MenuStructure { get; set; }
        //  public tblCompanyUsers tblCompanyUsers { get; set; }

       // [ForeignKey("CompanyCode")]
        public ICollection<tblCompanyUsers> tblCompanyUserss { get; set; }

    }
}
