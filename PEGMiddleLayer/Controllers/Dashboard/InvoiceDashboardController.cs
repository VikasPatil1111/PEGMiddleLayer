using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEGMiddleLayer.DIPattern;
using PEGMiddleLayer.Models.Common;
using PEGMiddleLayer.Models.Dashboard.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Controllers.Dashboard
{
    [Authorize(Roles = "SuperAdmin,InvoiceDashboard,BacklogInvoice")]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDashboardController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICommanRepository _commanRepository;
        private readonly ItblBI_PeriodRepository _tblBI_PeriodRepository;
        public InvoiceDashboardController(IInvoiceRepository invoiceRepository,ICommanRepository commanRepository, ItblBI_PeriodRepository itblBI_PeriodRepository)
        {
            _invoiceRepository = invoiceRepository;
            _commanRepository = commanRepository;
            _tblBI_PeriodRepository = itblBI_PeriodRepository;
        }

        [HttpGet]
        [Route("SupplierName")]
        //public async Task<ActionResult<tblBI_Invoice_Details>> getSupplierName() {
        public async Task<ActionResult<vw_InvoiceCustomerName>> getSupplierName()   
        {
            try
            {
                // var result = await _invoiceRepository.tblBI_Invoice_Details();
                var result = await _invoiceRepository.vw_InvoiceCustomerNames();
                return Ok(result.Select(res =>
                                            new {
                                                Customer_Code = res.Customer_Code.Trim(),
                                                Customer_Name = res.Customer_Name.Trim(),
                                                GSTIN = res.GSTIN.Trim(),
                                                PANNO = res.PAN_No.Trim()
                                            })
                    .OrderBy(res => res.PANNO).ThenBy(res => res.Customer_Code));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..") ;//NotFound(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
                
            }
        }

        [HttpGet]
        [Route("CompanySummaryValue/{year}/{Company_Code}/{Product}/{Customer_Code}/{QtyValue}")]
        public async Task<ActionResult<vw_Invoice_Tech_Deatils>> getCompanySummaryValue(int year,string Company_Code, string Product,string Customer_Code,string QtyValue) 
        {
            try
            {
                //string Product="";
                // string Customer_Code=""; string QtyValue="";
                // int Year = 2022; /{Product}/{Customer_Code}/{QtyValue} int? year, string Company_Code, string Product, string Customer_Code
                string _fromdate = "01/04/" + Convert.ToString(year);
                string _todate = "31/03/" + Convert.ToString(year+1);
                DateTime FromDate = Convert.ToDateTime(_fromdate);
                DateTime ToDate = Convert.ToDateTime(_todate);
                var result = await _invoiceRepository.vw_Invoice_Tech_Deatils(year, Company_Code, Product, Customer_Code);

                var setYear = year == null || year==0 ? System.DateTime.Now.Year : year;
                int Year = Convert.ToInt32(setYear);

               // var result = await _bookingDashboardRepository.bookingTables();
                var Targetresult = await _invoiceRepository.So_Invoice_Target(year, Company_Code);
                // var companyUser = await _companyMasterRepository.tblCompanyUsers();
                double setMonth = _commanRepository.CalculateFinancialMonthDifference(FromDate, ToDate,Year);
                
               // int month = 0;


              
                if (QtyValue == "Qty") {

                    var query1 = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Company_Code, res.ID }).

                                           Select(res => new
                                           {
                                               CompanyID = res.Key.ID,
                                               Companycode = res.Key.Company_Code,
                                               Invoice_Value = Math.Round((decimal)res.Sum(res => res.Invoice_Quantity), 0),
                                               Invoice_Qty = Math.Round((decimal)res.Sum(res => res.Invoice_Quantity), 0),
                                               Target_Value = 1,
                                               Target_YTD = 1

                                           }).OrderBy(res => res.CompanyID);

                    var queryTotal = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Invoice_Year }).
                                              Select(res => new
                                              {
                                                  CompanyID = 4,
                                                  Companycode = "PEG",
                                                  
                                                  Invoice_Value = Math.Round((decimal)res.Sum(res => res.Invoice_Quantity), 0),
                                                  Invoice_Qty = Math.Round((decimal)res.Sum(res => res.Invoice_Quantity), 0),
                                                  Target_Value = 1,
                                                  Target_YTD = 1
                                              });

                    return Ok(query1.Union(queryTotal));

                } else { 
                var query1 = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Company_Code,res.ID }).

                                           Select(res => new
                                           {
                                               CompanyID = res.Key.ID,
                                               Companycode = res.Key.Company_Code,
                                               Invoice_Value = Math.Round((decimal)res.Sum(res => res.Item_ValueIn_Lakhs), 0),
                                               Invoice_Qty = Math.Round((decimal)res.Sum(res => res.Invoice_Quantity), 0),
                                               Target_Value = from target in Targetresult
                                                              .GroupBy(tar => new { tar.Company_Code, tar.Year })
                                                              .Where(tar => tar.Key.Company_Code == res.Key.Company_Code && tar.Key.Year == Year)
                                                              select target.Sum(tar => tar.Target).Value,
                                               Target_YTD = from target in Targetresult
                                                            .GroupBy(tar => new { tar.Company_Code, tar.Year })
                                                            .Where(tar => tar.Key.Company_Code == res.Key.Company_Code && tar.Key.Year == Year)
                                                                // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code).                                                               
                                                            select Math.Round((target.Sum(tar => tar.Target).Value / 12) * setMonth, 0)

                                           }).OrderBy(res => res.CompanyID);

                var queryTotal = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Invoice_Year }).
                                          Select(res => new
                                          {
                                              CompanyID = 4,
                                              Companycode = "PEG",
                                              Invoice_Value = Math.Round((decimal)res.Sum(res => res.Item_ValueIn_Lakhs) , 0),
                                              Invoice_Qty = Math.Round((decimal)res.Sum(res => res.Invoice_Quantity), 0),
                                              Target_Value = from target in Targetresult
                                                             .GroupBy(tar => new { tar.Year })
                                                             .Where(tar => tar.Key.Year == Year)
                                                                 // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code)
                                                             select target.Sum(tar => tar.Target).Value,
                                              Target_YTD = from target in Targetresult
                                                              .GroupBy(tar => new { tar.Year })
                                                              .Where(tar => tar.Key.Year == Year)
                                                               // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code)
                                                           select Math.Round((target.Sum(tar => tar.Target).Value / 12) * setMonth, 0)
                                          });

                return Ok(query1.Union(queryTotal));
                }
                //return Ok(result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Company_Code }).
                //    Select(res => new { 
                //         Companycode = res.Key.Company_Code,
                //         Invoice_Value = Math.Round( (double)res.Sum(res=>res.Item_ValueIn_Lakhs),0),
                //         Invoice_Qty = res.Sum(res => res.Invoice_Quantity)}).OrderBy(res => res.Companycode)
                //    );
            }
            catch (Exception ex)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, "");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }

        [HttpGet]
        [Route("GetDropDownListValues/{Filter}")]
        public async Task<ActionResult<vw_Invoice_Tech_Deatils>> GetDropDownListValues(string Filter)
        {
            try
            {
                var result = await _invoiceRepository.vw_Invoice_Tech_Deatils(0, "null", "null",null);

                if (Filter == "Comapny")
                {
                    var query = result.OrderBy(res => res.Company_Code).Select(res => res.Company_Code).Distinct();
                    return Ok(query);
                }
                else if (Filter == "Year")
                {
                    var query = result.OrderBy(res => res.Invoice_Year).Select(res => res.Invoice_Year).Distinct();
                    return Ok(query);
                }
                else if (Filter == "Product")
                {
                    var query = result.OrderBy(res => res.ProdGrp_MIS_Id).Select(res => res.ProdGrp_MIS_Name).Distinct();
                    return Ok(query);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
               
            }
            
        }
        [HttpGet]
        [Route("ProductWiseDetails/{Year}/{Company_Code}/{Product}/{Customer_Code}/{QtyValue}")]
        public async Task<ActionResult<vw_Invoice_Tech_Deatils>> ProductWiseDetails(int Year, string Company_Code, string Product, string Customer_Code, string QtyValue)
        {
            try
            {
                //year, Company_Code, Product, Customer_Code
                var result = await _invoiceRepository.vw_Invoice_Tech_Deatils(Year, Company_Code, Product, Customer_Code);
                /*      
                  var query = result.GroupBy(res => new { res.Invoice_Year, res.ProdGrp_MIS_Id, res.ProdGrp_MIS_Name })
                      .Select(res => new
                  {
                    //  Invoice_Year = res.Key.Invoice_Year,
                      Product_Id = res.Key.ProdGrp_MIS_Id,
                      Product_Name = res.Key.ProdGrp_MIS_Name,
                      Invoice_Qty = res.Sum(res => res.Invoice_Quantity),
                      Invoice_Value = Math.Round( (double) res.Sum(res => res.Item_ValueIn_Lakhs),0)
                  }).OrderBy(res => res.Product_Id);
                */
                if (QtyValue == "Qty")
                {
                    var query = result.GroupBy(res => new { res.Invoice_Year, res.ProdGrp_MIS_Name_L2,res.ProdGrp_MIS_Name_L2_SrNo })
                         .Select(res => new
                         {
                         //  Invoice_Year = res.Key.Invoice_Year,
                             Product_Id = res.Key.ProdGrp_MIS_Name_L2_SrNo,
                             Product_Name = res.Key.ProdGrp_MIS_Name_L2,
                             Invoice_Qty = res.Sum(res => res.Invoice_Quantity),
                             Invoice_Value = res.Sum(res => res.Invoice_Quantity)
                         }).OrderBy(res => res.Product_Id);

                    return Ok(query);
                }
                else {
                    var query = result.GroupBy(res => new { res.Invoice_Year, res.ProdGrp_MIS_Name_L2,res.ProdGrp_MIS_Name_L2_SrNo })
                            .Select(res => new
                            {
                             //  Invoice_Year = res.Key.Invoice_Year,
                                Product_Id = res.Key.ProdGrp_MIS_Name_L2_SrNo,
                                Product_Name = res.Key.ProdGrp_MIS_Name_L2,
                                Invoice_Qty = res.Sum(res => res.Invoice_Quantity),
                                Invoice_Value = Math.Round((double)res.Sum(res => res.Item_ValueIn_Lakhs), 0)
                            }).OrderBy(res => res.Product_Id);

                    return Ok(query);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }

        [HttpGet]
        [Route("ProductWiseDrillDown/{Year}/{Company_Code}/{Product}/{Customer_Code}/{QtyValue}/{ProdLevel1}")]
        public async Task<ActionResult<vw_Invoice_Tech_Deatils>> ProductWiseDrillDown(int Year, string Company_Code, string Product, string Customer_Code, string QtyValue,string ProdLevel1)
        {
            try
            {
                //year, Company_Code, Product, Customer_Code
                var result = await _invoiceRepository.vw_Invoice_Tech_Deatils(Year, Company_Code, Product, Customer_Code);

                if (QtyValue == "Qty")
                {
                    var query = result.Where(res => res.ProdGrp_MIS_Name_L2 == ProdLevel1).GroupBy(res => new { res.Invoice_Year, res.ProdGrp_MIS_Id, res.ProdGrp_MIS_Name })
                        .Select(res => new
                        {
                        //Invoice_Year = res.Key.Invoice_Year,
                        Product_Id = res.Key.ProdGrp_MIS_Id,
                            Product_Name = res.Key.ProdGrp_MIS_Name,
                            Invoice_Qty = res.Sum(res => res.Invoice_Quantity),
                            Invoice_Value = res.Sum(res => res.Invoice_Quantity),
                        }).OrderBy(res => res.Product_Id);



                    return Ok(query);
                }
                else {
                    var query = result.Where(res => res.ProdGrp_MIS_Name_L2 == ProdLevel1).GroupBy(res => new { res.Invoice_Year, res.ProdGrp_MIS_Id, res.ProdGrp_MIS_Name })
                            .Select(res => new
                            {
                            //Invoice_Year = res.Key.Invoice_Year,
                            Product_Id = res.Key.ProdGrp_MIS_Id,
                                Product_Name = res.Key.ProdGrp_MIS_Name,
                                Invoice_Qty = res.Sum(res => res.Invoice_Quantity),
                                Invoice_Value = Math.Round((double)res.Sum(res => res.Item_ValueIn_Lakhs), 0)
                            }).OrderBy(res => res.Product_Id);



                    return Ok(query);
                }

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }
        [HttpGet]
        [Route("YearlySummary/{Year}/{Company_Code}/{Product}/{Customer_Code}/{QtyValue}")]
        public async Task<ActionResult<vw_Invoice_Tech_Deatils>> YearlySummary(int Year, string Company_Code, string Product, string Customer_Code,string QtyValue)
        {
            try
            {
                var result = await _invoiceRepository.InvoiceCompanywiseYearlySummary(Year, Company_Code, Product, Customer_Code);

                if (QtyValue == "Qty") {
                    return Ok(result.GroupBy(res => new { res.Invoice_Year }).Select(
                      res => new {
                          Year = res.Key.Invoice_Year,
                          IV1 = Math.Round((double)res.Sum(res => res.IV1_Qty), 0),
                          IV2 = Math.Round((double)res.Sum(res => res.IV2_Qty), 0),
                          EIPL = Math.Round((double)res.Sum(res => res.EIPL_Qty), 0),
                          PEG = Math.Round((double)res.Sum(res => res.IV1_Qty), 0) + Math.Round((double)res.Sum(res => res.IV2_Qty), 0) + Math.Round((double)res.Sum(res => res.EIPL_Qty), 0)

                      }
                      ).OrderBy(res => res.Year));

                } else { 
                return Ok(result.GroupBy(res => new { res.Invoice_Year }).Select(
                    res => new { Year = res.Key.Invoice_Year,
                                  IV1 = Math.Round((double)res.Sum(res=> res.IV1)  ,0),
                        IV2 = Math.Round((double)res.Sum(res => res.IV2), 0),
                        EIPL = Math.Round((double)res.Sum(res => res.EIPL), 0),
                        PEG = Math.Round((double)res.Sum(res => res.IV1), 0) + Math.Round((double)res.Sum(res => res.IV2), 0) + Math.Round((double)res.Sum(res => res.EIPL), 0)

                    }
                    ).OrderBy(res=> res.Year));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");             

            }
        }

        [HttpGet]
        [Route("ProjectRevenueSummary/{Year}/{Flag}/{Company_Code}/{Product}/{Customer_Code}/{QtyValue}")]
        public async Task<ActionResult<vw_Invoice_Tech_Deatils>> ProjectRevenueSummary(int Year,bool Flag,  string Company_Code, string Product, string Customer_Code,string QtyValue)
        {
            try
            {
                Boolean chk = Flag;
                if (Flag)
                {
                    var result = await _invoiceRepository.InvoiceDetailsOrderCategory(Year, Company_Code, Product, Customer_Code);

                    if(QtyValue == "Qty") 
                    {

                        var query = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Company_Code, res.Invoice_Year })
                        .Select(res => new
                        {
                            Company_Code = res.Key.Company_Code,
                            //Year = res.Key.Inv,
                            DEALER = Math.Round((double)res.Sum(res => res.DEALER_Qty), 0),
                            PROJECT = Math.Round((double)res.Sum(res => res.PROJECT_Qty), 0),
                            REVENUE = Math.Round((double)res.Sum(res => res.REVENUE_Qty), 0),
                            Other = Math.Round((double)res.Sum(res => res.Other_Qty),0)

                        }).OrderBy(res => res.Company_Code);

                        var query1 = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Invoice_Year })
                            .Select(res => new
                            {
                                Company_Code = "PEG",
                            //Year = res.Key.Inv,
                            DEALER = Math.Round((double)res.Sum(res => res.DEALER_Qty), 0),
                                PROJECT = Math.Round((double)res.Sum(res => res.PROJECT_Qty), 0),
                                REVENUE = Math.Round((double)res.Sum(res => res.REVENUE_Qty), 0),
                                Other = Math.Round((double)res.Sum(res => res.Other_Qty), 0)

                            });

                        return Ok(query.Union(query1));

                    } 
                    else 
                    {
                        /*  
                        if (QtyValue == "Qty")
                        {
                            var query = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Company_Code, res.Invoice_Year })
                            .Select(res => new
                            {
                            Company_Code = res.Key.Company_Code,
                            //Year = res.Key.Inv,
                            DEALER = Math.Round((double)res.Sum(res => res.DEALER_Qty), 0),
                            PROJECT = Math.Round((double)res.Sum(res => res.PROJECT_Qty), 0),
                            REVENUE = Math.Round((double)res.Sum(res => res.REVENUE_Qty), 0),
                            Other = Math.Round((double)res.Sum(res => res.Other_Qty), 0)

                   }).OrderBy(res => res.Company_Code);

                            var query1 = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Invoice_Year })
                                .Select(res => new
                                {
                                    Company_Code = "PEG",
                                    //Year = res.Key.Inv,
                                    DEALER = Math.Round((double)res.Sum(res => res.DEALER_Qty), 0),
                                    PROJECT = Math.Round((double)res.Sum(res => res.PROJECT_Qty), 0),
                                    REVENUE = Math.Round((double)res.Sum(res => res.REVENUE_Qty), 0),
                                    Other = Math.Round((double)res.Sum(res => res.Other_Qty), 0)

                                });

                            return Ok(query.Union(query1));

                        }
                        else
                        {
                            */

                           var query = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Company_Code, res.Invoice_Year })
                        .Select(res => new
                        {
                            Company_Code = res.Key.Company_Code,
                            //Year = res.Key.Inv,
                            DEALER = Math.Round((double)res.Sum(res => res.DEALER), 0),
                            PROJECT = Math.Round((double)res.Sum(res => res.PROJECT), 0),
                            REVENUE = Math.Round((double)res.Sum(res => res.REVENUE), 0),
                            Other = Math.Round((double)res.Sum(res => res.Other), 0)

                        }).OrderBy(res => res.Company_Code);

                            var query1 = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Invoice_Year })
                                .Select(res => new
                                {
                                    Company_Code = "PEG",
                            //Year = res.Key.Inv,
                            DEALER = Math.Round((double)res.Sum(res => res.DEALER), 0),
                                    PROJECT = Math.Round((double)res.Sum(res => res.PROJECT), 0),
                                    REVENUE = Math.Round((double)res.Sum(res => res.REVENUE), 0),
                                    Other = Math.Round((double)res.Sum(res => res.Other), 0)

                                });

                            return Ok(query.Union(query1));
                        }
                  //  }
                }
                else
                {
                    if (QtyValue == "Qty") 
                    {
                        var result = await _invoiceRepository.InvoiceCompanywiseCategorySummary(Year, Company_Code, Product, Customer_Code);
                        return Ok(result.GroupBy(res => new { res.Order_Category }).Select(
                            res => new
                            {
                                Order_Category = res.Key.Order_Category,
                                IV1 = Math.Round((double)res.Sum(res => res.IV1_Qty), 0),
                                IV2 = Math.Round((double)res.Sum(res => res.IV2_Qty), 0),
                                EIPL = Math.Round((double)res.Sum(res => res.EIPL_Qty), 0),
                                PEG = Math.Round((double)res.Sum(res => res.IV1_Qty), 0) + Math.Round((double)res.Sum(res => res.IV2_Qty), 0) + Math.Round((double)res.Sum(res => res.EIPL_Qty), 0)

                            }
                            ).OrderBy(res => res.Order_Category));
                    }
                    else
                    {
                        var result = await _invoiceRepository.InvoiceCompanywiseCategorySummary(Year, Company_Code, Product, Customer_Code);
                        return Ok(result.GroupBy(res => new { res.Order_Category }).Select(
                            res => new
                            {
                                Order_Category = res.Key.Order_Category,
                                IV1 = Math.Round((double)res.Sum(res => res.IV1), 0),
                                IV2 = Math.Round((double)res.Sum(res => res.IV2), 0),
                                EIPL = Math.Round((double)res.Sum(res => res.EIPL), 0),
                                PEG = Math.Round((double)res.Sum(res => res.IV1), 0) + Math.Round((double)res.Sum(res => res.IV2), 0) + Math.Round((double)res.Sum(res => res.EIPL), 0)

                            }
                            ).OrderBy(res => res.Order_Category));
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");               
            }
        }

        [HttpGet]
        [Route("MonthWiseInvoiceSummary/{Year}/{Company_Code}/{Product}/{Customer_Code}/{QtyValue}")]
        public async Task<ActionResult<tblBI_Invoice_Details>> MonthWiseInvoiceSummary(int Year , string Company_Code, string Product, string Customer_Code,string QtyValue)
        {
            try
            {
                var result = await _invoiceRepository.InvoiceCompanywiseYearlySummary(Year, Company_Code, Product, Customer_Code);
                if (QtyValue == "Qty") {

                    var query = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Invoice_Date.Value.Year, res.Invoice_Date.Value.Month }).Select(res =>
                                              new
                                              {
                                                  Year = _commanRepository.getMonthName(res.Key.Month)

                                                  + '-' + (res.Key.Year).ToString(),
                                                  IV1 = Math.Round((double)res.Sum(res => res.IV1_Qty), 0),
                                                  IV2 = Math.Round((double)res.Sum(res => res.IV2_Qty), 0),
                                                  EIPL = Math.Round((double)res.Sum(res => res.EIPL_Qty), 0),
                                                  PEG = Math.Round((double)res.Sum(res => res.IV1_Qty), 0) + Math.Round((double)res.Sum(res => res.IV2_Qty), 0) + Math.Round((double)res.Sum(res => res.EIPL_Qty), 0),
                                                  IV1_Qty = res.Sum(res => res.IV1_Qty),
                                                  IV2_Qty = res.Sum(res => res.IV2_Qty),
                                                  EIPL_Qty = res.Sum(res => res.EIPL_Qty),

                                                  Month = res.Key.Month,
                                                  Year1 = res.Key.Year
                                              })
                                             .OrderBy(res => res.Year1).ThenBy(res => res.Month);
                    return Ok(query);
                }
                else
                {
                    var query = result.Where(res => res.Invoice_Year == Year).GroupBy(res => new { res.Invoice_Date.Value.Year, res.Invoice_Date.Value.Month }).Select(res =>
                                              new
                                              {
                                                  Year = _commanRepository.getMonthName(res.Key.Month)

                                                  + '-' + (res.Key.Year).ToString(),
                                                  IV1 = Math.Round((double)res.Sum(res => res.IV1), 0),
                                                  IV2 = Math.Round((double)res.Sum(res => res.IV2), 0),
                                                  EIPL = Math.Round((double)res.Sum(res => res.EIPL), 0),
                                                  PEG = Math.Round((double)res.Sum(res => res.IV1), 0) + Math.Round((double)res.Sum(res => res.IV2), 0) + Math.Round((double)res.Sum(res => res.EIPL), 0),
                                                  IV1_Qty = res.Sum(res => res.IV1_Qty),
                                                  IV2_Qty = res.Sum(res => res.IV2_Qty),
                                                  EIPL_Qty = res.Sum(res => res.EIPL_Qty),

                                                  Month = res.Key.Month,
                                                  Year1 = res.Key.Year
                                              })
                                             .OrderBy(res => res.Year1).ThenBy(res => res.Month);
                    return Ok(query);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }

        [HttpGet]
        [Route("BranchWiseInvoiceSummary/{Year}/{Company_Code}/{Product}/{Customer_Code}/{QtyValue}")]
        public async Task<ActionResult<tblBI_Invoice_Details>> BranchWiseInvoiceSummary(int Year, string Company_Code, string Product, string Customer_Code, string QtyValue)
        {
            try
            {
                var result = await _invoiceRepository.BranchwiseYearlySummary(Year, Company_Code, Product, Customer_Code);

                if (QtyValue == "Qty")
                {
                    return Ok(result.GroupBy(res => new { res.Branch_Name,res.Branch_Serial_No }).Select(
                      res => new {
                          Year = res.Key.Branch_Name,
                          Branch_Code = res.Key.Branch_Serial_No,
                          IV1 = Math.Round((double)res.Sum(res => res.IV1_Qty), 0),
                          IV2 = Math.Round((double)res.Sum(res => res.IV2_Qty), 0),
                          EIPL = Math.Round((double)res.Sum(res => res.EIPL_Qty), 0),
                          PEG = Math.Round((double)res.Sum(res => res.IV1_Qty), 0) + Math.Round((double)res.Sum(res => res.IV2_Qty), 0) + Math.Round((double)res.Sum(res => res.EIPL_Qty), 0)
                      }
                      ).OrderBy(res => res.Branch_Code));
                }
                else
                {
                    return Ok(result.GroupBy(res => new { res.Branch_Name, res.Branch_Serial_No }).Select(
                        res => new {
                            Year = res.Key.Branch_Name,
                            Branch_Code = res.Key.Branch_Serial_No,
                            IV1 = Math.Round((double)res.Sum(res => res.IV1), 0),
                            IV2 = Math.Round((double)res.Sum(res => res.IV2), 0),
                            EIPL = Math.Round((double)res.Sum(res => res.EIPL), 0),
                            PEG = Math.Round((double)res.Sum(res => res.IV1), 0) + Math.Round((double)res.Sum(res => res.IV2), 0) + Math.Round((double)res.Sum(res => res.EIPL), 0)

                        }
                        ).OrderBy(res => res.Branch_Code));
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }

        [HttpGet]
        [Route("GetPeriod/{Year}")]
        public async Task<ActionResult<tblBI_Period>> gettblBI_Periods(int Year)
        {
            var result = await _tblBI_PeriodRepository.gettblBI_Periods();
            if (Year == System.DateTime.Now.Year-1)
            {
                var query = result.Where(res => res.Year == Year && res.Module == "INVOICE")
                            .Select(res => new
                            {
                                FromDate = res.FromDate.ToString("dd/MM/yyyy"),
                                ToDate = System.DateTime.Now.ToString("dd/MM/yyyy")
                            });
                return Ok(query);
            }
            else
            {
                var query = result.Where(res => res.Year == Year && res.Module == "INVOICE")
                              .Select(res => new
                              {
                                  FromDate = res.FromDate.ToString("dd/MM/yyyy"),
                                  ToDate = res.ToDate.ToString("dd/MM/yyyy")
                              });
                return Ok(query);
            }

        }

    }
}
