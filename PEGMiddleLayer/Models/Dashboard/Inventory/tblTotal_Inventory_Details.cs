using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Inventory
{
    public class tblTotal_Inventory_Details
    {
        public string Company_Code { get; set; }
        public int? SrNo { get; set; }
        public string Inventory_Type { get; set; }
        public string Location_Code { get; set; }
        public string Group_Code { get; set; }
        public string Group_Description { get; set; }
        public string Stock_Group_Code { get; set; }
        public string Stock_Group_Description { get; set; }
        public string Supplier_Code { get; set; }
        public string Supplier_Name { get; set; }
        public string Part_Code { get; set; }
        public string Storage_Location { get; set; }
        public string Document_Type { get; set; }
        public string Document_No { get; set; }
        public DateTime? Document_Date { get; set; }
        public int? Age_Days { get; set; }
        public int? Age_Group_SrNo { get; set; }
        public string Age_Group { get; set; }
        public double Available_Quantity { get; set; }
        public double? Available_Value { get; set; }
        public double? Available_Value_In_Lakhs { get; set; }
        public double? Alloc_Quantity { get; set; }
        public double? Alloc_Value { get; set; }
        public double? Alloc_Value_In_Lakhs { get; set; }
        public double? Free_Quantity { get; set; }
        public double? Free_Value { get; set; }
        public double? Free_Value_In_Lakhs { get; set; }
        public double? Obsolete_Quantity { get; set; }
        public double? Obsolete_Value { get; set; }
        public double? Obsolete_Value_In_Lakhs { get; set; }
        public string Order_Type { get; set; }
        public string COrder_Serial_No { get; set; }
        public string Major_No { get; set; }
        public int? Minor_No { get; set; }
        public int? Item_Serial_No { get; set; }
        public double? Sales_Value { get; set; }
        public double? Sales_Value_In_Lakhs { get; set; }
        public string Part_Description { get; set; }
        public DateTime? Create_Date { get; set; }
        public string User_Name { get; set; }
        public int? MISInventory_SrNo { get; set; }
        public string MISInventory_Type { get; set; }
        public string MISInventory_Desc { get; set; }
        public int? ItemTy { get; set; }
        public string StructureCd { get; set; }
        public string Item_In_StdBom { get; set; }
        public string Active_YN { get; set; }
        public string ROL_YN { get; set; }
        public DateTime? MTC_Issued_Date { get; set; }
        public DateTime? Etl_On { get; set; }
        public int ID { get; set; }

    }
}
