using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Backlog
{
   public interface IBackLogOrderRepository
    {
       public  Task<IEnumerable< vw_Backlog_Order_Rev1>> vw_Backlog_Order_Rev1();
       public Task<IEnumerable<vw_Backlog_Order_Rev1>> Search_Backlog_Order(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo);
        public Task<IEnumerable<vw_Backlog_Order_Summary>> vw_Backlog_Order_Summary();
        public Task<IEnumerable<vw_Backlog_Order_Summary>> Search_Backlog_Order_Summary(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo,string CDD_Filter);
        public Task<IEnumerable<vw_Backlog_Order_Summary>> Search_Backlog_Order_Drilled_Summary(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo, int CDD_Year);
        public Task<IEnumerable<vw_BacklogOrder_Tech_Spec_Summary>> vw_BacklogOrder_Tech_Spec_Summary();
        public Task<IEnumerable<vw_BacklogOrder_Tech_Spec_Summary>> Search_BacklogOrder_Tech_Spec_Summary(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo,string CDDFilter);
        public Task<IEnumerable<vw_BacklogOrder_Tech_Spec_Summary>> Search_BacklogOrder_Tech_Spec_Summary(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo, int CDDYear,string MonthYear);
        public Task<IEnumerable<vw_BacklogOrder_Tech_Spec_Summary>> Search_BacklogOrder_Tech_Spec_Summary(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo, string CDDFilter, int CDDYear, string MonthYear);
        public Task<IEnumerable<vw_BacklogOrder_Tech_Spec_Summary>> Search_BacklogOrder_Tech_Spec_Grid(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo, string TechProduct);


    }
}
