using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Menu
{
   // [DisplayName("childerns")]
    public class MainMenu_Sub_Child
    {
        
        [Key]
        public string Sub_Child_ID { get; set; }
        [ForeignKey("Child_ID")]
        public string Child_ID { get; set; }
        public string Sub_Child_Name { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Company { get; set; }
        public bool Status { get; set; }
        public string Path { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Last_Modified_Date { get; set; }

        [ForeignKey("Sub_Child_ID1")]
        public ICollection<MainMenu_Sub_Child2> childrens { get; set; }

       

    }
}
