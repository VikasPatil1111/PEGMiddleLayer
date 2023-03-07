using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEGMiddleLayer.DIPattern;
using PEGMiddleLayer.Models.Dashboard.Invoice;
using PEGMiddleLayer.Models.Dashboard.InvoiceMargin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Controllers.Dashboard
{
    [Authorize(Roles = "SuperAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceMarginController : ControllerBase
    {
        public readonly IInvoiceMarginRepository _invoiceMarginRepository;
        private readonly ICommanRepository _commanRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceMarginController(IInvoiceMarginRepository invoiceMarginRepository, ICommanRepository commanRepository, IInvoiceRepository invoiceRepository)
        {
            _invoiceMarginRepository = invoiceMarginRepository;
            _commanRepository = commanRepository;
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        [Route("GetDropDownListValues/{Filter}")]
        public async Task<ActionResult<vw_InvoiceMarginCustomerList>> GetDropDownListValues(string Filter)
        {
            try
            {
                var result = await _invoiceMarginRepository.Vw_InvoiceMarginCustomerLists();

                if (Filter == "Customer")
                {
                    var query = result.Select(res => new
                    {
                        Customer_Code = res.Customer_Code,
                        Customer_Name = res.Customer_Name,
                        GSTIN = res.GSTIN,
                        PANNO = res.PAN_No
                    }).Distinct().OrderBy(res => res.PANNO).ThenBy(res => res.Customer_Code);
                    return Ok(query);
                }
                else if (Filter == "Company")
                {
                    var query = result.OrderBy(res => res.Company_Code).Select(res => res.Company_Code).Distinct();
                    return Ok(query);
                }

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error please later.....");

            }
        }
        [HttpGet]
        [Route("GetDropDownListValue/{Filter}")]
        public async Task<ActionResult<vw_InvoiceMargin_Tech_Details>> GetDropDownListValue(string Filter)
        {
            try
            {
                var result = await _invoiceMarginRepository.vw_InvoiceMargin_Tech_Details();

                if (Filter == "Year")
                {
                    var query = result.OrderBy(res => res.Invoice_Year).Select(res => res.Invoice_Year).Distinct();
                    return Ok(query);
                }
                else if (Filter == "Month")
                {
                    var query = result.OrderBy(res =>
                    res.Invoice_Date.Value.Month)
                        .Select(res => new { Month = res.Invoice_Date.Value.Month, MonthName = _commanRepository.getMonthName((int)res.Invoice_Date.Value.Month) }).Distinct();
                    return Ok(query);
                   // _commanRepository.getMonthName(res.Key.Month)
                }
                else if (Filter == "Order")
                {
                    //var query = result.OrderBy(res => res.).Select(res => res.Invoice_Year).Distinct();
                    //return Ok(query);
                }

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error please later.....");

            }
        }
        [HttpGet]
        [Route("CompanySummaryValue/{year}/{company}/{Customer}/{Month}")]
        public async Task<ActionResult<vw_InvoiceMargin_Tech_Details>> getCompanySummaryValue(int year,string company,string Customer,int Month) //, string Company_Code, string Product, string Customer_Code, string QtyValue
        {
            try
            {
                //string Product="";
                // string Customer_Code=""; string QtyValue="";
                // int Year = 2022; /{Product}/{Customer_Code}/{QtyValue} int? year, string Company_Code, string Product, string Customer_Code
                string _fromdate = "01/04/" + Convert.ToString(year);
                string _todate = "31/03/" + Convert.ToString(year + 1);
                DateTime FromDate = Convert.ToDateTime(_fromdate);
                DateTime ToDate = Convert.ToDateTime(_todate);
                var result = await _invoiceMarginRepository.vw_InvoiceMargin_Tech_Details(year,company,Customer); //, Company_Code, Product, Customer_Code
                var resultMonth = await _invoiceMarginRepository.vw_InvoiceMargin_Tech_Details(year, company, Customer,Month);

                var setYear = year == null || year == 0 ? System.DateTime.Now.Year : year;
                int Year = Convert.ToInt32(setYear);

                // var result = await _bookingDashboardRepository.bookingTables();
                var Targetresult = await _invoiceRepository.So_Invoice_Target(year, "null");
                // var companyUser = await _companyMasterRepository.tblCompanyUsers();
                double setMonth = _commanRepository.CalculateFinancialMonthDifference(FromDate, ToDate, Year);

                // int month = 0;                
                    var query1 = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Company_Code, res.ID }).

                                               Select(res => new
                                               {
                                                   CompanyID = res.Key.ID,
                                                   Companycode = res.Key.Company_Code,
                                                   Invoice_Value = Math.Round((decimal)res.Sum(res => res.Item_ValueIn_Lakhs), 0),
                                                   Invoice_Qty = Math.Round((decimal)res.Sum(res => res.Invoice_Quantity), 0),
                                                   MC_Value = Math.Round((decimal)res.Sum(res => res.TOTAL_INVMC)/100000, 0),
                                                   Target_Value = from target in Targetresult
                                                             .GroupBy(tar => new { tar.Company_Code, tar.Year })
                                                             .Where(tar => tar.Key.Company_Code == res.Key.Company_Code && tar.Key.Year == Year)
                                                                  select target.Sum(tar => tar.Target).Value,
                                                   Target_YTD = from target in Targetresult
                                                                .GroupBy(tar => new { tar.Company_Code, tar.Year })
                                                                .Where(tar => tar.Key.Company_Code == res.Key.Company_Code && tar.Key.Year == Year)
                                                                    // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code).                                                               
                                                                select Math.Round((target.Sum(tar => tar.Target).Value / 12) * setMonth, 0),
                                                   // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code).                                                               
                                                   MTD_Inv_Value = from MonthValue in resultMonth
                                                             .GroupBy(tar => new { tar.Company_Code })
                                                              .Where(tar => tar.Key.Company_Code == res.Key.Company_Code )
                                                               select Math.Round(MonthValue.Sum(tar => tar.Item_ValueIn_Lakhs).Value,0),
                                                   MTD_MC_Value = from MonthValue in resultMonth
                                                              .GroupBy(tar => new { tar.Company_Code })
                                                               .Where(tar => tar.Key.Company_Code == res.Key.Company_Code )
                                                                   select Math.Round(MonthValue.Sum(tar =>  tar.TOTAL_INVMC / 100000).Value,0)
                                               }).OrderBy(res => res.CompanyID);

                    var queryTotal = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Invoice_Year }).
                                              Select(res => new
                                              {
                                                  CompanyID = 4,
                                                  Companycode = "PEG",
                                                  Invoice_Value = Math.Round((decimal)res.Sum(res => res.Item_ValueIn_Lakhs), 0),
                                                  Invoice_Qty = Math.Round((decimal)res.Sum(res => res.Invoice_Quantity), 0),
                                                  MC_Value = Math.Round((decimal)res.Sum(res => res.TOTAL_INVMC) / 100000, 0),

                                                  Target_Value = from target in Targetresult
                                                              .GroupBy(tar => new { tar.Year })
                                                              .Where(tar => tar.Key.Year == Year)
                                                                     // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code)
                                                                 select target.Sum(tar => tar.Target).Value,
                                                  Target_YTD = from target in Targetresult
                                                                  .GroupBy(tar => new { tar.Year })
                                                                  .Where(tar => tar.Key.Year == Year)
                                                                   // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code)
                                                               select Math.Round((target.Sum(tar => tar.Target).Value / 12) * setMonth, 0),
                                                  MTD_Inv_Value = from MonthValue in resultMonth
                                                                .GroupBy(tar => new { tar.Invoice_Year })
                                                                select Math.Round( MonthValue.Sum(tar => tar.Item_ValueIn_Lakhs).Value,0),
                                                  MTD_MC_Value = from MonthValue in resultMonth
                                                  .GroupBy(tar => new { tar.Invoice_Year })
                                                                  select Math.Round(MonthValue.Sum(tar => tar.TOTAL_INVMC / 100000).Value, 0)
                                              });

                    return Ok(query1.Union(queryTotal));
                 
               
            }
            catch (Exception ex)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, "");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }
        [HttpGet]
        [Route("MonthWiseMarginSummary/{Year}/{company}/{Customer}/{Month}")] ///{Company_Code}/{Product}/{Customer_Code}/{QtyValue}
        public async Task<ActionResult<vw_InvoiceMargin_Tech_Details>> MonthWiseMarginSummary(int Year, string company, string Customer,int Month) //, string Company_Code, string Product, string Customer_Code, string QtyValue
        {
            try
            {
                var result = await _invoiceMarginRepository.vw_InvoiceMargin_Tech_Details(Year, company, Customer,Month); //, Company_Code, Product, Customer_Code

                var query = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Invoice_Date.Value.Year, res.Invoice_Date.Value.Month }).Select(res =>
                                              new
                                              {
                                                  Year = _commanRepository.getMonthName( res.Key.Month)

                                                  + '-' + (res.Key.Year).ToString(),
                                                  Invoice_Value = Math.Round((double)res.Sum(res => res.Item_ValueIn_Lakhs), 0),
                                                  MC_Value = Math.Round((double)res.Sum(res => res.TOTAL_INVMC)/100000, 0),
                                                  PER = Math.Round( Math.Round((double)res.Sum(res => res.TOTAL_INVMC) / 100000, 0)/ Math.Round((double)res.Sum(res => res.Item_ValueIn_Lakhs), 0)*100,0)+"%",

                                                  Month = res.Key.Month,
                                                  Year1 = res.Key.Year
                                              })
                                             .OrderBy(res => res.Year1).ThenBy(res => res.Month);
                    return Ok(query);
                 
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }

        [HttpGet]
        [Route("ProductMarginSummary/{Year}/{company}/{Customer}/{Month}")] ///{Company_Code}/{Product}/{Customer_Code}/{QtyValue}
        public async Task<ActionResult<vw_InvoiceMargin_Tech_Details>> ProductMarginSummary(int Year, string company, string Customer,int Month) //, string Company_Code, string Product, string Customer_Code, string QtyValue
        {
            try
            {
                var result = await _invoiceMarginRepository.vw_InvoiceMargin_Tech_Details(Year, company, Customer, Month); //, Company_Code, Product, Customer_Code

                var query = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.ProdGrp_MIS_Name_L2,res.ProdGrp_MIS_Name_L2_SrNo }).Select(res =>
                                              new
                                              {
                                                  Year = res.Key.ProdGrp_MIS_Name_L2,
                                                  Invoice_Value = Math.Round((double)res.Sum(res => res.Item_ValueIn_Lakhs), 0),
                                                  MC_Value = Math.Round((double)res.Sum(res => res.TOTAL_INVMC) / 100000, 0),
                                                  PER = Math.Round(Math.Round((double)res.Sum(res => res.TOTAL_INVMC) / 100000, 0) / Math.Round((double)res.Sum(res => res.Item_ValueIn_Lakhs), 0) * 100, 0) + "%",

                                                  SerialNO = res.Key.ProdGrp_MIS_Name_L2_SrNo,
                                                 
                                              })
                                             .OrderBy(res => res.SerialNO);
                return Ok(query);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }

        [HttpGet]
        [Route("TechProductMarginSummary/{Year}/{Product}/{company}/{Customer}/{Month}")] ///{Company_Code}/{Product}/{Customer_Code}/{QtyValue}
        public async Task<ActionResult<vw_InvoiceMargin_Tech_Details>> TechProductMarginSummary(int Year,string Product, string company, string Customer,int Month) //, string Company_Code, string Product, string Customer_Code, string QtyValue
        {
            try
            {
                var result = await _invoiceMarginRepository.vw_InvoiceMargin_Tech_Details(Year, company, Customer,Month); //, Company_Code, Product, Customer_Code

                var query = result.Where(res => res.Invoice_Year == Year && res.ProdGrp_MIS_Name_L2== Product).GroupBy(res => new { res.ProdGrp_MIS_Name, res.ProdGrp_MIS_Id }).Select(res =>
                                               new
                                               {
                                                   Product = res.Key.ProdGrp_MIS_Name,
                                                   Invoice_Value = Math.Round((double)res.Sum(res => res.Item_ValueIn_Lakhs), 0),
                                                   MC_Value = Math.Round((double)res.Sum(res => res.TOTAL_INVMC) / 100000, 0),
                                                   PER = Math.Round(Math.Round((double)res.Sum(res => res.TOTAL_INVMC) / 100000, 0) / Math.Round((double)res.Sum(res => res.Item_ValueIn_Lakhs), 0) * 100, 0) + "%",
                                                   SerialNO = res.Key.ProdGrp_MIS_Id,
                                               })
                                             .OrderBy(res => res.SerialNO);
                return Ok(query);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }

    }
}
