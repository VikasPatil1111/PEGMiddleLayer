using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEGMiddleLayer.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Controllers.Dashboard
{
    [Authorize(Roles = "SuperAdmin,BacklogDashboard,BookingDashboard,Admin,BacklogInvoice,AccountReceivable")]
    [Route("api/[controller]")]
    [ApiController]
    public class PendingOrderController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;

        public PendingOrderController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet]
        [Route("PendingOrders")]
        public async Task<ActionResult<PendingOrder>> PendingOrders()
        {
            try
            {
                var result = await _dashboardRepository.GetPendingOrders();
                //List<PendingOrder> pendingOrder = new List<PendingOrder>();
                //var result1 = result.Select(data => data.Company_Code);
                return Ok(result.Select(data=>data.Company_Code.Trim()).Distinct()); //(IEnumerable<PendingOrder>)result1;
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("OrderQtyDetails/{CompanyList}")]
        public async Task<ActionResult<PendingOrder>> GetPendingOrderQty(string CompanyList) {
            try
            {
                if (CompanyList == "All")
                {
                    var result = await _dashboardRepository.GetPendingOrders();
                    //var query1 = result.Where(item => arr.Contains(item.Company_Code.Trim())).ToList();
                    //var query3 = (from item in result
                    //where arr.Contains(item.Company_Code.Trim())
                    //select item).ToList();
                    var query = result.GroupBy(x => true).Select(x => new
                    {
                        PendingForInvoices =  x.Sum(y => y.Fg_Val_MtcIssue) + x.Sum(y => y.Fg_Val_MtcNotIssue) + x.Sum(y => y.Order_Val_Clear_For_Prod) + x.Sum(y => y.MCH_Total_Val), // x.Sum(y => y.Pending_For_Invoice_Val),
                        FinishedGoods = Math.Round( (double)x.Sum(y => y.Fg_Val_MtcIssue),0),
                        ProducedbutnotReadytoShip = Math.Round((double)x.Sum(y => y.Fg_Val_MtcNotIssue),0),
                        ClearedOrderSchProductio = Math.Round((double)x.Sum(y => y.Order_Val_Clear_For_Prod),0),
                        MCH = Math.Round((double)x.Sum(y => y.MCH_Total_Val),0),
                        OrderQty = Math.Round((double)x.Sum(y => y.Order_Qty),0)
                    }).ToList();

                    return Ok(query);
                }
                else
                {
                    string[] arr = CompanyList.Split(',');
                    if (arr.Length > 0)
                    {
                        var result = await _dashboardRepository.GetPendingOrders();
                        var query1 = result.Where(item => arr.Contains(item.Company_Code.Trim())).ToList();
                        //var query3 = (from item in result
                        //             where arr.Contains(item.Company_Code.Trim())
                        //            select item).ToList();
                        var query = result.Where(item => arr.Contains(item.Company_Code.Trim())).GroupBy(x => true).Select(x => new
                        {
                            PendingForInvoices =  x.Sum(y => y.Fg_Val_MtcIssue) + x.Sum(y => y.Fg_Val_MtcNotIssue) + x.Sum(y => y.Order_Val_Clear_For_Prod) + x.Sum(y => y.MCH_Total_Val), // x.Sum(y => y.Pending_For_Invoice_Val),
                            FinishedGoods = Math.Round((double)x.Sum(y => y.Fg_Val_MtcIssue),0),
                            ProducedbutnotReadytoShip = Math.Round((double)x.Sum(y => y.Fg_Val_MtcNotIssue),0),
                            ClearedOrderSchProductio = Math.Round((double)x.Sum(y => y.Order_Val_Clear_For_Prod),0),
                            MCH = Math.Round((double)x.Sum(y => y.MCH_Total_Val),0),
                            OrderQty = x.Sum(y => y.Order_Qty)

                        }).ToList();

                        return Ok(query);
                    }

                    else
                    {
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return NotFound("Error In Getting Records");
            }
        }

        [HttpGet]
        [Route("productWisePendingOrders")]
        public async Task<ActionResult<ProductWisePendingOrder>> productWisePendingOrders() {
            try
            {   
                var result = await _dashboardRepository.productWisePendingOrders();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound("Error");
            }
        }

        [HttpGet]
        [Route("PendingOrderDetails/{Company}")]
        public async Task<ActionResult<PendingOrder>> getPendingOrderDetails(string Company) 
        {   
            try
            {
                string[] company = Company.Split(',');
                if (company[0] == "All")
                {
                    var result = await _dashboardRepository.GetPendingOrders();
                    Int16 RowId = 1;
                    return Ok(result.Select(
                        result => new
                        {
                            Id = RowId++,
                            Order_Serial_No = result.COrder_Serial_No,
                            result.Order_Date,
                            result.Customer_Code,
                            result.Customer_Name,
                            result.Customer_Po_No,
                            result.Customer_Po_Date,
                            result.Branch_Name,
                            result.Major_No,
                            result.Minor_No,
                            result.Order_Qty,
                            PendingForInvoice = result.Pending_For_Invoice_Val,
                            FinishGoods = result.Fg_Qty_MtcIssue,
                            ProducedNotReadyShipment = result.Fg_Qty_MtcNotIssue,
                            ClearedOrderSchForProd = result.Order_Val_Clear_For_Prod,
                            MCH = result.MCH_Total_Val,
                            CDD = result.Customer_Req_Date
                        }
                        ));
                }
                else
                {

                    var result = await _dashboardRepository.GetPendingOrders();
                    Int16 RowId = 1;
                    return Ok(result.Where(result => company.Contains(result.Company_Code.Trim())).Select(
                        result => new
                        {
                            Id = RowId++,
                            Order_Serial_No =  result.COrder_Serial_No,
                            result.Order_Date,
                            result.Customer_Code,
                            result.Customer_Name,
                            result.Customer_Po_No,
                            result.Customer_Po_Date,
                            result.Branch_Name,
                            result.Major_No,
                            result.Minor_No,
                            result.Order_Qty,
                            PendingForInvoice = result.Pending_For_Invoice_Val,
                            FinishGoods = result.Fg_Qty_MtcIssue,
                            ProducedNotReadyShipment = result.Fg_Qty_MtcNotIssue,
                            ClearedOrderSchForProd = result.Order_Val_Clear_For_Prod,
                            MCH = result.MCH_Total_Val,
                            CDD = result.Customer_Req_Date
                        }
                        ));
                }
            }
            catch (Exception ex)
            {
                return NotFound("Error");
            }
        }
        [HttpGet]
        [Route("pendingOrderDetailsBarCharts/{ComapnyName}")]
        public async Task<ActionResult<PendingOrderDetailsBarChart>> pendingOrderDetailsBarCharts(string ComapnyName)
        {
            try
            {
                var result = await _dashboardRepository.pendingOrderDetailsBarCharts();
                string[] company = ComapnyName.Split(',');

                int addMonth1 = DateTime.Now.Month;
                int addMonth = DateTime.Now.AddMonths(6).Month;
                int index = 1;
                if (company.Length > 0 && company[0].ToString() != "All")
                {
                    if (addMonth1 > 6)
                    {
                        var resBefore = result
                            .Where(res => (((res.Month < System.DateTime.Now.Month
                            && res.Year == DateTime.Now.Year) || res.Year < DateTime.Now.Year)
                            && company.Contains(res.Company_Code.Trim())
                            ))
                            .GroupBy(g => true)
                            // .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName })
                            // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                            .Select(g => new
                            {
                                // MonthName = g.Key.MonthName, 
                                // Month = g.Key.Month,
                                // Year = g.Key.Year,
                                MonthYear = "Delay",//g.MonthName + "-" + g.Year.ToString().Substring(2, 2),
                                BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                                BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                                ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                                GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                                Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                                // Total = Math.Round(g.ButterFlyValve, 2) + Math.Round(g.BallValve, 2) + Math.Round(g.Actuator, 2) + Math.Round(g.GGCValve, 2),
                                Id = index++
                            }).ToList();

                        var resw = result.OrderBy(res => res.Year).ThenBy(res => res.Month)
                            .Where(res => (((res.Month >= System.DateTime.Now.Month
                            && res.Year == DateTime.Now.Year) || (res.Month <= DateTime.Now.AddMonths(6).Month
                            && res.Year == DateTime.Now.AddYears(1).Year))
                            && company.Contains(res.Company_Code.Trim())
                            ))
                            // .GroupBy(res => new { res.MonthName, res.Month, res.Year })
                            .GroupBy(g => new { g.MonthName, g.Year })
                            // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                            .Select(g => new
                            {
                                // MonthName = g.Key.MonthName, 
                                // Month = g.Key.Month,
                                // Year = g.Key.Year,
                                MonthYear = g.Key.MonthName + "-" + g.Key.Year.ToString().Substring(2, 2),
                                //BFV = Math.Round(g.ButterFlyValve, 0),
                                BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                                //BLV = Math.Round(g.BallValve, 0),
                                BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                                // ACT = Math.Round(g.Actuator, 0),
                                ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                                // GGC = Math.Round(g.GGCValve, 0),
                                GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                                Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                                Id = index++
                            });
                        var resAfter = result
                           .Where(res => (res.Month > System.DateTime.Now.AddMonths(6).Month &&
                           res.Year >= DateTime.Now.AddYears(1).Year
                           && company.Contains(res.Company_Code.Trim())
                           ))
                           .GroupBy(g => true)
                           // .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName })
                           // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                           .Select(g => new
                           {
                               // MonthName = g.Key.MonthName, 
                               // Month = g.Key.Month,
                               // Year = g.Key.Year,
                               MonthYear = "After",//g.MonthName + "-" + g.Year.ToString().Substring(2, 2),
                               BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                               BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                               ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                               GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                               Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                               // Total = Math.Round(g.ButterFlyValve, 2) + Math.Round(g.BallValve, 2) + Math.Round(g.Actuator, 2) + Math.Round(g.GGCValve, 2),
                               Id = index++
                           }).ToList();
                        var merge = resBefore.Union(resw).Union(resAfter);
                        return Ok(merge);
                    }
                    else
                    {
                        var resw = result.OrderBy(res => res.Year).ThenBy(res => res.Month)
                            .Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month
                            && res.Year == DateTime.Now.Year
                            && company.Contains(res.Company_Code.Trim())
                            )
                             .GroupBy(res => new { res.MonthName, res.Year })
                            // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                            .Select(g => new
                            {
                                // MonthName = g.Key.MonthName, 
                                // Month = g.Key.Month,
                                // Year = g.Key.Year,
                                MonthYear = g.Key.MonthName + "-" + g.Key.Year.ToString().Substring(2, 2),
                                //BFV = Math.Round(g.ButterFlyValve, 0),
                                BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                                //BLV = Math.Round(g.BallValve, 0),
                                BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                                // ACT = Math.Round(g.Actuator, 0),
                                ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                                // GGC = Math.Round(g.GGCValve, 0),
                                GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                                Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                                Id = index++
                            });
                        return Ok(resw);
                    }
                }

                if (addMonth1 > 6)
                {
                    var resBefore = result
                        .Where(res => (((res.Month < System.DateTime.Now.Month && res.Year == DateTime.Now.Year)) || res.Year < DateTime.Now.Year))
                        .GroupBy(g => true)

                        // .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName })
                        // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                        .Select(g => new
                        {
                            // MonthName = g.Key.MonthName, 
                            // Month = g.Key.Month,
                            // Year = g.Key.Year,
                            MonthYear = "Delay",//g.MonthName + "-" + g.Year.ToString().Substring(2, 2),
                            BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                            BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                            ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                            GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            // Total = Math.Round(g.ButterFlyValve, 2) + Math.Round(g.BallValve, 2) + Math.Round(g.Actuator, 2) + Math.Round(g.GGCValve, 2),
                            Id = index++
                        }).ToList();

                    var resw = result.OrderBy(res => res.Year).ThenBy(res => res.Month)
                        .Where(res => ((res.Month >= System.DateTime.Now.Month && res.Year == DateTime.Now.Year) || (res.Month <= DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.AddYears(1).Year)))
                        // .GroupBy(res => new { res.MonthName, res.Month, res.Year })
                        .GroupBy(g => new { g.MonthName, g.Year })
                        // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                        .Select(g => new
                        {
                            // MonthName = g.Key.MonthName, 
                            // Month = g.Key.Month,
                            // Year = g.Key.Year,
                            MonthYear = g.Key.MonthName + "-" + g.Key.Year.ToString().Substring(2, 2),
                            //BFV = Math.Round(g.ButterFlyValve, 0),
                            BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                            //BLV = Math.Round(g.BallValve, 0),
                            BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                            // ACT = Math.Round(g.Actuator, 0),
                            ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                            // GGC = Math.Round(g.GGCValve, 0),
                            GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Id = index++
                        });
                    var resAfter = result
                       .Where(res => (res.Month > System.DateTime.Now.AddMonths(6).Month && res.Year >= DateTime.Now.AddYears(1).Year))
                       .GroupBy(g => true)
                       // .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName })
                       // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                       .Select(g => new
                       {
                           // MonthName = g.Key.MonthName, 
                           // Month = g.Key.Month,
                           // Year = g.Key.Year,
                           MonthYear = "After",//g.MonthName + "-" + g.Year.ToString().Substring(2, 2),
                           BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                           BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                           ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                           GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                           Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                           // Total = Math.Round(g.ButterFlyValve, 2) + Math.Round(g.BallValve, 2) + Math.Round(g.Actuator, 2) + Math.Round(g.GGCValve, 2),
                           Id = index++
                       }).ToList();
                    var merge = resBefore.Union(resw).Union(resAfter);
                    return Ok(merge);
                }
                else
                {
                    var resw = result.OrderBy(res => res.Year).ThenBy(res => res.Month)
                        .Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.Year)
                         .GroupBy(res => new { res.MonthName, res.Year })
                        // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                        .Select(g => new
                        {
                            // MonthName = g.Key.MonthName, 
                            // Month = g.Key.Month,
                            // Year = g.Key.Year,
                            MonthYear = g.Key.MonthName + "-" + g.Key.Year.ToString().Substring(2, 2),
                            //BFV = Math.Round(g.ButterFlyValve, 0),
                            BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                            //BLV = Math.Round(g.BallValve, 0),
                            BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                            // ACT = Math.Round(g.Actuator, 0),
                            ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                            // GGC = Math.Round(g.GGCValve, 0),
                            GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Id = index++
                        });
                    return Ok(resw);
                }

                //  .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName });


            }
            catch (Exception ex)
            {
                return NotFound("Error");
            }
        }


        [HttpGet]
        [Route("pendingOrderDetailsBarChartsDelay")]
        public async Task<ActionResult<PendingOrderDetailsBarChart>> pendingOrderDetailsBarChartsDelay()
        {
            try
            {
                var result = await _dashboardRepository.pendingOrderDetailsBarCharts();


                int addMonth1 = DateTime.Now.Month;
                int addMonth = DateTime.Now.AddMonths(6).Month;
                int index = 1;


                if (addMonth1 > 6)
                {
                   

                    var resw = result.OrderBy(res => res.Year).ThenBy(res => res.Month)
                       .Where(res => (((res.Month < System.DateTime.Now.Month && res.Year == DateTime.Now.Year)) || res.Year < DateTime.Now.Year))
                        // .GroupBy(res => new { res.MonthName, res.Month, res.Year })
                        .GroupBy(g => new { g.Year })
                        // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                        .Select(g => new
                        {
                            // MonthName = g.Key.MonthName, 
                            // Month = g.Key.Month,
                            // Year = g.Key.Year,
                            MonthYear =  g.Key.Year.ToString(),//.Substring(2, 2),
                            //BFV = Math.Round(g.ButterFlyValve, 0),
                            BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                            //BLV = Math.Round(g.BallValve, 0),
                            BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                            // ACT = Math.Round(g.Actuator, 0),
                            ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                            // GGC = Math.Round(g.GGCValve, 0),
                            GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Id = index++
                        });
                    
                   // var merge = resBefore.Union(resw).Union(resAfter);
                    return Ok(resw);
                }
                else
                {
                    var resw = result.OrderBy(res => res.Year).ThenBy(res => res.Month)
                        //.Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.Year)
                        .Where(res => (((res.Month < System.DateTime.Now.Month && res.Year == DateTime.Now.Year)) || res.Year < DateTime.Now.Year))
                         .GroupBy(res => new { res.MonthName, res.Year })
                        // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                        .Select(g => new
                        {
                            // MonthName = g.Key.MonthName, 
                            // Month = g.Key.Month,
                            // Year = g.Key.Year,
                            MonthYear = g.Key.MonthName + "-" + g.Key.Year.ToString().Substring(2, 2),
                            //BFV = Math.Round(g.ButterFlyValve, 0),
                            BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                            //BLV = Math.Round(g.BallValve, 0),
                            BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                            // ACT = Math.Round(g.Actuator, 0),
                            ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                            // GGC = Math.Round(g.GGCValve, 0),
                            GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Id = index++
                        });
                    return Ok(resw);
                }

                //  .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName });


            }
            catch (Exception ex)
            {
                return NotFound("Error");
            }
        }

        [HttpGet]
        [Route("pendingOrderDetailsBarChartsDelay/{Year}")]
        public async Task<ActionResult<PendingOrderDetailsBarChart>> pendingOrderDetailsBarChartsDelay(int Year)
        {
            try
            {
                var result = await _dashboardRepository.pendingOrderDetailsBarCharts();


                int addMonth1 = DateTime.Now.Month;
                int addMonth = DateTime.Now.AddMonths(6).Month;
                int index = 1;


                 


                    var resw = result.OrderBy(res => res.Year).ThenBy(res => res.Month)
                       .Where(res =>  res.Year == Year)
                        // .GroupBy(res => new { res.MonthName, res.Month, res.Year })
                        .GroupBy(g => new { g.MonthName, g.Year })
                        // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                        .Select(g => new
                        {
                            // MonthName = g.Key.MonthName, 
                            // Month = g.Key.Month,
                            // Year = g.Key.Year,
                            // MonthYear = g.Key.Year.ToString(),//.Substring(2, 2),
                            MonthYear = g.Key.MonthName + "-" + g.Key.Year.ToString().Substring(2, 2),
                            //BFV = Math.Round(g.ButterFlyValve, 0),
                            BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                            //BLV = Math.Round(g.BallValve, 0),
                            BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                            // ACT = Math.Round(g.Actuator, 0),
                            ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                            // GGC = Math.Round(g.GGCValve, 0),
                            GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Id = index++
                        });

                    // var merge = resBefore.Union(resw).Union(resAfter);
                    return Ok(resw);
                

                //  .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName });


            }
            catch (Exception ex)
            {
                return NotFound("Error");
            }
        }
        [HttpGet]
        [Route("pendingOrderDetailsBarCharts")]
        public async Task<ActionResult<PendingOrderDetailsBarChart>> pendingOrderDetailsBarCharts()
        {
            try
            {
                var result = await _dashboardRepository.pendingOrderDetailsBarCharts();
                

                int addMonth1 = DateTime.Now.Month;
                int addMonth = DateTime.Now.AddMonths(6).Month;
                int index = 1;
              
                
                if (addMonth1 > 6)
                {
                    var resBefore = result                       
                        .Where(res => (((res.Month < System.DateTime.Now.Month && res.Year == DateTime.Now.Year)) || res.Year < DateTime.Now.Year))
                        .GroupBy(g => true)

                        // .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName })
                        // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                        .Select(g => new
                        {
                            // MonthName = g.Key.MonthName, 
                            // Month = g.Key.Month,
                            // Year = g.Key.Year,
                            MonthYear = "Delay",//g.MonthName + "-" + g.Year.ToString().Substring(2, 2),
                            BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                            BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                            ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                            GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0)+ Math.Round(g.Sum(bv => bv.BallValve), 0)+ Math.Round(g.Sum(bv => bv.Actuator), 0)+ Math.Round(g.Sum(bv => bv.GGCValve), 0),
                           // Total = Math.Round(g.ButterFlyValve, 2) + Math.Round(g.BallValve, 2) + Math.Round(g.Actuator, 2) + Math.Round(g.GGCValve, 2),
                            Id = index++
                        }).ToList();

                    var resw = result.OrderBy(res => res.Year).ThenBy(res => res.Month)
                        .Where(res => ((res.Month >= System.DateTime.Now.Month && res.Year == DateTime.Now.Year) || ( res.Month <= DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.AddYears(1).Year) ))
                        // .GroupBy(res => new { res.MonthName, res.Month, res.Year })
                        .GroupBy(g => new { g.MonthName, g.Year })
                        // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                        .Select(g => new
                        {
                            // MonthName = g.Key.MonthName, 
                            // Month = g.Key.Month,
                            // Year = g.Key.Year,
                            MonthYear = g.Key.MonthName + "-" + g.Key.Year.ToString().Substring(2, 2),
                            //BFV = Math.Round(g.ButterFlyValve, 0),
                            BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                            //BLV = Math.Round(g.BallValve, 0),
                            BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                            // ACT = Math.Round(g.Actuator, 0),
                            ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                            // GGC = Math.Round(g.GGCValve, 0),
                            GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Id = index++
                        });
                    var resAfter = result
                       .Where(res => (res.Month > System.DateTime.Now.AddMonths(6).Month && res.Year >= DateTime.Now.AddYears(1).Year))
                       .GroupBy(g => true)
                       // .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName })
                       // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                       .Select(g => new
                       {
                            // MonthName = g.Key.MonthName, 
                            // Month = g.Key.Month,
                            // Year = g.Key.Year,
                            MonthYear = "After",//g.MonthName + "-" + g.Year.ToString().Substring(2, 2),
                            BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                           BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                           ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                           GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                           Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            // Total = Math.Round(g.ButterFlyValve, 2) + Math.Round(g.BallValve, 2) + Math.Round(g.Actuator, 2) + Math.Round(g.GGCValve, 2),
                            Id = index++
                       }).ToList();
                    var merge = resBefore.Union(resw).Union(resAfter);
                    return Ok(merge);
                }
                else
                {
                    var resw = result.OrderBy(res => res.Year).ThenBy(res => res.Month)
                        .Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.Year)
                         .GroupBy(res => new { res.MonthName,  res.Year  })
                        // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                        .Select(g => new
                        {
                            // MonthName = g.Key.MonthName, 
                            // Month = g.Key.Month,
                            // Year = g.Key.Year,
                            MonthYear = g.Key.MonthName + "-" + g.Key.Year.ToString().Substring(2, 2),
                            //BFV = Math.Round(g.ButterFlyValve, 0),
                            BFV = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0),
                            //BLV = Math.Round(g.BallValve, 0),
                            BLV = Math.Round(g.Sum(bv => bv.BallValve), 0),
                            // ACT = Math.Round(g.Actuator, 0),
                            ACT = Math.Round(g.Sum(bv => bv.Actuator), 0),
                            // GGC = Math.Round(g.GGCValve, 0),
                            GGC = Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Total = Math.Round(g.Sum(bv => bv.ButterFlyValve), 0) + Math.Round(g.Sum(bv => bv.BallValve), 0) + Math.Round(g.Sum(bv => bv.Actuator), 0) + Math.Round(g.Sum(bv => bv.GGCValve), 0),
                            Id = index++
                        });
                    return Ok(resw);
                }
               
                  //  .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName });
                    
                    
            }
            catch (Exception ex)
            {
                return NotFound("Error");
            }
        }

        [HttpGet]
        [Route("pendingOrderQtyDetailsBarCharts")]
        public async Task<ActionResult<PendingOrderQtyDetailsBarChart>> pendingOrderQtyDetailsBarCharts()
        {
            try
            {
                var result = await _dashboardRepository.pendingOrderQtyDetailsBarCharts();
                int index = 1;
                int addMonth1 = DateTime.Now.Month;
                int addMonth = DateTime.Now.AddMonths(6).Month;
                //int index = 1;
                if (addMonth1 > 6)
                {
                    var resBefore = result
                      .Where(res => ((res.Month < System.DateTime.Now.Month
                            && res.Year == DateTime.Now.Year) || res.Year < DateTime.Now.Year))
                      .GroupBy(g => true)
                      // .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName })
                      // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                      .Select(g => new
                      {
                            // MonthName = g.Key.MonthName, 
                            // Month = g.Key.Month,
                            // Year = g.Key.Year,
                            MonthYear = "Delay",//g.MonthName + "-" + g.Year.ToString().Substring(2, 2),
                            BFV = g.Sum(bv => bv.ButterFlyValve),
                            BLV = g.Sum(bv => bv.BallValve),
                            ACT = g.Sum(bv => bv.Actuator),
                            GGC = g.Sum(bv => bv.GGCValve),
                            Total = g.Sum(bv => bv.ButterFlyValve) + g.Sum(bv => bv.BallValve)+ g.Sum(bv => bv.Actuator)+ g.Sum(bv => bv.GGCValve),
                            // Total = Math.Round(g.ButterFlyValve, 2) + Math.Round(g.BallValve, 2) + Math.Round(g.Actuator, 2) + Math.Round(g.GGCValve, 2),
                            Id = index++
                      }).ToList();

                    var Query = result.OrderBy(res => res.Year).ThenBy(res => res.Month).
                         // Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.Year)
                         Where(res => ((res.Month >= System.DateTime.Now.Month && res.Year == DateTime.Now.Year) || (res.Month <= DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.AddYears(1).Year)))
                         .GroupBy(res => new { res.MonthName,res.Year })
                          .Select(res => new
                          {
                              MonthYear = res.Key.MonthName + "-" + res.Key.Year.ToString().Substring(2, 2),
                              BFV =  res.Sum(r => r.ButterFlyValve),
                              //BLV = res.BallValve,
                              BLV = res.Sum(r => r.BallValve),
                              //ACT = res.Actuator,
                              ACT = res.Sum(r => r.Actuator),
                              // GGC = res.GGCValve,
                              GGC = res.Sum(r => r.GGCValve),
                              Total = res.Sum(r => r.ButterFlyValve) + res.Sum(r => r.BallValve) + res.Sum(r => r.Actuator) + res.Sum(r => r.GGCValve),
                              Id = index++
                          });
                    var resAfter = result
                       .Where(res => (res.Month > System.DateTime.Now.AddMonths(6).Month && res.Year >= DateTime.Now.AddYears(1).Year))
                       .GroupBy(g => true)
                       // .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName })
                       // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                       .Select(g => new
                       {
                           // MonthName = g.Key.MonthName, 
                           // Month = g.Key.Month,
                           // Year = g.Key.Year,
                           MonthYear = "After",//g.MonthName + "-" + g.Year.ToString().Substring(2, 2),
                           BFV = g.Sum(bv => bv.ButterFlyValve),
                           BLV = g.Sum(bv => bv.BallValve),
                           ACT = g.Sum(bv => bv.Actuator),
                           GGC = g.Sum(bv => bv.GGCValve),
                           Total = g.Sum(bv => bv.ButterFlyValve)+ g.Sum(bv => bv.BallValve)+ g.Sum(bv => bv.Actuator)+ g.Sum(bv => bv.GGCValve),
                           // Total = Math.Round(g.ButterFlyValve, 2) + Math.Round(g.BallValve, 2) + Math.Round(g.Actuator, 2) + Math.Round(g.GGCValve, 2),
                           Id = index++
                       }).ToList();
                    return Ok(resBefore.Union(Query).Union(resAfter));
                }
                else
                {
                    var Query = result.OrderBy(res => res.Year).ThenBy(res => res.Month).
                           // Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.Year)
                           Where(res => res.Month >= System.DateTime.Now.Month && res.Year == DateTime.Now.Year) 
                           .GroupBy(res => new { res.MonthName,res.Year })
                            .Select(res => new
                            {
                                MonthYear = res.Key.MonthName + "-" + res.Key.Year.ToString().Substring(2, 2),
                                BFV = res.Sum(r => r.ButterFlyValve),
                                //BLV = res.BallValve,
                                BLV = res.Sum(r => r.BallValve),
                                //ACT = res.Actuator,
                                ACT = res.Sum(r => r.Actuator),
                                // GGC = res.GGCValve,
                                GGC = res.Sum(r => r.GGCValve),
                                Total = res.Sum(r => r.ButterFlyValve) + res.Sum(r => r.BallValve) + res.Sum(r => r.Actuator) + res.Sum(r => r.GGCValve),
                                Id = index++
                                //MonthYear = res.MonthName + "-" + res.Year.ToString().Substring(2, 2),
                                //ButterFlyValve = res.ButterFlyValve,
                                //BallValve = res.BallValve,
                                //Actuator = res.Actuator,
                                //GGCValve = res.GGCValve,
                                //Total = res.ButterFlyValve + res.BallValve + res.Actuator + res.GGCValve,
                                //Id = index++
                            });
                    return Ok(Query);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Error");
            }
            
        }

        [HttpGet]
        [Route("pendingOrderQtyDetailsBarChartsDelay")]
        public async Task<ActionResult<PendingOrderQtyDetailsBarChart>> pendingOrderQtyDetailsBarChartsDelay()
        {
            try
            {
                var result = await _dashboardRepository.pendingOrderQtyDetailsBarCharts();
                int index = 1;
                int addMonth1 = DateTime.Now.Month;
                int addMonth = DateTime.Now.AddMonths(6).Month;
                //int index = 1;
                if (addMonth1 > 6)
                {                   

                       var Query = result.OrderBy(res => res.Year).ThenBy(res => res.Month).
                         // Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.Year)
                         // Where(res => ((res.Month >= System.DateTime.Now.Month && res.Year == DateTime.Now.Year) || (res.Month <= DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.AddYears(1).Year)))
                        Where(res => ((res.Month < System.DateTime.Now.Month
                            && res.Year == DateTime.Now.Year) || res.Year < DateTime.Now.Year))
                         .GroupBy(res => new {  res.Year })
                          .Select(res => new
                          {
                              MonthYear =   res.Key.Year.ToString(),//.Substring(2, 2),
                              BFV = res.Sum(r => r.ButterFlyValve),
                              //BLV = res.BallValve,
                              BLV = res.Sum(r => r.BallValve),
                              //ACT = res.Actuator,
                              ACT = res.Sum(r => r.Actuator),
                              // GGC = res.GGCValve,
                              GGC = res.Sum(r => r.GGCValve),
                              Total = res.Sum(r => r.ButterFlyValve) + res.Sum(r => r.BallValve) + res.Sum(r => r.Actuator) + res.Sum(r => r.GGCValve),
                              Id = index++
                          });
                  
                    return Ok(Query);
                }
                else
                {
                    var Query = result.OrderBy(res => res.Year).ThenBy(res => res.Month).
                           // Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.Year)
                           Where(res => res.Month >= System.DateTime.Now.Month && res.Year == DateTime.Now.Year)
                           .GroupBy(res => new { res.MonthName, res.Year })
                            .Select(res => new
                            {
                                MonthYear = res.Key.MonthName + "-" + res.Key.Year.ToString().Substring(2, 2),
                                BFV = res.Sum(r => r.ButterFlyValve),
                                //BLV = res.BallValve,
                                BLV = res.Sum(r => r.BallValve),
                                //ACT = res.Actuator,
                                ACT = res.Sum(r => r.Actuator),
                                // GGC = res.GGCValve,
                                GGC = res.Sum(r => r.GGCValve),
                                Total = res.Sum(r => r.ButterFlyValve) + res.Sum(r => r.BallValve) + res.Sum(r => r.Actuator) + res.Sum(r => r.GGCValve),
                                Id = index++
                                //MonthYear = res.MonthName + "-" + res.Year.ToString().Substring(2, 2),
                                //ButterFlyValve = res.ButterFlyValve,
                                //BallValve = res.BallValve,
                                //Actuator = res.Actuator,
                                //GGCValve = res.GGCValve,
                                //Total = res.ButterFlyValve + res.BallValve + res.Actuator + res.GGCValve,
                                //Id = index++
                            });
                    return Ok(Query);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Error");
            }

        }

        [HttpGet]
        [Route("pendingOrderQtyDetailsBarChartsDelay/{Year}")]
        public async Task<ActionResult<PendingOrderQtyDetailsBarChart>> pendingOrderQtyDetailsBarChartsDelay(int Year)
        {
            try
            {
                var result = await _dashboardRepository.pendingOrderQtyDetailsBarCharts();
                int index = 1;     
                    var Query = result.OrderBy(res => res.Year).ThenBy(res => res.Month).
                     // Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.Year)
                     // Where(res => ((res.Month >= System.DateTime.Now.Month && res.Year == DateTime.Now.Year) || (res.Month <= DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.AddYears(1).Year)))
                     Where(res => ( res.Year == Year))
                      .GroupBy(res => new { res.MonthName, res.Year })
                       .Select(res => new
                       {
                           MonthYear = res.Key.MonthName + "-" + res.Key.Year.ToString().Substring(2, 2),
                           BFV = res.Sum(r => r.ButterFlyValve),
                              //BLV = res.BallValve,
                              BLV = res.Sum(r => r.BallValve),
                              //ACT = res.Actuator,
                              ACT = res.Sum(r => r.Actuator),
                              // GGC = res.GGCValve,
                              GGC = res.Sum(r => r.GGCValve),
                           Total = res.Sum(r => r.ButterFlyValve) + res.Sum(r => r.BallValve) + res.Sum(r => r.Actuator) + res.Sum(r => r.GGCValve),
                           Id = index++
                       });

                    return Ok(Query);
                
            }
            catch (Exception ex)
            {
                return NotFound("Error");
            }

        }

        [HttpGet]
        [Route("pendingOrderQtyDetailsBarCharts/{CompanyName}")]
        public async Task<ActionResult<PendingOrderQtyDetailsBarChart>> pendingOrderQtyDetailsBarCharts(string CompanyName)
        {
            try
            {
                string[] Comapny = CompanyName.Split(",");
              
                var result = await _dashboardRepository.pendingOrderQtyDetailsBarCharts();
                int index = 1;
                int addMonth1 = DateTime.Now.Month;
                int addMonth = DateTime.Now.AddMonths(6).Month;
                if (Comapny.Length > 0)
                {
                    if (addMonth1 > 6)
                    {
                        var resBefore = result
                          .Where(res => (((res.Month < System.DateTime.Now.Month
                            && res.Year == DateTime.Now.Year) || res.Year < DateTime.Now.Year) && Comapny.Contains(res.Company_Code.Trim())))
                          .GroupBy(g => true)
                          // .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName })
                          // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                          .Select(g => new
                          {
                          // MonthName = g.Key.MonthName, 
                          // Month = g.Key.Month,
                          // Year = g.Key.Year,
                          MonthYear = "Delay",//g.MonthName + "-" + g.Year.ToString().Substring(2, 2),
                          BFV = g.Sum(bv => bv.ButterFlyValve),
                              BLV = g.Sum(bv => bv.BallValve),
                              ACT = g.Sum(bv => bv.Actuator),
                              GGC = g.Sum(bv => bv.GGCValve),
                              Total = g.Sum(bv => bv.ButterFlyValve) + g.Sum(bv => bv.BallValve) + g.Sum(bv => bv.Actuator) + g.Sum(bv => bv.GGCValve),
                          // Total = Math.Round(g.ButterFlyValve, 2) + Math.Round(g.BallValve, 2) + Math.Round(g.Actuator, 2) + Math.Round(g.GGCValve, 2),
                          Id = index++
                          }).ToList();

                        var Query = result.OrderBy(res => res.Year).ThenBy(res => res.Month).
                             // Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.Year)
                             Where(res => (((res.Month >= System.DateTime.Now.Month && res.Year == DateTime.Now.Year) || 
                                            (res.Month <= DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.AddYears(1).Year))
                                            && Comapny.Contains(res.Company_Code.Trim())
                                            ))
                             .GroupBy(res => new { res.MonthName, res.Year })
                              .Select(res => new
                              {
                                  MonthYear = res.Key.MonthName + "-" + res.Key.Year.ToString().Substring(2, 2),
                                  BFV = res.Sum(r => r.ButterFlyValve),
                              //BLV = res.BallValve,
                              BLV = res.Sum(r => r.BallValve),
                              //ACT = res.Actuator,
                              ACT = res.Sum(r => r.Actuator),
                              // GGC = res.GGCValve,
                              GGC = res.Sum(r => r.GGCValve),
                                  Total = res.Sum(r => r.ButterFlyValve) + res.Sum(r => r.BallValve) + res.Sum(r => r.Actuator) + res.Sum(r => r.GGCValve),
                                  Id = index++
                              });
                        var resAfter = result
                           .Where(res => (res.Month > System.DateTime.Now.AddMonths(6).Month 
                                          && res.Year >= DateTime.Now.AddYears(1).Year
                                          && Comapny.Contains(res.Company_Code.Trim())
                                          ))
                           .GroupBy(g => true)
                           // .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName })
                           // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                           .Select(g => new
                           {
                           // MonthName = g.Key.MonthName, 
                           // Month = g.Key.Month,
                           // Year = g.Key.Year,
                           MonthYear = "After",//g.MonthName + "-" + g.Year.ToString().Substring(2, 2),
                           BFV = g.Sum(bv => bv.ButterFlyValve),
                               BLV = g.Sum(bv => bv.BallValve),
                               ACT = g.Sum(bv => bv.Actuator),
                               GGC = g.Sum(bv => bv.GGCValve),
                               Total = g.Sum(bv => bv.ButterFlyValve) + g.Sum(bv => bv.BallValve) + g.Sum(bv => bv.Actuator) + g.Sum(bv => bv.GGCValve),
                           // Total = Math.Round(g.ButterFlyValve, 2) + Math.Round(g.BallValve, 2) + Math.Round(g.Actuator, 2) + Math.Round(g.GGCValve, 2),
                           Id = index++
                           }).ToList();
                        return Ok(resBefore.Union(Query).Union(resAfter));
                    }
                    else
                    {
                        var Query = result.OrderBy(res => res.Year).ThenBy(res => res.Month).
                               // Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.Year)
                               Where(res => res.Month >= System.DateTime.Now.Month && res.Year == DateTime.Now.Year && Comapny.Contains(res.Company_Code.Trim()))
                               .GroupBy(res => new { res.MonthName, res.Year })
                                .Select(res => new
                                {
                                    MonthYear = res.Key.MonthName + "-" + res.Key.Year.ToString().Substring(2, 2),
                                    BFV = res.Sum(r => r.ButterFlyValve),
                                //BLV = res.BallValve,
                                BLV = res.Sum(r => r.BallValve),
                                //ACT = res.Actuator,
                                ACT = res.Sum(r => r.Actuator),
                                // GGC = res.GGCValve,
                                GGC = res.Sum(r => r.GGCValve),
                                    Total = res.Sum(r => r.ButterFlyValve) + res.Sum(r => r.BallValve) + res.Sum(r => r.Actuator) + res.Sum(r => r.GGCValve),
                                    Id = index++
                                //MonthYear = res.MonthName + "-" + res.Year.ToString().Substring(2, 2),
                                //ButterFlyValve = res.ButterFlyValve,
                                //BallValve = res.BallValve,
                                //Actuator = res.Actuator,
                                //GGCValve = res.GGCValve,
                                //Total = res.ButterFlyValve + res.BallValve + res.Actuator + res.GGCValve,
                                //Id = index++
                            });
                        return Ok(Query);
                    }

                }
                   
                if (addMonth1 > 6)
                {
                    var resBefore = result
                      .Where(res => (((res.Month < System.DateTime.Now.Month
                            && res.Year == DateTime.Now.Year) || res.Year < DateTime.Now.Year)
                      && Comapny.Contains(res.Company_Code.Trim())))
                      .GroupBy(g => true)
                      // .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName })
                      // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                      .Select(g => new
                      {
                          // MonthName = g.Key.MonthName, 
                          // Month = g.Key.Month,
                          // Year = g.Key.Year,
                          MonthYear = "Delay",//g.MonthName + "-" + g.Year.ToString().Substring(2, 2),
                          BFV = g.Sum(bv => bv.ButterFlyValve),
                          BLV = g.Sum(bv => bv.BallValve),
                          ACT = g.Sum(bv => bv.Actuator),
                          GGC = g.Sum(bv => bv.GGCValve),
                          Total = g.Sum(bv => bv.ButterFlyValve) + g.Sum(bv => bv.BallValve) + g.Sum(bv => bv.Actuator) + g.Sum(bv => bv.GGCValve),
                          // Total = Math.Round(g.ButterFlyValve, 2) + Math.Round(g.BallValve, 2) + Math.Round(g.Actuator, 2) + Math.Round(g.GGCValve, 2),
                          Id = index++
                      }).ToList();

                    var Query = result.OrderBy(res => res.Year).ThenBy(res => res.Month).
                         // Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.Year)
                         Where(res => (((res.Month >= System.DateTime.Now.Month && res.Year == DateTime.Now.Year) 
                                        || (res.Month <= DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.AddYears(1).Year))
                                            && Comapny.Contains(res.Company_Code.Trim())
                                        ))
                         .GroupBy(res => new { res.MonthName, res.Year })
                          .Select(res => new
                          {
                              MonthYear = res.Key.MonthName + "-" + res.Key.Year.ToString().Substring(2, 2),
                              BFV = res.Sum(r => r.ButterFlyValve),
                              //BLV = res.BallValve,
                              BLV = res.Sum(r => r.BallValve),
                              //ACT = res.Actuator,
                              ACT = res.Sum(r => r.Actuator),
                              // GGC = res.GGCValve,
                              GGC = res.Sum(r => r.GGCValve),
                              Total = res.Sum(r => r.ButterFlyValve) + res.Sum(r => r.BallValve) + res.Sum(r => r.Actuator) + res.Sum(r => r.GGCValve),
                              Id = index++
                          });
                    var resAfter = result
                       .Where(res => (res.Month > System.DateTime.Now.AddMonths(6).Month && res.Year >= DateTime.Now.AddYears(1).Year && Comapny.Contains(res.Company_Code.Trim())))
                       .GroupBy(g => true)
                       // .GroupBy(res => new { res.MonthName, res.Month, res.Year, res.ProductName })
                       // .OrderBy(gg => new {  gg.Key.Year, gg.Key.Month })
                       .Select(g => new
                       {
                           // MonthName = g.Key.MonthName, 
                           // Month = g.Key.Month,
                           // Year = g.Key.Year,
                           MonthYear = "After",//g.MonthName + "-" + g.Year.ToString().Substring(2, 2),
                           BFV = g.Sum(bv => bv.ButterFlyValve),
                           BLV = g.Sum(bv => bv.BallValve),
                           ACT = g.Sum(bv => bv.Actuator),
                           GGC = g.Sum(bv => bv.GGCValve),
                           Total = g.Sum(bv => bv.ButterFlyValve) + g.Sum(bv => bv.BallValve) + g.Sum(bv => bv.Actuator) + g.Sum(bv => bv.GGCValve),
                           // Total = Math.Round(g.ButterFlyValve, 2) + Math.Round(g.BallValve, 2) + Math.Round(g.Actuator, 2) + Math.Round(g.GGCValve, 2),
                           Id = index++
                       }).ToList();
                    return Ok(resBefore.Union(Query).Union(resAfter));
                }
                else
                {
                    var Query = result.OrderBy(res => res.Year).ThenBy(res => res.Month).
                           // Where(res => res.Month >= System.DateTime.Now.AddMonths(6).Month && res.Year == DateTime.Now.Year)
                           Where(res => res.Month >= System.DateTime.Now.Month && res.Year == DateTime.Now.Year)
                           .GroupBy(res => new { res.MonthName, res.Year })
                            .Select(res => new
                            {
                                MonthYear = res.Key.MonthName + "-" + res.Key.Year.ToString().Substring(2, 2),
                                BFV = res.Sum(r => r.ButterFlyValve),
                                //BLV = res.BallValve,
                                BLV = res.Sum(r => r.BallValve),
                                //ACT = res.Actuator,
                                ACT = res.Sum(r => r.Actuator),
                                // GGC = res.GGCValve,
                                GGC = res.Sum(r => r.GGCValve),
                                Total = res.Sum(r => r.ButterFlyValve) + res.Sum(r => r.BallValve) + res.Sum(r => r.Actuator) + res.Sum(r => r.GGCValve),
                                Id = index++
                                //MonthYear = res.MonthName + "-" + res.Year.ToString().Substring(2, 2),
                                //ButterFlyValve = res.ButterFlyValve,
                                //BallValve = res.BallValve,
                                //Actuator = res.Actuator,
                                //GGCValve = res.GGCValve,
                                //Total = res.ButterFlyValve + res.BallValve + res.Actuator + res.GGCValve,
                                //Id = index++
                            });
                    return Ok(Query);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Error");
            }

        }

        [HttpGet]
        [Route("GetRefreshedDate")]
        public async Task<ActionResult> GetRefreshedDate() {
            try {

                var result = await _dashboardRepository.GetPendingOrders();

                return Ok(result.GroupBy(r => true)
                    .Select(res =>  res.Max(r => r.ETL_On))
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        
        }

        [HttpGet]
        [Route("GetPendingOrderTechDetails/{Year}/{Product_Code}/{Month?}")]
        public async Task<ActionResult<PendingOrder_Tech_Dtl>> GetPendingOrderTechDetails(int Year,int Product_Code,int? Month)
        {
            try
            {
                var result = await _dashboardRepository.GetPendingOrder_Tech_Dtls();
                if (Year == 0) {
                    var Query = result.Where(res => res.Product_Code == Product_Code)
                        .GroupBy(res => new { res.Company_Code,res.Order_Year,res.ProdGrp_MIS_Id,res.ProdGrp_MIS_Name })
                                       .Select(res => new {
                                           Company_Code = res.Key.Company_Code,
                                           Order_Year = res.Key.Order_Year,
                                           ProdGrp_MIS_Id = res.Key.ProdGrp_MIS_Id,
                                           ProdGrp_MIS_Name = res.Key.ProdGrp_MIS_Name,
                                           Order_Value = Math.Round( (double) res.Sum(res=>  res.Order_Value) ,0),
                                           Order_Qty = res.Sum(res=> res.Order_Qty)
                                       }).OrderBy(res => res.ProdGrp_MIS_Id);
                    return Ok(Query);

                }
                else
                {
                    var Query = result.Where(res => res.Order_Year == Year && res.Product_Code == Product_Code)
                         .GroupBy(res => new { res.Company_Code, res.Order_Year, res.ProdGrp_MIS_Id, res.ProdGrp_MIS_Name })
                                        .Select(res => new {
                                            Company_Code = res.Key.Company_Code,
                                            Order_Year = res.Key.Order_Year,
                                            ProdGrp_MIS_Id = res.Key.ProdGrp_MIS_Id,
                                            ProdGrp_MIS_Name = res.Key.ProdGrp_MIS_Name,
                                            Order_Value = Math.Round((double)res.Sum(res => res.Order_Value), 0),
                                            Order_Qty = res.Sum(res => res.Order_Qty)
                                        }).OrderBy(res => res.ProdGrp_MIS_Id);
                    return Ok(Query);
                }
               // return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try later..");
            }
        }
        [HttpGet]
        [Route("GetPendingOrderTechGrid/{Product_Code}/{Year}")]
        public async Task<ActionResult<PendingOrder_Tech_Dtl>> GetPendingOrderTechGrid(int Product_Code, int Year)
        {
            try
            {
                var result = await _dashboardRepository.GetPendingOrder_Tech_Dtls();

                int Id = 1;
                if (Year == 0)
                {
                    var query = result.Where(res => res.Product_Code == Product_Code
                                           )
                           .GroupBy(res => new
                           {
                               res.Order_Year,
                               res.Company_Code,
                               res.Product,
                               res.Month,
                               res.MonthName,
                               res.ProdGrp_MIS_Id,
                               res.ProdGrp_MIS_Name,
                               res.COrder_Serial_No,
                               res.Customer_Code,
                               res.Customer_Name,
                               res.Major_No,
                               res.Minor_No
                           })
                           .Select(res => new
                           {
                               Company_Code = res.Key.Company_Code, //
                               Year = res.Key.Order_Year.ToString(),//
                               Order_No = res.Key.COrder_Serial_No,
                               Customer_Code = res.Key.Customer_Code,
                               Customer_Name = res.Key.Customer_Name,
                               Major_No = res.Key.Major_No,
                               Minor_No = res.Key.Minor_No,
                               Product = res.Key.Product,
                               Month = res.Key.Month,//
                               Month_Name = res.Key.MonthName,//
                               ProdGrp_MIS_Id = res.Key.ProdGrp_MIS_Id,//
                               ProdGrp_MIS_Name = res.Key.ProdGrp_MIS_Name,//
                               Order_Value = Math.Round((double)res.Sum(res => res.Order_Value), 0),
                               Order_Qty = res.Sum(res => res.Order_Qty),
                               Id = Id++
                           }).OrderByDescending(res => res.Year).OrderBy(res => res.Month).ThenBy(res => res.ProdGrp_MIS_Id);

                    return Ok(query);
                }
                else
                {
                    var query = result.Where(res => res.Product_Code == Product_Code
                                           && res.Order_Year == Year)
                           .GroupBy(res => new
                           {
                               res.Order_Year,
                               res.Company_Code,
                               res.Product,
                               res.Month,
                               res.MonthName,
                               res.ProdGrp_MIS_Id,
                               res.ProdGrp_MIS_Name,
                               res.COrder_Serial_No,
                               res.Customer_Code,
                               res.Customer_Name,
                               res.Major_No,
                               res.Minor_No
                           })
                           .Select(res => new
                           {
                               Company_Code = res.Key.Company_Code, //
                               Year = res.Key.Order_Year.ToString(),//
                               Order_No = res.Key.COrder_Serial_No,
                               Customer_Code = res.Key.Customer_Code,
                               Customer_Name = res.Key.Customer_Name,
                               Major_No = res.Key.Major_No,
                               Minor_No = res.Key.Minor_No,
                               Product = res.Key.Product,
                               Month = res.Key.Month,//
                               Month_Name = res.Key.MonthName,//
                               ProdGrp_MIS_Id = res.Key.ProdGrp_MIS_Id,//
                               ProdGrp_MIS_Name = res.Key.ProdGrp_MIS_Name,//
                               Order_Value = Math.Round((double)res.Sum(res => res.Order_Value), 0),
                               Order_Qty = res.Sum(res => res.Order_Qty),
                               Id = Id++
                           }).OrderByDescending(res => res.Year).OrderBy(res => res.Month).ThenBy(res => res.ProdGrp_MIS_Id);

                    return Ok(query);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later...");              
            }
        }
        [HttpGet]
        [Route("getOrderYear")]
        public async Task<ActionResult<PendingOrder>> getOrderYear()
        {
            try
            {
                var result = await _dashboardRepository.GetPendingOrders();
                return Ok(result.Select(res => res.Order_Year).Distinct());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try Later..");
            }
        }

        [HttpGet]
        [Route("getOrderProducts/{Year}")]
        public async Task<ActionResult<PendingOrder>> getOrderProducts(int Year)
        {
            try
            {
                var result = await _dashboardRepository.GetPendingOrders();
                return Ok(result.Select(res => new { res.Product_Code,res.Product }).Distinct());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try Later..");
            }
        }

    }    
}
