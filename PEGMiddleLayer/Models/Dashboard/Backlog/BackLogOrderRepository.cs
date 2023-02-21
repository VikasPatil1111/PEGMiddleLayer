using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.Backlog
{
    public class BackLogOrderRepository : IBackLogOrderRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BackLogOrderRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<vw_Backlog_Order_Rev1>> vw_Backlog_Order_Rev1()
        {
            return await _applicationDbContext.vw_Backlog_Order_Rev1.ToListAsync();
        }

        public async Task<IEnumerable<vw_Backlog_Order_Rev1>> Search_Backlog_Order(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo)
        {
            // IQueryable<vw_Backlog_Order_Rev1> query = _applicationDbContext.vw_Backlog_Order_Rev1;
            IQueryable<vw_Backlog_Order_Rev1> query = _applicationDbContext.vw_Backlog_Order_Rev1;
            string[] company = company_Code.Split(',');
            string[] CustCDD = CDD.Split(',');
            string[] CustPDD = PDD.Split(',');
            string[] Staging = Stages.Split(',');
            string[] Cust = Customer.Split(',');
            string[] OrderNos = OrderNo.Split(',');
            if (!string.IsNullOrEmpty(company_Code) && company_Code.Length >0 && company_Code != "null" && company_Code != "undefined")
            {
              query = query.Where(res => company.Contains(res.Company_Code.Trim()) );              
            }
            if (!string.IsNullOrEmpty(CDD) && CDD != "null" && CDD != "undefined")
            {
                query = query.Where(res => CustCDD.Contains(res.Filter_CDD));
            }
            if (!string.IsNullOrEmpty(PDD) && PDD != "null" && PDD != "undefined")
            {
                query = query.Where(res => CustPDD.Contains(res.Filter_PDD));
            }
            if (!string.IsNullOrEmpty(Stages) && Stages != "null" && Stages != "undefined")
            {
                query = query.Where(res => Staging.Contains(res.Filter_Stages));
            }
            if (!string.IsNullOrEmpty(Customer) && Customer != "null" && Customer != "undefined")
            {
                query = query.Where(res => Cust.Contains(res.Customer_Name));
            }
            if (!string.IsNullOrEmpty(OrderNo) && OrderNo != "null" && OrderNo != "undefined")
            {
                query = query.Where(res => OrderNos.Contains(res.COrder_Serial_No));
            }
            //&& string.IsNullOrEmpty(CDD) && string.IsNullOrEmpty(PDD) && string.IsNullOrEmpty(Stages)
            //var res = query in q 
            //return await 
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<vw_Backlog_Order_Summary>> vw_Backlog_Order_Summary()
        {
            //var result = await _applicationDbContext.vw_Backlog_Order_Summary.ToListAsync();
            return await _applicationDbContext.vw_Backlog_Order_Summary.ToListAsync();
        }

        //
        public async Task<IEnumerable<vw_Backlog_Order_Summary>> Search_Backlog_Order_Summary(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo,string CDDMonth)
        {
            IQueryable<vw_Backlog_Order_Summary> query = _applicationDbContext.vw_Backlog_Order_Summary;
             
            string[] company = company_Code.Split(',');
            string[] CustCDD = CDD.Split(',');
            string[] CustPDD = PDD.Split(',');
            string[] Staging = Stages.Split(',');
            string[] Cust = Customer.Split(',');
            string[] OrderNos = OrderNo.Split(',');
            string[] CDDMnth = CDDMonth.Split(',');
            if (!string.IsNullOrEmpty(company_Code) && company_Code.Length > 0 && company_Code != "null" && company_Code != "undefined")
            {
                query = query.Where(res => company.Contains(res.Company_Code.Trim()));
            }
            if (!string.IsNullOrEmpty(CDD) && CDD != "null" && CDD != "undefined")
            {
                query = query.Where(res => CustCDD.Contains(res.Filter_CDD));
            }
            if (!string.IsNullOrEmpty(PDD) && PDD != "null" && PDD != "undefined")
            {
                query = query.Where(res => CustPDD.Contains(res.Filter_PDD));
            }
            if (!string.IsNullOrEmpty(Stages) && Stages != "null" && Stages != "undefined")
            {
                query = query.Where(res => Staging.Contains(res.Filter_Stages));
            }
            if (!string.IsNullOrEmpty(Customer) && Customer != "null" && Customer != "undefined")
            {
                query = query.Where(res => Cust.Contains(res.Customer_Name));
            }
            if (!string.IsNullOrEmpty(OrderNo) && OrderNo != "null" && OrderNo != "undefined")
            {
                query = query.Where(res => OrderNos.Contains(res.COrder_Serial_No));
            }
            if (!string.IsNullOrEmpty(CDDMonth) && CDDMonth != "null" && CDDMonth != "undefined")
            {
                query = query.Where(res => CDDMnth.Contains(res.Filter_CDD));
            }
            //if (!string.IsNullOrEmpty(Customer) && Customer != "null" && Customer != "undefined")
            //{
            //    query = query.Where(res => Cust.Contains(res.Customer_Name));
            //}
            //if (!string.IsNullOrEmpty(OrderNo) && OrderNo != "null" && OrderNo != "undefined")
            //{
            //    query = query.Where(res => OrderNos.Contains(res.COrder_Serial_No));
            //}
            //&& string.IsNullOrEmpty(CDD) && string.IsNullOrEmpty(PDD) && string.IsNullOrEmpty(Stages)
            //   var res = query in q 
            //return await 
            return await query.ToListAsync();


        }
        //Search_BacklogOrder_Tech_Spec_Summary

        public async Task<IEnumerable<vw_Backlog_Order_Summary>> Search_Backlog_Order_Drilled_Summary(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo, int CDD_Year)
        {
            IQueryable<vw_Backlog_Order_Summary> query = _applicationDbContext.vw_Backlog_Order_Summary;

            string[] company = company_Code.Split(',');

            string[] CustCDD = CDD.Split(',');
            string[] CustPDD = PDD.Split(',');
            string[] Staging = Stages.Split(',');
            string[] Cust = Customer.Split(',');
            string[] OrderNos = OrderNo.Split(',');
           // string[] CDDMnth = CDDMonth.Split(',');
            if (!string.IsNullOrEmpty(company_Code) && company_Code.Length > 0 && company_Code != "null" && company_Code != "undefined")
            {
                query = query.Where(res => company.Contains(res.Company_Code.Trim()));
            }
            if (!string.IsNullOrEmpty(CDD) && CDD != "null" && CDD != "undefined")
            {
                query = query.Where(res => CustCDD.Contains(res.Filter_CDD));
            }
            if (!string.IsNullOrEmpty(PDD) && PDD != "null" && PDD != "undefined")
            {
                query = query.Where(res => CustPDD.Contains(res.Filter_PDD));
            }
            if (!string.IsNullOrEmpty(Stages) && Stages != "null" && Stages != "undefined")
            {
                query = query.Where(res => Staging.Contains(res.Filter_Stages));
            }
            if (!string.IsNullOrEmpty(Customer) && Customer != "null" && Customer != "undefined")
            {
                query = query.Where(res => Cust.Contains(res.Customer_Name));
            }
            if (!string.IsNullOrEmpty(OrderNo) && OrderNo != "null" && OrderNo != "undefined")
            {
                query = query.Where(res => OrderNos.Contains(res.COrder_Serial_No));
            }
            if (CDD_Year > 1950)
            {
                query = query.Where(res => res.CDD_Year == CDD_Year);
                query = query.Where(res => res.Filter_CDD == "Delay");
            }
           
            //if (!string.IsNullOrEmpty(Customer) && Customer != "null" && Customer != "undefined")
            //{
            //    query = query.Where(res => Cust.Contains(res.Customer_Name));
            //}
            //if (!string.IsNullOrEmpty(OrderNo) && OrderNo != "null" && OrderNo != "undefined")
            //{
            //    query = query.Where(res => OrderNos.Contains(res.COrder_Serial_No));
            //}
            //&& string.IsNullOrEmpty(CDD) && string.IsNullOrEmpty(PDD) && string.IsNullOrEmpty(Stages)
            //   var res = query in q 
            //return await 
            return await query.ToListAsync();


        }

        public async Task<IEnumerable<vw_BacklogOrder_Tech_Spec_Summary>> vw_BacklogOrder_Tech_Spec_Summary()
        {
            //var result = await _applicationDbContext.vw_Backlog_Order_Summary.ToListAsync();
            return await _applicationDbContext.vw_BacklogOrder_Tech_Spec_Summary.ToListAsync();
        }

        public async Task<IEnumerable<vw_BacklogOrder_Tech_Spec_Summary>> Search_BacklogOrder_Tech_Spec_Summary(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo,string CDDFilter)
        {
            IQueryable<vw_BacklogOrder_Tech_Spec_Summary> query = _applicationDbContext.vw_BacklogOrder_Tech_Spec_Summary;

            string[] company = company_Code.Split(',');

            string[] CustCDD = CDD.Split(',');
            string[] CustPDD = PDD.Split(',');
            string[] Staging = Stages.Split(',');
            string[] Cust = Customer.Split(',');
            string[] OrderNos = OrderNo.Split(',');
            string[] CddFilters = CDDFilter.Split(',');
            if (!string.IsNullOrEmpty(company_Code) && company_Code.Length > 0 && company_Code != "null" && company_Code != "undefined")
            {
                query = query.Where(res => company.Contains(res.Company_Code.Trim()));
            }
            if (!string.IsNullOrEmpty(CDD) && CDD != "null" && CDD != "undefined")
            {
                query = query.Where(res => CustCDD.Contains(res.Filter_CDD));
            }
            if (!string.IsNullOrEmpty(PDD) && PDD != "null" && PDD != "undefined")
            {
                query = query.Where(res => CustPDD.Contains(res.Filter_PDD));
            }
            if (!string.IsNullOrEmpty(Stages) && Stages != "null" && Stages != "undefined")
            {
                query = query.Where(res => Staging.Contains(res.Filter_Stages));
            }
            if (!string.IsNullOrEmpty(Customer) && Customer != "null" && Customer != "undefined")
            {
                query = query.Where(res => Cust.Contains(res.Customer_Name));
            }
            if (!string.IsNullOrEmpty(OrderNo) && OrderNo != "null" && OrderNo != "undefined")
            {
                query = query.Where(res => OrderNos.Contains(res.COrder_Serial_No));
            }
            if (!string.IsNullOrEmpty(CDDFilter) && CDDFilter != "null" && CDDFilter != "undefined")
            {
                query = query.Where(res => CddFilters.Contains(res.Filter_CDD));
            }
            //&& string.IsNullOrEmpty(CDD) && string.IsNullOrEmpty(PDD) && string.IsNullOrEmpty(Stages)
            //   var res = query in q  CddFilters
            //return await 
            return await query.ToListAsync();


        }


        public async Task<IEnumerable<vw_BacklogOrder_Tech_Spec_Summary>> Search_BacklogOrder_Tech_Spec_Grid(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo, string TechProduct)
        {
            IQueryable<vw_BacklogOrder_Tech_Spec_Summary> query = _applicationDbContext.vw_BacklogOrder_Tech_Spec_Summary;

            string[] company = company_Code.Split(',');

            string[] CustCDD = CDD.Split(',');
            string[] CustPDD = PDD.Split(',');
            string[] Staging = Stages.Split(',');
            string[] Cust = Customer.Split(',');
            string[] OrderNos = OrderNo.Split(',');
            string[] CddFilters = TechProduct.Split(',');
            if (!string.IsNullOrEmpty(company_Code) && company_Code.Length > 0 && company_Code != "null" && company_Code != "undefined")
            {
                query = query.Where(res => company.Contains(res.Company_Code.Trim()));
            }
            if (!string.IsNullOrEmpty(CDD) && CDD != "null" && CDD != "undefined")
            {
                query = query.Where(res => CustCDD.Contains(res.Filter_CDD));
            }
            if (!string.IsNullOrEmpty(PDD) && PDD != "null" && PDD != "undefined")
            {
                query = query.Where(res => CustPDD.Contains(res.Filter_PDD));
            }
            if (!string.IsNullOrEmpty(Stages) && Stages != "null" && Stages != "undefined")
            {
                query = query.Where(res => Staging.Contains(res.Filter_Stages));
            }
            if (!string.IsNullOrEmpty(Customer) && Customer != "null" && Customer != "undefined")
            {
                query = query.Where(res => Cust.Contains(res.Customer_Name));
            }
            if (!string.IsNullOrEmpty(OrderNo) && OrderNo != "null" && OrderNo != "undefined")
            {
                query = query.Where(res => OrderNos.Contains(res.COrder_Serial_No));
            }
            if (!string.IsNullOrEmpty(TechProduct) && TechProduct != "null" && TechProduct != "undefined")
            {
                query = query.Where(res => CddFilters.Contains(res.ProdGrp_MIS_Name));
            }
            //&& string.IsNullOrEmpty(CDD) && string.IsNullOrEmpty(PDD) && string.IsNullOrEmpty(Stages)
            //   var res = query in q  CddFilters
            //return await 
            return await query.ToListAsync();


        }
        public async Task<IEnumerable<vw_BacklogOrder_Tech_Spec_Summary>> Search_BacklogOrder_Tech_Spec_Summary(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo, int CDDYear,string MonthYear)
        {
            IQueryable<vw_BacklogOrder_Tech_Spec_Summary> query = _applicationDbContext.vw_BacklogOrder_Tech_Spec_Summary;

            string[] company = company_Code.Split(',');

            string[] CustCDD = CDD.Split(',');
            string[] CustPDD = PDD.Split(',');
            string[] Staging = Stages.Split(',');
            string[] Cust = Customer.Split(',');
            string[] OrderNos = OrderNo.Split(',');
            
            // string[] CddFilters = CDDFilter.Split(',');
            if (!string.IsNullOrEmpty(company_Code) && company_Code.Length > 0 && company_Code != "null" && company_Code != "undefined")
            {
                query = query.Where(res => company.Contains(res.Company_Code.Trim()));
            }
            if (!string.IsNullOrEmpty(CDD) && CDD != "null" && CDD != "undefined")
            {
                query = query.Where(res => CustCDD.Contains(res.Filter_CDD));
            }
            if (!string.IsNullOrEmpty(PDD) && PDD != "null" && PDD != "undefined")
            {
                query = query.Where(res => CustPDD.Contains(res.Filter_PDD));
            }
            if (!string.IsNullOrEmpty(Stages) && Stages != "null" && Stages != "undefined")
            {
                query = query.Where(res => Staging.Contains(res.Filter_Stages));
            }
            if (!string.IsNullOrEmpty(Customer) && Customer != "null" && Customer != "undefined")
            {
                query = query.Where(res => Cust.Contains(res.Customer_Name));
            }
            if (!string.IsNullOrEmpty(OrderNo) && OrderNo != "null" && OrderNo != "undefined")
            {
                query = query.Where(res => OrderNos.Contains(res.COrder_Serial_No));
            }
            if (CDDYear > 1950)
            {
                query = query.Where(res => res.CDD_Year == CDDYear);
                query = query.Where(res => res.Filter_CDD == "Delay");
            }
            if (!string.IsNullOrEmpty(MonthYear) && MonthYear != "null" && MonthYear != "undefined")
            {
                string Month = MonthYear.Substring(0, 3);
                string Year = MonthYear.Substring(4, 4);
                query = query.Where(res => res.CDD_Year == Convert.ToInt32(Year) && res.CDD_Month==Month);
            }
               
            //&& string.IsNullOrEmpty(CDD) && string.IsNullOrEmpty(PDD) && string.IsNullOrEmpty(Stages)
            //   var res = query in q  CddFilters
            //return await 
            return await query.ToListAsync();


        }

        public async Task<IEnumerable<vw_BacklogOrder_Tech_Spec_Summary>> Search_BacklogOrder_Tech_Spec_Summary(string company_Code, string CDD, string PDD, string Stages, string Customer, string OrderNo, string CDDFilter, int CDDYear, string MonthYear)
        {
            IQueryable<vw_BacklogOrder_Tech_Spec_Summary> query = _applicationDbContext.vw_BacklogOrder_Tech_Spec_Summary;

            string[] company = company_Code.Split(',');

            string[] CustCDD = CDD.Split(',');
            string[] CustPDD = PDD.Split(',');
            string[] Staging = Stages.Split(',');
            string[] Cust = Customer.Split(',');
            string[] OrderNos = OrderNo.Split(',');

            // string[] CddFilters = CDDFilter.Split(',');
            if (!string.IsNullOrEmpty(company_Code) && company_Code.Length > 0 && company_Code != "null" && company_Code != "undefined")
            {
                query = query.Where(res => company.Contains(res.Company_Code.Trim()));
            }
            if (!string.IsNullOrEmpty(CDD) && CDD != "null" && CDD != "undefined")
            {
                query = query.Where(res => CustCDD.Contains(res.Filter_CDD));
            }
            if (!string.IsNullOrEmpty(PDD) && PDD != "null" && PDD != "undefined")
            {
                query = query.Where(res => CustPDD.Contains(res.Filter_PDD));
            }
            if (!string.IsNullOrEmpty(Stages) && Stages != "null" && Stages != "undefined")
            {
                query = query.Where(res => Staging.Contains(res.Filter_Stages));
            }
            if (!string.IsNullOrEmpty(Customer) && Customer != "null" && Customer != "undefined")
            {
                query = query.Where(res => Cust.Contains(res.Customer_Name));
            }
            if (!string.IsNullOrEmpty(OrderNo) && OrderNo != "null" && OrderNo != "undefined")
            {
                query = query.Where(res => OrderNos.Contains(res.COrder_Serial_No));
            }
            if (CDDYear > 1950)
            {
                query = query.Where(res => res.CDD_Year == CDDYear);
                query = query.Where(res => res.Filter_CDD == "Delay");
            }
            if (!string.IsNullOrEmpty(MonthYear) && MonthYear != "null" && MonthYear != "undefined")
            {
                string Month = MonthYear.Substring(0, 3);
                string Year = MonthYear.Substring(4, 4);
                query = query.Where(res => res.CDD_Year == Convert.ToInt32(Year) && res.CDD_Month == Month);
            }

            //&& string.IsNullOrEmpty(CDD) && string.IsNullOrEmpty(PDD) && string.IsNullOrEmpty(Stages)
            //   var res = query in q  CddFilters
            //return await 
            return await query.ToListAsync();


        }


    }
}
