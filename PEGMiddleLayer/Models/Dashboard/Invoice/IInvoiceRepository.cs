using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Invoice
{
   public interface IInvoiceRepository
    {
        public Task<IEnumerable<tblBI_Invoice_Details>> tblBI_Invoice_Details();
        public Task<IEnumerable<vw_InvoiceCustomerName>> vw_InvoiceCustomerNames();
        public Task<IEnumerable<vw_Invoice_Tech_Deatils>> vw_Invoice_Tech_Deatils(int year, string Company_Code, string Product, string Customer_Code);
        public Task<IEnumerable<So_Invoice_Target>> So_Invoice_Target(int year,string Company_Code);
        public Task<IEnumerable<vw_Invoice_Order_Category>> InvoiceDetailsOrderCategory(int year, string Company_Code, string Product, string Customer_Code);
        public Task<IEnumerable<vw_Invoice_Yearly_Company_Summary>> InvoiceCompanywiseYearlySummary(int year, string Company_Code, string Product, string Customer_Code);
        public Task<IEnumerable<vw_Invoice_Yearly_Company_Summary>> InvoiceCompanywiseCategorySummary(int year, string Company_Code, string Product, string Customer_Code);
        public Task<IEnumerable<vw_Invoice_Yearly_Company_Summary>> BranchwiseYearlySummary(int year, string Company_Code, string Product, string Customer_Code);

    }
}
