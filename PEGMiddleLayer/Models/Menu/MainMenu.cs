using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Menu
{
     
    [Table("tblMainMenu")]
    public class tblMainMenu
    {

        // public int ID { get; set; }
        [Key]
        public string Parent_Menu_ID { get; set; }
        public string Parent_Menu_Name { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Company { get; set; }
        public bool Status { get; set; }
        public string Path { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Last_Modified_Date { get; set; }
        public int? Sr_No { get; set; }
        [ForeignKey("Parent_Menu_ID")]
        public ICollection<MainMenu_Child> childrens { get; set; }

        [ForeignKey("Parent_Menu_ID")]
        public ICollection<tblMainMenu_Access> MenuAccess { get; set; }
    }
}
