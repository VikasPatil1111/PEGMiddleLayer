using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PEGMiddleLayer.Models.Sales.Masters
{
    [Table("Customer_Master")]
    public class CustomerMaster
    {
       private string Add1 = "";
        private string Add2 = "";
        private string Add3 = "";
        private string Add4 = "";
        private string Add5 = "";
        private string Add6 = "";
        private DateTime setCreate_Date = System.DateTime.Now;
        private string setState_Code="";
        private string setCompanyCode = "IV";

        [Key]
        public int _Id { get; set; }
        //[Column(TypeName ="varchar(30)")]
        public string CompanyCode { get { return setCompanyCode; } set { value = setCompanyCode; } }
        public string Customer_Code { get; set; }
        public char? Consignee_Type { get; set; }
        public string Customer_Name { get; set; }
       
        public string Country { get; set; }
        public string Registration { get; set; }
        public string Our_Code_With_Cust { get; set; }
        public char? Branch_Code { get; set; }
        
        public byte Status { get; set; }
        public string Ecc_No { get; set; }
        public string Allow_Credit { get; set; }
        public string Telephone_No { get; set; }
        public string Fax_No { get; set; }
        public string E_Mail { get; set; }
        public string Website { get; set; }
        public string New_Eccno { get; set; }
        public string Cust_Add7 { get; set; }
        public string Cust_Add8 { get; set; }
        public char? Cost_Centre_Code { get; set; }
        public char? Cust_Catagary { get; set; }
       // public string Credit_Limit { get; set; }
        public int? Credit_Limit { get; set; }
        public string TINNo { get; set; }
        public DateTime? TINDate { get; set; }
        public char? Cust_Class { get; set; }
        public char? Cust_Cred { get; set; }
        public int? Sl_No { get; set; }
        public char? Inv_Type { get; set; }
        public string Contact_Person { get; set; }
        //check below
        public DateTime? Lock_From_Date { get; set; }
        public char? Region_Code { get; set; }
        public double? OS_Limit { get; set; }
        public double? Actual_Balance { get; set; }
        public DateTime? OS_Limit_Date { get; set; }
        public byte? Booking_Concession { get; set; }
        public byte? Invoicing_Concession { get; set; }
        public string Cust_Add9 { get; set; }
        public string Cust_Add10  { get; set; }
        public byte? Production_Concession { get; set; }
        public char? Group_Company { get; set; }
        public byte? Cust_Lock_For_CForm { get; set; }
        public byte? Cust_Lock_For_FG_Limit { get; set; }
        public byte? Cust_Lock_For_Credit_Limit { get; set; }
        public byte? Cust_Lock_For_Ovedue_OS { get; set; }
        public byte? Booking_Lock_Status { get; set; }
        public byte? Invoicing_Lock_Status { get; set; }
        public string Dealer_Agreement_No { get; set; }
        public string PAN_No { get; set; }
        public string GSTIN { get; set; }
        public char? GST_Validate_YN { get; set; }
        public DateTime? GSTIN_Date { get; set; }
        public DateTime? GST_Validate_Date { get; set; }
        public string GST_Validate_By { get; set; }
        public string GST_State_Code { 
            get { return setState_Code; } 
            set {  setState_Code = value; } }
        public string GST_Cust_Add1 { 
            get { return Add1; } 
            set { Add1 = value; } }

        public string GST_Cust_Add2
        {
            get { return Add2; }
            set { Add2 = value; }
        }
        public string GST_Cust_Add3
        {
            get { return Add3; }
            set { Add3 = value; }
        }
        public string GST_Cust_Add4
        {
            get { return Add4; }
            set { Add4 = value; }
        }
        public string GST_Cust_Add5
        {
            get { return Add5; }
            set { Add5 = value; }
        }
        public string GST_Cust_Add6
        {
            get { return Add6; }
            set { Add6 = value; }
        }
        public string GST_Cust_Add7 { get; set; }
        public string GST_Cust_Add8 { get; set; }
        public string GST_Cust_Add9 { get; set; }
        public string GST_Cust_Add10 { get; set; }
        public string Cust_Add1 { get { return Add1; } set { value=Add1; } }
        public string Cust_Add2 { get { return Add2; } set { value = Add2; } }
        public string Cust_Add3 { get { return Add3; } set { value = Add3; } }
        public string Cust_Add4 { get { return Add4; } set { value = Add4; } }
        public string Cust_Add5 { get { return Add5; } set { value = Add5; } }
        public string Cust_Add6 { get { return Add6; } set { value = Add6; } }
        public string State_Code { get { return setState_Code; } set { value = setState_Code; } }
        public DateTime Created_Date { get { return setCreate_Date; } set { value= setCreate_Date ; } }
        public DateTime Create_Date { get { return setCreate_Date; } 
            set { value = setCreate_Date; } }

        public DateTime Last_Modified_Date
        {
            get { return setCreate_Date; }
            set { value = setCreate_Date; }
        }
        public string User_ID { get; set; }
        public string IP_Address_WAN { get; set; }
        public string IP_Address_LAN { get; set; }


    }
}
