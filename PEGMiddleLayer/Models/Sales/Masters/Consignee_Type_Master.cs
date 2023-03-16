using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Sales.Masters
{
    public class Consignee_Type_Master2
    {
        [Key]
        public int _Id { get; set; }
        public string CompanyCode { get; set; }
        public string Consignee_Type { get; set; }
        public string Consignee_Desc { get; set; }
        public char Allow_Invoice_Flag { get; set; }
        public byte Status { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Last_Modified_Date { get; set; }
        public string User_ID { get; set; }
        public string IP_Address_WAN { get; set; }
        public string IP_Address_LAN { get; set; }

    }
}
