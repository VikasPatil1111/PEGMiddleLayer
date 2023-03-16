using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Sales.Utility
{
    public class GST_Percent_Master
    {
        public char Company_Code { get; set; }
        [Key]
        public char GST_Code { get; set; }
        public char GST_Description { get; set; }
        public double GST_Percent { get; set; }
        public double GST_CessPercent { get; set; }
        public double CGST_Percent { get; set; }
        public double CGST_CessPercent { get; set; }
        public double SGST_Percent { get; set; }
        public double SGST_CessPercent { get; set; }
        public double IGST_Percent { get; set; }
        public double IGST_CessPercent { get; set; }
        public char State_Code { get; set; }
        public DateTime? Effective_From { get; set; }
        public DateTime? Effective_To { get; set; }
        public char? Active_YN { get; set; }
        public DateTime? Create_Date { get; set; }
        public char? User_Name { get; set; }
        public char? Used_By_Sales { get; set; }
        public char? Used_By_Purchase { get; set; }
    }
}
