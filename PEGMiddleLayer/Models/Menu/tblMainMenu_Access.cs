using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Menu
{
    public class tblMainMenu_Access
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Parent_Menu_ID")]
        public string Parent_Menu_ID { get; set; }
        public string Parent_Menu_Name { get; set; }
        public string Role_Id { get; set; }
        public bool Status { get; set; }
        public string Path { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Last_Modified_Date { get; set; }

    }
}
