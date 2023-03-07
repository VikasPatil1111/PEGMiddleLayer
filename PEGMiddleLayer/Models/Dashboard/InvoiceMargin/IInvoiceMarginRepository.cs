using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.InvoiceMargin
{
    public interface IInvoiceMarginRepository
    {
        public Task<IEnumerable<vw_InvoiceMargin_Tech_Details>> vw_InvoiceMargin_Tech_Details();
        public Task<IEnumerable<vw_InvoiceMarginCustomerList>> Vw_InvoiceMarginCustomerLists();
        public Task<IEnumerable<vw_InvoiceMargin_Tech_Details>> vw_InvoiceMargin_Tech_Details(int year, string company, string Customer);

        public Task<IEnumerable<vw_InvoiceMargin_Tech_Details>> vw_InvoiceMargin_Tech_Details(int year, string company, string Customer,int Month);
    }
}
