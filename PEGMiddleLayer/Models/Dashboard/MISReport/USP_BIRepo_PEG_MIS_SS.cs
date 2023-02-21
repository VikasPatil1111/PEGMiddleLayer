using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.MISReport
{
    public class USP_BIRepo_PEG_MIS_SS
    {
        public int SrNo { get; set; }
        public string Repo_Title { get; set; }
        public double? IVPLU1_MTD { get; set; }
        public double? IVPLU1_YTD { get; set; }
        public double? IVPLU2_MTD { get; set; }
        public double? IVPLU2_YTD { get; set; }
        public double? IVPL_MTD { get; set; }
        public double? IVPL_YTD { get; set; }
        public double? EIPL_MTD { get; set; }
        public double? EIPL_YTD { get; set; }
        public double? PEG_MTD { get; set; }
        public double? PEG_YTD { get; set; }
      //  public DateTime? Create_Date { get; set; }
    }
}
