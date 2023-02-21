using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEGMiddleLayer.Models.Common;
using PEGMiddleLayer.Models.CompanySelection;
using PEGMiddleLayer.Models.Dashboard.Backlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Controllers.Dashboard
{
     [Authorize(Roles = "SuperAdmin,BacklogDashboard,Admin,BacklogInvoice")]
    [Route("api/[controller]")]
    [ApiController]
    public class BackLogOrderController : ControllerBase
    {
        private readonly IBackLogOrderRepository _backLogOrderRepository;
        private readonly ICompanyMasterRepository _companyMasterRepository;
        public BackLogOrderController(IBackLogOrderRepository backLogOrderRepository, ICompanyMasterRepository companyMasterRepository)
        {
            _backLogOrderRepository = backLogOrderRepository;
            _companyMasterRepository = companyMasterRepository;
        }

        [HttpGet]
        [Route("getBackLogOrderDetails/{Filter}")]
        public async Task<ActionResult<vw_Backlog_Order_Rev1>> getBackLogOrderDetails(string Filter) {
            try
            {
                var result = await _backLogOrderRepository.vw_Backlog_Order_Rev1();

                if (Filter == "STAGES")
                {
                   // var query = result.OrderBy(res => res.Filter_Stages).Select(res => res.Filter_Stages).Distinct();
                    var companyUser = await _companyMasterRepository.tblCompanyUsers();
                    var result1 = (from dataComany in result
                                   join userCompany in companyUser
                                   on dataComany.Company_Code.Trim() equals userCompany.CompanyCode.Trim()
                                   where userCompany.User_ID == setDatabase.UserId && userCompany.Allow == true
                                   orderby dataComany.Filter_Stages
                                   select new
                                   {
                                       dataComany.Filter_Stages
                                   }
                                   ).Distinct();
                    return Ok(result1);

                    //   var query = result.OrderBy(res => res.Filter_Stages).Select(res => res.Filter_Stages).Distinct();
                    //   return Ok(query);
                }
                else if (Filter == "COMPANY")
                {
                    var query = result.OrderBy(res => res.Company_Code).Select(res => res.Company_Code).Distinct();
                    var companyUser = await _companyMasterRepository.tblCompanyUsers();
                    var result1 = (from dataComany in result
                                   join userCompany in companyUser
                                   on dataComany.Company_Code.Trim() equals userCompany.CompanyCode.Trim()
                                   where userCompany.User_ID == setDatabase.UserId && userCompany.Allow == true
                                   orderby dataComany.Company_Code
                                   select new
                                   {
                                       dataComany.Company_Code
                                   }
                                   ).Distinct();
                    //var a = setDatabase.UserId;
                   //  return Ok(query); //commented on 28-12-2022
                    return Ok(result1);
                }
                else if (Filter == "CDD")
                {
                    var companyUser = await _companyMasterRepository.tblCompanyUsers();
                    var result1 = (from dataComany in result
                                   join userCompany in companyUser
                                   on dataComany.Company_Code.Trim() equals userCompany.CompanyCode.Trim()
                                   where userCompany.User_ID == setDatabase.UserId && userCompany.Allow == true
                                   orderby dataComany.Customer_Req_Date
                                   select new
                                   {
                                       dataComany.Filter_CDD
                                   }
                                   ).Distinct();
                    
                    return Ok(result1);

                   // var query = result.OrderBy(res => res.Customer_Req_Date).Select(res => res.Filter_CDD).Distinct();
                   // return Ok(query);
                }
                else if (Filter == "PDD")
                {
                    var companyUser = await _companyMasterRepository.tblCompanyUsers();
                    var result1 = (from dataComany in result
                                   join userCompany in companyUser
                                   on dataComany.Company_Code.Trim() equals userCompany.CompanyCode.Trim()
                                   where userCompany.User_ID == setDatabase.UserId && userCompany.Allow == true
                                   orderby dataComany.Planning_Commit_Dt
                                   select new
                                   {
                                       dataComany.Filter_PDD
                                   }
                                   ).Distinct();

                    return Ok(result1);
                    // var query = result.OrderBy(res => res.Planning_Commit_Dt).Select(res => res.Filter_PDD).Distinct();
                    // return Ok(query);
                }
                else if (Filter == "Cust")
                {
                    var companyUser = await _companyMasterRepository.tblCompanyUsers();
                    var result1 = (from dataComany in result
                                   join userCompany in companyUser
                                   on dataComany.Company_Code.Trim() equals userCompany.CompanyCode.Trim()
                                   where userCompany.User_ID == setDatabase.UserId && userCompany.Allow == true
                                   orderby dataComany.Customer_Name
                                   select new
                                   {
                                       dataComany.Customer_Name
                                   }
                                   ).Distinct();

                    return Ok(result1);

                    // var query = result.OrderBy(res => res.Customer_Name).Select(res =>   res.Customer_Name ).Distinct();
                    // return Ok(query);
                }
                else if (Filter == "Order")
                {
                    var companyUser = await _companyMasterRepository.tblCompanyUsers();
                    var result1 = (from dataComany in result
                                   join userCompany in companyUser
                                   on dataComany.Company_Code.Trim() equals userCompany.CompanyCode.Trim()
                                   where userCompany.User_ID == setDatabase.UserId && userCompany.Allow == true
                                   orderby dataComany.COrder_Serial_No
                                   select new
                                   {
                                       dataComany.COrder_Serial_No
                                   }
                                   ).Distinct();

                    return Ok(result1);

                    // var query = result.OrderBy(res => res.COrder_Serial_No).Select(res =>   res.COrder_Serial_No).Distinct();
                    // return Ok(query);
                }
                else {
                   // var query = result.Where(res => res.Filter_Stages == "MCH");
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error, Please Later..");
                 
            }
        }

        [HttpGet]
         [Route("Search/{company_Code}/{CDD}/{PDD}/{Stages}/{QtyValue}/{Customer}/{OrderNo}")]      
        public async Task<ActionResult<vw_Backlog_Order_Rev1>> Search_Backlog_Order(string company_Code, string CDD, string PDD, string Stages, string QtyValue,string Customer, string OrderNo)
        {
            try
            {
                var result = await _backLogOrderRepository.Search_Backlog_Order(company_Code, CDD, PDD, Stages,Customer,OrderNo);
                var userData = await _companyMasterRepository.tblCompanyUsers();

                if (result.Any())
                {
                  
                    if (QtyValue == "Qty")
                    {

                        var query = (from data in result
                                     join usser in userData
                                     on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                     where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                     select new
                                     {
                                         Stages = data.Filter_Stages,
                                         Value = data.Filter_Qty
                                     }
                                     );
                        return Ok(query.GroupBy(res=> new { res.Stages }).Select(res => new { Stages = res.Key.Stages,
                            Value = Math.Round(res.Sum(res => (double)res.Value), 0)
                        }));
                        //return Ok(result.GroupBy(res => new { res.Filter_Stages })
                        //               .Select(res => new
                        //               {
                        //                   Stages = res.Key.Filter_Stages,
                        //                   Value = Math.Round(res.Sum(res => (double)res.Filter_Qty), 0)

                        //               }).OrderBy(res => res.Stages));
                    }
                    else 
                    {
                        var query = (from data in result
                                     join usser in userData
                                     on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                     where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                     select new
                                     {
                                         Stages = data.Filter_Stages,
                                         Value = data.Filter_Value
                                     }
                                    );
                        return Ok(query.GroupBy(res => new { res.Stages }).Select(res => new {
                            Stages = res.Key.Stages,
                            Value = Math.Round(res.Sum(res => (double)res.Value), 0)
                        }));

                        //return Ok(result.GroupBy(res => new { res.Filter_Stages })
                        //               .Select(res => new
                        //               {
                        //                   Stages = res.Key.Filter_Stages,
                        //                   Value = Math.Round(res.Sum(res => (double)res.Filter_Value), 0)

                        //               }).OrderBy(res => res.Stages));
                    }

                }
                return NotFound();
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
                
            }
        }
        [HttpGet]
        [Route("SearchSummary/{company_Code}/{CDD}/{PDD}/{Stages}/{QtyValue}/{Customer}/{OrderNo}")]
        public async Task<ActionResult<vw_Backlog_Order_Summary>> Search_BackLog_Summary_CDD(string company_Code, string CDD, string PDD, string Stages, string QtyValue, string Customer, string OrderNo)
        {
            try
            {
                string month = "";
                var result = await _backLogOrderRepository.Search_Backlog_Order_Summary(company_Code,CDD,PDD,Stages, Customer, OrderNo, month);
                var userData = await _companyMasterRepository.tblCompanyUsers();


                if (QtyValue == "Qty")
                {

                    var query = (from data in result
                                 join usser in userData
                                 on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                 where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                 orderby data.CDD_Year, data.CDD_Month_No
                                 select new
                                 {

                                     CDD = data.Filter_CDD,
                                     BLV = data.BallValveQty,
                                     BFV = data.ButterFlyValveQty,
                                     GGC = data.GGCValveQty,
                                     ACT = data.ActuatorQty
                                 }
                                  );

                    return Ok(query.GroupBy(res => new { res.CDD }).Select(res => new
                    {
                        CDD = res.Key.CDD,
                        BLV = Math.Round(res.Sum(res => (double)res.BLV), 0),
                        BFV = Math.Round(res.Sum(res => (double)res.BFV), 0),
                        GGC = Math.Round(res.Sum(res => (double)res.GGC), 0),
                        ACT = Math.Round(res.Sum(res => (double)res.ACT), 0),
                        Total = Math.Round(res.Sum(res => (double)res.BLV), 0) + Math.Round(res.Sum(res => (double)res.BFV), 0) + Math.Round(res.Sum(res => (double)res.GGC), 0) + Math.Round(res.Sum(res => (double)res.ACT), 0)

                    }));

                    //return Ok(result.OrderBy(res => res.CDD_Year).ThenBy(res => res.CDD_Month_No).GroupBy(res => new { res.Filter_CDD }).Select(res => new
                    //{
                    //    CDD = res.Key.Filter_CDD,
                    //    BLV = Math.Round(res.Sum(res => (double)res.BallValveQty), 0),
                    //    BFV = Math.Round(res.Sum(res => (double)res.ButterFlyValveQty), 0),
                    //    GGC = Math.Round(res.Sum(res => (double)res.GGCValveQty), 0),
                    //    ACT = Math.Round(res.Sum(res => (double)res.ActuatorQty), 0),
                    //    Total = Math.Round(res.Sum(res => (double)res.BallValveQty), 0) + Math.Round(res.Sum(res => (double)res.ButterFlyValveQty), 0) + Math.Round(res.Sum(res => (double)res.GGCValveQty), 0) + Math.Round(res.Sum(res => (double)res.ActuatorQty), 0)

                    //})); 
                }
                else
                {
                    var query = (from data in result
                                 join usser in userData
                                 on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                 where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                 orderby data.CDD_Year, data.CDD_Month_No
                                 select new
                                 {
                                      
                                     CDD = data.Filter_CDD,
                                     BLV = data.BallValve,
                                     BFV = data.ButterFlyValve,
                                     GGC = data.GGCValve,
                                     ACT = data.Actuator
                                 }
                                    );

                    return Ok(query.GroupBy(res => new { res.CDD }).Select(res => new
                    {
                        CDD = res.Key.CDD,
                        BLV = Math.Round(res.Sum(res => (double)res.BLV), 0),
                        BFV = Math.Round(res.Sum(res => (double)res.BFV), 0),
                        GGC = Math.Round(res.Sum(res => (double)res.GGC), 0),
                        ACT = Math.Round(res.Sum(res => (double)res.ACT), 0),
                        Total = Math.Round(res.Sum(res => (double)res.BLV), 0) + Math.Round(res.Sum(res => (double)res.BFV), 0) + Math.Round(res.Sum(res => (double)res.GGC), 0) + Math.Round(res.Sum(res => (double)res.ACT), 0)

                    }));

                    //return Ok(result.OrderBy(res => res.CDD_Year).ThenBy(res => res.CDD_Month_No).GroupBy(res => new { res.Filter_CDD }).Select(res => new
                    //{
                    //    CDD = res.Key.Filter_CDD,
                    //    BLV = Math.Round(res.Sum(res => (double)res.BallValve), 0),
                    //    BFV = Math.Round(res.Sum(res => (double)res.ButterFlyValve), 0),
                    //    GGC = Math.Round(res.Sum(res => (double)res.GGCValve), 0),
                    //    ACT = Math.Round(res.Sum(res => (double)res.Actuator), 0),
                    //    Total = Math.Round(res.Sum(res => (double)res.BallValve), 0) + Math.Round(res.Sum(res => (double)res.ButterFlyValve), 0) + Math.Round(res.Sum(res => (double)res.GGCValve), 0) + Math.Round(res.Sum(res => (double)res.Actuator), 0)

                    //})); 
                }

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try Later...");
            }   
        }

        [HttpGet]
        [Route("SearchSummaryPDD/{company_Code}/{CDD}/{PDD}/{Stages}/{QtyValue}/{Customer}/{OrderNo}")]
        public async Task<ActionResult<vw_Backlog_Order_Summary>> Search_BackLog_Summary_PDD(string company_Code, string CDD, string PDD, string Stages, string QtyValue, string Customer, string OrderNo)
        {
            try
            {
                var result = await _backLogOrderRepository.Search_Backlog_Order_Summary(company_Code, CDD, PDD, Stages, Customer, OrderNo,"");
                var userData = await _companyMasterRepository.tblCompanyUsers();

                if (QtyValue == "Qty")
                {
                    var query = (from data in result
                                 join usser in userData
                                 on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                 where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                 orderby data.PDD_Year, data.PDD_Month_No
                                 select new
                                 {

                                     CDD = data.Filter_PDD,
                                     BLV = data.BallValveQty,
                                     BFV = data.ButterFlyValveQty,
                                     GGC = data.GGCValveQty,
                                     ACT = data.ActuatorQty
                                 }
                                    );

                    return Ok(query.GroupBy(res => new { res.CDD }).Select(res => new
                    {
                        CDD = res.Key.CDD,
                        BLV = Math.Round(res.Sum(res => (double)res.BLV), 0),
                        BFV = Math.Round(res.Sum(res => (double)res.BFV), 0),
                        GGC = Math.Round(res.Sum(res => (double)res.GGC), 0),
                        ACT = Math.Round(res.Sum(res => (double)res.ACT), 0),
                        Total = Math.Round(res.Sum(res => (double)res.BLV), 0) + Math.Round(res.Sum(res => (double)res.BFV), 0) + Math.Round(res.Sum(res => (double)res.GGC), 0) + Math.Round(res.Sum(res => (double)res.ACT), 0)

                    }));

                    //return Ok(result.OrderBy(res => res.PDD_Year).ThenBy(res => res.PDD_Month_No).GroupBy(res => new { res.Filter_PDD }).Select(res => new
                    //{
                    //    CDD = res.Key.Filter_PDD,
                    //    BLV = Math.Round(res.Sum(res => (double)res.BallValveQty), 0),
                    //    BFV = Math.Round(res.Sum(res => (double)res.ButterFlyValveQty), 0),
                    //    GGC = Math.Round(res.Sum(res => (double)res.GGCValveQty), 0),
                    //    ACT = Math.Round(res.Sum(res => (double)res.ActuatorQty), 0),
                    //    Total = Math.Round(res.Sum(res => (double)res.BallValveQty), 0) + Math.Round(res.Sum(res => (double)res.ButterFlyValveQty), 0) + Math.Round(res.Sum(res => (double)res.GGCValveQty), 0) + Math.Round(res.Sum(res => (double)res.ActuatorQty), 0)

                    //}));
                }
                else {
                    var query = (from data in result
                                 join usser in userData
                                 on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                 where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                 orderby data.PDD_Year, data.PDD_Month_No
                                 select new
                                 {

                                     CDD = data.Filter_PDD,
                                     BLV = data.BallValve,
                                     BFV = data.ButterFlyValve,
                                     GGC = data.GGCValve,
                                     ACT = data.Actuator
                                 }
                                    );

                    return Ok(query.GroupBy(res => new { res.CDD }).Select(res => new
                    {
                        CDD = res.Key.CDD,
                        BLV = Math.Round(res.Sum(res => (double)res.BLV), 0),
                        BFV = Math.Round(res.Sum(res => (double)res.BFV), 0),
                        GGC = Math.Round(res.Sum(res => (double)res.GGC), 0),
                        ACT = Math.Round(res.Sum(res => (double)res.ACT), 0),
                        Total = Math.Round(res.Sum(res => (double)res.BLV), 0) + Math.Round(res.Sum(res => (double)res.BFV), 0) + Math.Round(res.Sum(res => (double)res.GGC), 0) + Math.Round(res.Sum(res => (double)res.ACT), 0)

                    }));
                    //return Ok(result.OrderBy(res => res.PDD_Year).ThenBy(res => res.PDD_Month_No).GroupBy(res => new { res.Filter_PDD }).Select(res => new
                    //{
                    //    CDD = res.Key.Filter_PDD,
                    //    BLV = Math.Round(res.Sum(res => (double)res.BallValve), 0),
                    //    BFV = Math.Round(res.Sum(res => (double)res.ButterFlyValve), 0),
                    //    GGC = Math.Round(res.Sum(res => (double)res.GGCValve), 0),
                    //    ACT = Math.Round(res.Sum(res => (double)res.Actuator), 0),
                    //    Total = Math.Round(res.Sum(res => (double)res.BallValve), 0) + Math.Round(res.Sum(res => (double)res.ButterFlyValve), 0) + Math.Round(res.Sum(res => (double)res.GGCValve), 0) + Math.Round(res.Sum(res => (double)res.Actuator), 0)

                    //}));
                }

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try Later...");
            }
        }

        [HttpGet]
        [Route("SearchSummaryDrilled/{company_Code}/{CDD}/{PDD}/{Stages}/{QtyValue}/{Customer}/{OrderNo}/{Year}/{CDDFilter}")]
        public async Task<ActionResult<vw_Backlog_Order_Summary>> Search_BackLog_Summary_Drilled(string company_Code, string CDD, string PDD, string Stages, string QtyValue, string Customer, string OrderNo,int Year, string CDDFilter)
        {
            try
          {
                var result = await _backLogOrderRepository.Search_Backlog_Order_Summary(company_Code, CDD, PDD, Stages,Customer,OrderNo, CDDFilter);
                var userData = await _companyMasterRepository.tblCompanyUsers();

                if (QtyValue == "Qty")
                {
                    if (CDDFilter == "Delay")
                    {

                        var query = (from data in result
                                     join usser in userData
                                     on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                     where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                     orderby data.CDD_Year
                                     select new
                                     {

                                         CDD = data.CDD_Year,
                                         BLV = data.BallValveQty,
                                         BFV = data.ButterFlyValveQty,
                                         GGC = data.GGCValveQty,
                                         ACT = data.ActuatorQty
                                     }
                                    );

                        return Ok(query.GroupBy(res => new { res.CDD }).Select(res => new
                        {
                            CDD = res.Key.CDD,
                            BLV = Math.Round(res.Sum(res => (double)res.BLV), 0),
                            BFV = Math.Round(res.Sum(res => (double)res.BFV), 0),
                            GGC = Math.Round(res.Sum(res => (double)res.GGC), 0),
                            ACT = Math.Round(res.Sum(res => (double)res.ACT), 0),
                            Total = Math.Round(res.Sum(res => (double)res.BLV), 0) + Math.Round(res.Sum(res => (double)res.BFV), 0) + Math.Round(res.Sum(res => (double)res.GGC), 0) + Math.Round(res.Sum(res => (double)res.ACT), 0)

                        }));

                        //return Ok(result.OrderBy(res => res.CDD_Year).ThenBy(res => res.CDD_Month_No).GroupBy(res => new { res.CDD_Year }).Select(res => new
                        //{
                        //    CDD = res.Key.CDD_Year,
                        //    BLV = Math.Round(res.Sum(res => (double)res.BallValveQty), 0),
                        //    BFV = Math.Round(res.Sum(res => (double)res.ButterFlyValveQty), 0),
                        //    GGC = Math.Round(res.Sum(res => (double)res.GGCValveQty), 0),
                        //    ACT = Math.Round(res.Sum(res => (double)res.ActuatorQty), 0),
                        //    Total = Math.Round(res.Sum(res => (double)res.BallValveQty), 0) + Math.Round(res.Sum(res => (double)res.ButterFlyValveQty), 0) + Math.Round(res.Sum(res => (double)res.GGCValveQty), 0) + Math.Round(res.Sum(res => (double)res.ActuatorQty), 0)

                        //}));
                    }
                    else
                    {

                        var query = (from data in result
                                     join usser in userData
                                     on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                     where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                     orderby data.CDD_Year, data.CDD_Month_No
                                     select new
                                     {

                                         CDD = data.CDD_Year,
                                         CDD_Month = data.CDD_Month,
                                         BLV = data.BallValveQty,
                                         BFV = data.ButterFlyValveQty,
                                         GGC = data.GGCValveQty,
                                         ACT = data.ActuatorQty
                                     }
                                    );

                        return Ok(query.GroupBy(res => new { res.CDD_Month, res.CDD }).Select(res => new
                        {
                            CDD = res.Key.CDD_Month + " " + res.Key.CDD,
                            BLV = Math.Round(res.Sum(res => (double)res.BLV), 0),
                            BFV = Math.Round(res.Sum(res => (double)res.BFV), 0),
                            GGC = Math.Round(res.Sum(res => (double)res.GGC), 0),
                            ACT = Math.Round(res.Sum(res => (double)res.ACT), 0),
                            Total = Math.Round(res.Sum(res => (double)res.BLV), 0) + Math.Round(res.Sum(res => (double)res.BFV), 0) + Math.Round(res.Sum(res => (double)res.GGC), 0) + Math.Round(res.Sum(res => (double)res.ACT), 0)

                        }));

                        //return Ok(result.OrderBy(res => res.CDD_Year).ThenBy(res => res.CDD_Month_No).GroupBy(res => new { res.CDD_Month, res.CDD_Year }).Select(res => new
                        //{
                        //    CDD = res.Key.CDD_Month + " " + res.Key.CDD_Year,
                        //    BLV = Math.Round(res.Sum(res => (double)res.BallValveQty), 0),
                        //    BFV = Math.Round(res.Sum(res => (double)res.ButterFlyValveQty), 0),
                        //    GGC = Math.Round(res.Sum(res => (double)res.GGCValveQty), 0),
                        //    ACT = Math.Round(res.Sum(res => (double)res.ActuatorQty), 0),
                        //    Total = Math.Round(res.Sum(res => (double)res.BallValveQty), 0) + Math.Round(res.Sum(res => (double)res.ButterFlyValveQty), 0) + Math.Round(res.Sum(res => (double)res.GGCValveQty), 0) + Math.Round(res.Sum(res => (double)res.ActuatorQty), 0)

                        //}));
                    }
                }
                else
                {
                    if (CDDFilter == "Delay")
                    {

                        var query = (from data in result
                                     join usser in userData
                                     on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                     where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                     orderby data.CDD_Year
                                     select new
                                     {

                                         CDD = data.CDD_Year,
                                         BLV = data.BallValve,
                                         BFV = data.ButterFlyValve,
                                         GGC = data.GGCValve,
                                         ACT = data.Actuator
                                     }
                                    );

                        return Ok(query.GroupBy(res => new { res.CDD }).Select(res => new
                        {
                            CDD = res.Key.CDD,
                            BLV = Math.Round(res.Sum(res => (double)res.BLV), 0),
                            BFV = Math.Round(res.Sum(res => (double)res.BFV), 0),
                            GGC = Math.Round(res.Sum(res => (double)res.GGC), 0),
                            ACT = Math.Round(res.Sum(res => (double)res.ACT), 0),
                            Total = Math.Round(res.Sum(res => (double)res.BLV), 0) + Math.Round(res.Sum(res => (double)res.BFV), 0) + Math.Round(res.Sum(res => (double)res.GGC), 0) + Math.Round(res.Sum(res => (double)res.ACT), 0)

                        }));

                        //return Ok(result.OrderBy(res => res.CDD_Year).ThenBy(res => res.CDD_Month_No).GroupBy(res => new { res.CDD_Year }).Select(res => new
                        //{
                        //    CDD = res.Key.CDD_Year,
                        //    BLV = Math.Round(res.Sum(res => (double)res.BallValve), 0),
                        //    BFV = Math.Round(res.Sum(res => (double)res.ButterFlyValve), 0),
                        //    GGC = Math.Round(res.Sum(res => (double)res.GGCValve), 0),
                        //    ACT = Math.Round(res.Sum(res => (double)res.Actuator), 0),
                        //    Total = Math.Round(res.Sum(res => (double)res.BallValve), 0) + Math.Round(res.Sum(res => (double)res.ButterFlyValve), 0) + Math.Round(res.Sum(res => (double)res.GGCValve), 0) + Math.Round(res.Sum(res => (double)res.Actuator), 0)

                        //}));
                    }
                    else {

                        var query = (from data in result
                                     join usser in userData
                                     on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                     where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                     orderby data.CDD_Year, data.CDD_Month_No
                                     select new
                                     {

                                         CDD = data.CDD_Year,
                                         CDD_Month = data.CDD_Month,
                                         BLV = data.BallValve,
                                         BFV = data.ButterFlyValve,
                                         GGC = data.GGCValve,
                                         ACT = data.Actuator
                                     }
                                   );

                        return Ok(query.GroupBy(res => new { res.CDD_Month, res.CDD }).Select(res => new
                        {
                            CDD = res.Key.CDD_Month + " " + res.Key.CDD,
                            BLV = Math.Round(res.Sum(res => (double)res.BLV), 0),
                            BFV = Math.Round(res.Sum(res => (double)res.BFV), 0),
                            GGC = Math.Round(res.Sum(res => (double)res.GGC), 0),
                            ACT = Math.Round(res.Sum(res => (double)res.ACT), 0),
                            Total = Math.Round(res.Sum(res => (double)res.BLV), 0) + Math.Round(res.Sum(res => (double)res.BFV), 0) + Math.Round(res.Sum(res => (double)res.GGC), 0) + Math.Round(res.Sum(res => (double)res.ACT), 0)

                        }));

                        //return Ok(result.OrderBy(res => res.CDD_Year).ThenBy(res => res.CDD_Month_No).GroupBy(res => new { res.CDD_Month,res.CDD_Year }).Select(res => new
                        //{
                        //    CDD = res.Key.CDD_Month + " " + res.Key.CDD_Year,
                        //    BLV = Math.Round(res.Sum(res => (double)res.BallValve), 0),
                        //    BFV = Math.Round(res.Sum(res => (double)res.ButterFlyValve), 0),
                        //    GGC = Math.Round(res.Sum(res => (double)res.GGCValve), 0),
                        //    ACT = Math.Round(res.Sum(res => (double)res.Actuator), 0),
                        //    Total = Math.Round(res.Sum(res => (double)res.BallValve), 0) + Math.Round(res.Sum(res => (double)res.ButterFlyValve), 0) + Math.Round(res.Sum(res => (double)res.GGCValve), 0) + Math.Round(res.Sum(res => (double)res.Actuator), 0)

                        //}));

                    }
                }

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try Later...");
            }
        }

        [HttpGet]
        [Route("Search_BackLog_Summary_NextDrilled/{company_Code}/{CDD}/{PDD}/{Stages}/{QtyValue}/{Customer}/{OrderNo}/{CDD_Year}")]
        public async Task<ActionResult<vw_Backlog_Order_Summary>> Search_BackLog_Summary_NextDrilled(string company_Code, string CDD, string PDD, string Stages, string QtyValue, string Customer, string OrderNo, int CDD_Year)
        {
            try
            {
                if (CDD_Year > 1950)
                {
                    var result = await _backLogOrderRepository.Search_Backlog_Order_Drilled_Summary(company_Code, CDD, PDD, Stages, Customer, OrderNo, CDD_Year);
                    var userData = await _companyMasterRepository.tblCompanyUsers();

                    if (QtyValue == "Qty")
                    {

                        var query = (from data in result
                                     join usser in userData
                                     on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                     where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                     orderby data.CDD_Year,data.CDD_Month_No
                                     select new
                                     {

                                         CDD = data.CDD_Year,
                                         CDD_Month = data.CDD_Month,
                                         BLV = data.BallValveQty,
                                         BFV = data.ButterFlyValveQty,
                                         GGC = data.GGCValveQty,
                                         ACT = data.ActuatorQty
                                     }
                                   );

                        return Ok(query.GroupBy(res => new { res.CDD_Month, res.CDD }).Select(res => new
                        {
                            CDD = res.Key.CDD_Month + " " + res.Key.CDD,
                            BLV = Math.Round(res.Sum(res => (double)res.BLV), 0),
                            BFV = Math.Round(res.Sum(res => (double)res.BFV), 0),
                            GGC = Math.Round(res.Sum(res => (double)res.GGC), 0),
                            ACT = Math.Round(res.Sum(res => (double)res.ACT), 0),
                            Total = Math.Round(res.Sum(res => (double)res.BLV), 0) + Math.Round(res.Sum(res => (double)res.BFV), 0) + Math.Round(res.Sum(res => (double)res.GGC), 0) + Math.Round(res.Sum(res => (double)res.ACT), 0)

                        }));

                        //return Ok(result.OrderBy(res => res.CDD_Year).ThenBy(res => res.CDD_Month_No).GroupBy(res => new { res.CDD_Month, res.CDD_Year }).Select(res => new
                        //{
                        //    CDD = res.Key.CDD_Month + " " + res.Key.CDD_Year,
                        //    BLV = Math.Round(res.Sum(res => (double)res.BallValveQty), 0),
                        //    BFV = Math.Round(res.Sum(res => (double)res.ButterFlyValveQty), 0),
                        //    GGC = Math.Round(res.Sum(res => (double)res.GGCValveQty), 0),
                        //    ACT = Math.Round(res.Sum(res => (double)res.ActuatorQty), 0),
                        //    Total = Math.Round(res.Sum(res => (double)res.BallValveQty), 0) + Math.Round(res.Sum(res => (double)res.ButterFlyValveQty), 0) + Math.Round(res.Sum(res => (double)res.GGCValveQty), 0) + Math.Round(res.Sum(res => (double)res.ActuatorQty), 0)

                        //}));

                    }
                    else
                    {
                        var query = (from data in result
                                     join usser in userData
                                     on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                     where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                     orderby data.CDD_Year, data.CDD_Month_No
                                     select new
                                     {

                                         CDD = data.CDD_Year,
                                         CDD_Month = data.CDD_Month,
                                         BLV = data.BallValve,
                                         BFV = data.ButterFlyValve,
                                         GGC = data.GGCValve,
                                         ACT = data.Actuator
                                     }
                                 );

                        return Ok(query.GroupBy(res => new { res.CDD_Month, res.CDD }).Select(res => new
                        {
                            CDD = res.Key.CDD_Month + " " + res.Key.CDD,
                            BLV = Math.Round(res.Sum(res => (double)res.BLV), 0),
                            BFV = Math.Round(res.Sum(res => (double)res.BFV), 0),
                            GGC = Math.Round(res.Sum(res => (double)res.GGC), 0),
                            ACT = Math.Round(res.Sum(res => (double)res.ACT), 0),
                            Total = Math.Round(res.Sum(res => (double)res.BLV), 0) + Math.Round(res.Sum(res => (double)res.BFV), 0) + Math.Round(res.Sum(res => (double)res.GGC), 0) + Math.Round(res.Sum(res => (double)res.ACT), 0)

                        }));

                        //return Ok(result.OrderBy(res => res.CDD_Year).ThenBy(res => res.CDD_Month_No).GroupBy(res => new { res.CDD_Month, res.CDD_Year }).Select(res => new
                        //{
                        //    CDD = res.Key.CDD_Month + " " + res.Key.CDD_Year,
                        //    BLV = Math.Round(res.Sum(res => (double)res.BallValve), 0),
                        //    BFV = Math.Round(res.Sum(res => (double)res.ButterFlyValve), 0),
                        //    GGC = Math.Round(res.Sum(res => (double)res.GGCValve), 0),
                        //    ACT = Math.Round(res.Sum(res => (double)res.Actuator), 0),
                        //    Total = Math.Round(res.Sum(res => (double)res.BallValve), 0) + Math.Round(res.Sum(res => (double)res.ButterFlyValve), 0) + Math.Round(res.Sum(res => (double)res.GGCValve), 0) + Math.Round(res.Sum(res => (double)res.Actuator), 0)

                        //}));


                    }
                }
                return NotFound();

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try Later...");
            }
        }

        [HttpGet]
        [Route("SearchTechSummary/{company_Code}/{CDD}/{PDD}/{Stages}/{QtyValue}/{Customer}/{OrderNo}/{CDDFilter}")]
        public async Task<ActionResult<vw_BacklogOrder_Tech_Spec_Summary>> Search_BacklogOrder_Tech_Spec_Summary(string company_Code, string CDD, string PDD, string Stages, string QtyValue, string Customer, string OrderNo, string CDDFilter)
        {
            try
            {
                var result = await _backLogOrderRepository.Search_BacklogOrder_Tech_Spec_Summary(company_Code, CDD, PDD, Stages, Customer, OrderNo, CDDFilter);
                var userData = await _companyMasterRepository.tblCompanyUsers();

                if (QtyValue == "Qty")
                {

                    var query = (from data in result
                                 join usser in userData
                                 on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                 where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                 orderby data.ProdGrp_MIS_Id
                                 select new
                                 {

                                     Product = data.ProdGrp_MIS_Name,
                                     Qty = data.Filter_Qty,
                                     Value = data.Filter_Qty
                                  
                                 }
                                 );

                    return Ok(query.GroupBy(res => new { res.Product }).Select(res => new
                    {
                        Product = res.Key.Product,
                        Qty = Math.Round(res.Sum(res => (double)res.Qty), 0),
                        Value = Math.Round(res.Sum(res => (double)res.Value), 0)

                    }));

                    //return Ok(result.OrderBy(res => res.ProdGrp_MIS_Id).GroupBy(res => new { res.ProdGrp_MIS_Name }).Select(res => new
                    //{
                    //    Product = res.Key.ProdGrp_MIS_Name,
                    //    Qty = Math.Round(res.Sum(res => (double)res.Filter_Qty), 0),
                    //    Value = Math.Round(res.Sum(res => (double)res.Filter_Qty), 0)

                    //}));
                }
                else 
                {
                    var query = (from data in result
                                 join usser in userData
                                 on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                 where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                 orderby data.ProdGrp_MIS_Id
                                 select new
                                 {

                                     Product = data.ProdGrp_MIS_Name,
                                     Qty = data.Filter_Qty,
                                     Value = data.Filter_Value

                                 }
                                );

                    return Ok(query.GroupBy(res => new { res.Product }).Select(res => new
                    {
                        Product = res.Key.Product,
                        Qty = Math.Round(res.Sum(res => (double)res.Qty), 0),
                        Value = Math.Round(res.Sum(res => (double)res.Value), 0)

                    }));

                    //return Ok(result.OrderBy(res => res.ProdGrp_MIS_Id).GroupBy(res => new { res.ProdGrp_MIS_Name }).Select(res => new
                    //{
                    //    Product = res.Key.ProdGrp_MIS_Name,
                    //    Qty = Math.Round(res.Sum(res => (double)res.Filter_Qty), 0),
                    //    Value = Math.Round(res.Sum(res => (double)res.Filter_Value), 0)

                    //}));
                }

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try Later...");
            }
        }

        [HttpGet]
        [Route("SearchTechSummaryDrilled/{company_Code}/{CDD}/{PDD}/{Stages}/{QtyValue}/{Customer}/{OrderNo}/{CDD_Year}/{MonthYear}")]
        public async Task<ActionResult<vw_BacklogOrder_Tech_Spec_Summary>> Search_BacklogOrder_Tech_Spec_SummaryDrilled(string company_Code, string CDD, string PDD, string Stages, string QtyValue, string Customer, string OrderNo, int CDD_Year,string MonthYear)
        {
            try
            {
                 
                    var result = await _backLogOrderRepository.Search_BacklogOrder_Tech_Spec_Summary(company_Code, CDD, PDD, Stages, Customer, OrderNo, CDD_Year, MonthYear);
                    var userData = await _companyMasterRepository.tblCompanyUsers();

                if (QtyValue == "Qty")
                    {
                    var query = (from data in result
                                 join usser in userData
                                 on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                 where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                 orderby data.ProdGrp_MIS_Id
                                 select new
                                 {

                                     Product = data.ProdGrp_MIS_Name,
                                     Qty = data.Filter_Qty,
                                     Value = data.Filter_Qty

                                 }
                                );

                    return Ok(query.GroupBy(res => new { res.Product }).Select(res => new
                    {
                        Product = res.Key.Product,
                        Qty = Math.Round(res.Sum(res => (double)res.Qty), 0),
                        Value = Math.Round(res.Sum(res => (double)res.Qty), 0)

                    }));

                    //return Ok(result.OrderBy(res => res.ProdGrp_MIS_Id).GroupBy(res => new { res.ProdGrp_MIS_Name }).Select(res => new
                    //{
                    //    Product = res.Key.ProdGrp_MIS_Name,
                    //    Qty = Math.Round(res.Sum(res => (double)res.Filter_Qty), 0),
                    //    Value = Math.Round(res.Sum(res => (double)res.Filter_Qty), 0)

                    //}));
                }
                    else
                    {
                    var query = (from data in result
                                 join usser in userData
                                 on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                                 where usser.User_ID == setDatabase.UserId && usser.Allow == true
                                 orderby data.ProdGrp_MIS_Id
                                 select new
                                 {

                                     Product = data.ProdGrp_MIS_Name,
                                     Qty = data.Filter_Qty,
                                     Value = data.Filter_Value

                                 }
                                );

                    return Ok(query.GroupBy(res => new { res.Product }).Select(res => new
                    {
                        Product = res.Key.Product,
                        Qty = Math.Round(res.Sum(res => (double)res.Qty), 0),
                        Value = Math.Round(res.Sum(res => (double)res.Value), 0)

                    }));

                    //return Ok(result.OrderBy(res => res.ProdGrp_MIS_Id).GroupBy(res => new { res.ProdGrp_MIS_Name }).Select(res => new
                    //{
                    //    Product = res.Key.ProdGrp_MIS_Name,
                    //    Qty = Math.Round(res.Sum(res => (double)res.Filter_Qty), 0),
                    //    Value = Math.Round(res.Sum(res => (double)res.Filter_Value), 0)

                    //}));
                }
                

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try Later...");
            }
        }
        //Search_BacklogOrder_Tech_Spec_Grid
        [HttpGet]
        [Route("Search_BacklogOrder_Tech_Spec_Grid/{company_Code}/{CDD}/{PDD}/{Stages}/{QtyValue}/{Customer}/{OrderNo}/{TechProduct}")]
        public async Task<ActionResult<vw_BacklogOrder_Tech_Spec_Summary>> Search_BacklogOrder_Tech_Spec_Grid(string company_Code, string CDD, string PDD, string Stages, string QtyValue, string Customer, string OrderNo, string TechProduct)
        {
            try
            {
                var result = await _backLogOrderRepository.Search_BacklogOrder_Tech_Spec_Grid(company_Code, CDD, PDD, Stages, Customer, OrderNo,TechProduct);

                var userData = await _companyMasterRepository.tblCompanyUsers();

                int id = 1;


                var query = (from data in result
                             join usser in userData
                             on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                             where usser.User_ID == setDatabase.UserId && usser.Allow == true
                             orderby data.COrder_Serial_No

                             select new
                             {
                                 Id = id++,
                                 company_Code = data.Company_Code,
                                 customer_Code = data.Customer_Code,
                                 customer_Name = data.Customer_Name,
                                 corder_Serial_No = data.COrder_Serial_No,
                                 Order_Year = data.Order_Year,
                                 Order_Type = data.Order_Type,
                                 Order_Booking_Completed_Date = data.Order_Booking_Completed_Date,
                                 major_No = data.Major_No,
                                 minor_No = data.Minor_No,
                                 Product = data.ProdGrp_MIS_Name,
                                 Qty = data.Filter_Qty,
                                 Value = data.Filter_Value
                             }
                             );

                return Ok(query);

                //return Ok(result.OrderBy(res => res.COrder_Serial_No)
                //        //.GroupBy(res => new { res.ProdGrp_MIS_Name })
                //        .Select(res => new
                //    {
                //            Id= id++,
                //            company_Code = res.Company_Code,
                //            customer_Code = res.Customer_Code,
                //            customer_Name = res.Customer_Name,
                //            corder_Serial_No = res.COrder_Serial_No,
                //            Order_Year = res.Order_Year,
                //            Order_Type = res.Order_Type,
                //            Order_Booking_Completed_Date = res.Order_Booking_Completed_Date,
                //            major_No = res.Major_No,
                //            minor_No = res.Minor_No,
                //            Product = res.ProdGrp_MIS_Name,
                //            Qty = res.Filter_Qty,
                //            Value = res.Filter_Value

                //    }));


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try Later...");
            }
        }

        [HttpGet]
        [Route("SearchTechSummaryGrid/{company_Code}/{CDD}/{PDD}/{Stages}/{QtyValue}/{Customer}/{OrderNo}/{CDDFilter}/{CDDYear}/{CDDYearMonth}")]
        public async Task<ActionResult<vw_BacklogOrder_Tech_Spec_Summary>> Search_BacklogOrder_Tech_Spec_Summary_Grid(string company_Code, string CDD, string PDD, string Stages, string QtyValue, string Customer, string OrderNo, string CDDFilter,int CDDYear,string CDDYearMonth)
        {
            try
            {
                var result = await _backLogOrderRepository.Search_BacklogOrder_Tech_Spec_Summary(company_Code, CDD, PDD, Stages, Customer, OrderNo, CDDFilter,CDDYear,CDDYearMonth);

                var userData = await _companyMasterRepository.tblCompanyUsers();

                int id = 1;


                var query = (from data in result
                             join usser in userData
                             on data.Company_Code.Trim() equals usser.CompanyCode.Trim()
                             where usser.User_ID == setDatabase.UserId && usser.Allow == true
                             orderby data.COrder_Serial_No
                             
                             select new
                             {
                                 Id = id++,
                                 company_Code = data.Company_Code,
                                 customer_Code = data.Customer_Code,
                                 customer_Name = data.Customer_Name,
                                 corder_Serial_No = data.COrder_Serial_No,
                                 Order_Year = data.Order_Year,
                                 Order_Type = data.Order_Type,
                                 Order_Booking_Completed_Date = data.Order_Booking_Completed_Date,
                                 major_No = data.Major_No,
                                 minor_No = data.Minor_No,
                                 Product = data.ProdGrp_MIS_Name,
                                 Qty = data.Filter_Qty,
                                 Value = data.Filter_Value
                             }
                             );

                return Ok(query);

                //return Ok(result.OrderBy(res => res.COrder_Serial_No)
                //        //.GroupBy(res => new { res.ProdGrp_MIS_Name })
                //        .Select(res => new
                //    {
                //            Id= id++,
                //            company_Code = res.Company_Code,
                //            customer_Code = res.Customer_Code,
                //            customer_Name = res.Customer_Name,
                //            corder_Serial_No = res.COrder_Serial_No,
                //            Order_Year = res.Order_Year,
                //            Order_Type = res.Order_Type,
                //            Order_Booking_Completed_Date = res.Order_Booking_Completed_Date,
                //            major_No = res.Major_No,
                //            minor_No = res.Minor_No,
                //            Product = res.ProdGrp_MIS_Name,
                //            Qty = res.Filter_Qty,
                //            Value = res.Filter_Value

                //    }));
                 

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try Later...");
            }
        }
    }
}
