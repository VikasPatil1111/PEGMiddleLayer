using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Menu
{
    [DisplayName("childrens")]
    public class MainMenu_Child//MainMenu_Child
    {
        [Key]
        public string Child_Id { get; set; }

        [ForeignKey("Parent_Menu_ID")]
        public string Parent_Menu_ID { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        // public tblMainMenu tblMainMenus { get; set; }

        [ForeignKey("Child_ID")]
  
        public ICollection<MainMenu_Sub_Child> childrens { get; set; }
    }
}
