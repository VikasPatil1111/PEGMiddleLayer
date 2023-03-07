using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEGMiddleLayer.Models.Common;
using PEGMiddleLayer.Models.CompanySelection;
using PEGMiddleLayer.Models.Dashboard.Booking;
using PEGMiddleLayer.Models.Dashboard.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Controllers.Dashboard
{
    [Authorize(Roles = "SuperAdmin,BookingDashboard,Admin")]
   
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingDashboardRepository _bookingDashboardRepository;
        private readonly ICompanyMasterRepository _companyMasterRepository;
        private readonly ItblBI_PeriodRepository _tblBI_PeriodRepository;
        public BookingController(IBookingDashboardRepository bookingDashboardRepository, 
            ICompanyMasterRepository companyMasterRepository, ItblBI_PeriodRepository tblBI_PeriodRepository)
        {
            _bookingDashboardRepository = bookingDashboardRepository;
            _companyMasterRepository = companyMasterRepository;
            _tblBI_PeriodRepository = tblBI_PeriodRepository;
        }

        [HttpGet]
        [Route("GetBookingDetails/{Year?}/{MonthName}/{Customer}/{Company}")]
        public async Task<ActionResult<BookingTable>> GetBookingDetails(int? year,int MonthName,string Customer,string Company)
        {
            try
            {
                var setYear = year == null ? System.DateTime.Now.Year : year;

               // string Month = MonthName;
                int Year = Convert.ToInt32(setYear);
                var result = await _bookingDashboardRepository.bookingTables(Year,MonthName, Customer, Company);
                var resultYTD = await _bookingDashboardRepository.bookingTablesYTD(Year, MonthName, Customer, Company);

                var Targetresult = await _bookingDashboardRepository.bookingTargetTables(Company);
                string com;
                if (Company == null)
                {
                    com = "PEG";
                }
                else
                {
                    com = Company;
                }
               // var companyUser = await _companyMasterRepository.tblCompanyUsers();
                double setMonth = 0;

                    if (setYear == System.DateTime.Now.Year)
                {
                    setMonth = System.DateTime.Now.Month;
                }
                else
                {
                    setMonth = 12;
                }
                var query1 = result.GroupBy(res => new { res.Company_Code,res.ID }).

                                         Select(res => new
                                         {
                                             Company_Code = res.Key.Company_Code,
                                             ID = res.Key.ID,
                                             Order_Value = Math.Round((decimal)res.Sum(res => res.Order_Value) / 100000, 0),
                                             Target_Value = from target in Targetresult
                                                            .GroupBy(tar => new { tar.Company_Code, tar.Year })
                                                            .Where(tar => tar.Key.Company_Code == res.Key.Company_Code && tar.Key.Year == Year)
                                                            select target.Sum(tar => tar.Target).Value,
                                             Target_YTD = from target in Targetresult
                                                           .GroupBy(tar => new { tar.Company_Code, tar.Year })
                                                          .Where(tar => tar.Key.Company_Code == res.Key.Company_Code && tar.Key.Year == Year)
                                                              // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code).                                                               
                                                                select Math.Round((target.Sum(tar => tar.Target).Value / 12) * setMonth, 0),
                                             Order_ValueYTD = from OrderYTD in resultYTD
                                                              .GroupBy(res1 => new { res1.Company_Code, res1.Year })
                                                              .Where(res1 => res1.Key.Company_Code == res.Key.Company_Code)
                                                              select Math.Round((decimal)OrderYTD.Sum(res1 => res1.Order_Value) / 100000, 0)



                                         }).OrderBy(res => res.ID);

                var queryTotal = result.GroupBy(res => new { res.Year }).
                                          Select(res => new
                                          {
                                              Company_Code =  Company == "null" ?  "PEG" : "Total",
                                              ID = 4,
                                              Order_Value = Math.Round((decimal)res.Sum(res => res.Order_Value) / 100000, 0),
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
                                              Order_ValueYTD = from OrderYTD in resultYTD
                                            .GroupBy(res1 => new {  res1.Year })
                                            //.Where(res1 => res1.Key.Company_Code == res.Key.Company_Code)
                                                               select Math.Round((decimal)OrderYTD.Sum(res1 => res1.Order_Value) /100000,0)
                                          });

                return Ok(query1.Union(queryTotal));
                //if (MonthName == 0)
                //{
                //    var query1 = result.Where(res => res.Year == Year).GroupBy(res => new { res.Company_Code }).

                //                               Select(res => new
                //                               {
                //                                   Company_Code = res.Key.Company_Code,
                //                                   Order_Value = Math.Round((decimal)res.Sum(res => res.Order_Value) / 100000, 0),
                //                                   Target_Value = from target in Targetresult
                //                                                  .GroupBy(tar => new { tar.Company_Code, tar.Year })
                //                                                  .Where(tar => tar.Key.Company_Code == res.Key.Company_Code && tar.Key.Year == Year)
                //                                                  select target.Sum(tar => tar.Target).Value,
                //                                   Target_YTD = from target in Targetresult
                //                                                 .GroupBy(tar => new { tar.Company_Code, tar.Year })
                //                                                .Where(tar => tar.Key.Company_Code == res.Key.Company_Code && tar.Key.Year == Year)
                //                                                    // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code).                                                               
                //                                                select Math.Round((target.Sum(tar => tar.Target).Value / 12) * setMonth, 0)

                //                               }).OrderBy(res => res.Company_Code);

                //    var queryTotal = result.Where(res => res.Year == Year).GroupBy(res => new { res.Year }).
                //                              Select(res => new
                //                              {
                //                                  Company_Code = "PEG",
                //                                  Order_Value = Math.Round((decimal)res.Sum(res => res.Order_Value) / 100000, 0),
                //                                  Target_Value = from target in Targetresult
                //                                                 .GroupBy(tar => new { tar.Year })
                //                                                 .Where(tar => tar.Key.Year == Year)
                //                                                     // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code)
                //                                                 select target.Sum(tar => tar.Target).Value,
                //                                  Target_YTD = from target in Targetresult
                //                                                  .GroupBy(tar => new { tar.Year })
                //                                                  .Where(tar => tar.Key.Year == Year)
                //                                                   // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code)
                //                                               select Math.Round((target.Sum(tar => tar.Target).Value / 12) * setMonth, 0)
                //                              });

                //    return Ok(query1.Union(queryTotal));
                //}
                //else {
                //    var query1 = result.Where(res => res.Year == Year && res.Month==MonthName).GroupBy(res => new { res.Company_Code }).

                //                                   Select(res => new
                //                                   {
                //                                       Company_Code = res.Key.Company_Code,
                //                                       Order_Value = Math.Round((decimal)res.Sum(res => res.Order_Value) / 100000, 0),
                //                                       Target_Value = from target in Targetresult
                //                                                      .GroupBy(tar => new { tar.Company_Code, tar.Year })
                //                                                      .Where(tar => tar.Key.Company_Code == res.Key.Company_Code && tar.Key.Year == Year )
                //                                                      select target.Sum(tar => tar.Target).Value,
                //                                       Target_YTD = from target in Targetresult
                //                                                     .GroupBy(tar => new { tar.Company_Code, tar.Year })
                //                                                    .Where(tar => tar.Key.Company_Code == res.Key.Company_Code && tar.Key.Year == Year)
                //                                                        // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code).                                                               
                //                                                select Math.Round((target.Sum(tar => tar.Target).Value / 12) * setMonth, 0)

                //                                   }).OrderBy(res => res.Company_Code);

                //    var queryTotal = result.Where(res => res.Year == Year && res.Month == MonthName).GroupBy(res => new { res.Year }).
                //                              Select(res => new
                //                              {
                //                                  Company_Code = "PEG",
                //                                  Order_Value = Math.Round((decimal)res.Sum(res => res.Order_Value) / 100000, 0),
                //                                  Target_Value = from target in Targetresult
                //                                                 .GroupBy(tar => new { tar.Year })
                //                                                 .Where(tar => tar.Key.Year == Year)
                //                                                     // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code)
                //                                                 select target.Sum(tar => tar.Target).Value,
                //                                  Target_YTD = from target in Targetresult
                //                                                  .GroupBy(tar => new { tar.Year })
                //                                                  .Where(tar => tar.Key.Year == Year)
                //                                                   // .Where(tar => tar.Key.Company_Code == res.Key.Company_Code)
                //                                               select Math.Round((target.Sum(tar => tar.Target).Value / 12) * setMonth, 0)
                //                              });

                //    return Ok(query1.Union(queryTotal));
                //}
            }
            catch (Exception ex)
            {
                return NoContent();

            }
        }

        [HttpGet]
        [Route("bookingTargetTables")]
        public async Task<ActionResult<BookingTargetTable>> bookingTargetTables()
        {
            try
            {
                var Result = await _bookingDashboardRepository.bookingTargetTables("");

                return Ok(Result);
            }
            catch (Exception ex)
            {

                return NoContent();
            }

        }
        [HttpGet]
        [Route("GetDealerNonDealer/{Year?}/{Month}/{Customer}/{Company}")]
        public async Task<ActionResult<BookingTable>> GetDealerNonDealer(int? year,int Month,string Customer,string Company)
        {
            try
            {
                var setYear = year == null ? System.DateTime.Now.Year : year;
                int Year = Convert.ToInt32(setYear);
               
                var result = await _bookingDashboardRepository.GetBookingDealerNonDealers( Year, Month, Customer, Company);
                var query = result.Where(res => res.Year == Year).GroupBy(res => new { res.Company_Code,res.ID }).
                                Select(res => new {
                                    Company_Code = res.Key.Company_Code,
                                    IDs = res.Key.ID,
                                    Dealer = res.Sum(res=> Math.Round(res.Dealer / 100000, 0)),
                                    Direct = res.Sum(res => Math.Round(res.Direct / 100000, 0)),
                                    Inter_Company = res.Sum(res => Math.Round(res.Inter_Company / 100000, 0))
                                }).OrderBy(res => res.IDs);
                var query_PEG = result.Where(res => res.Year == Year).
                                GroupBy(res => true).
                                Select(res => new {
                                    Company_Code = Company == "null" ? "PEG" : "Total",
                                    IDs =4,
                                    Dealer = Math.Round(res.Sum(res => res.Dealer) / 100000, 0),
                                    Direct = Math.Round(res.Sum(res => res.Direct) / 100000, 0),
                                    Inter_Company = Math.Round(res.Sum(res => res.Inter_Company) / 100000, 0)
                                });
                return Ok(query.Union(query_PEG));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Retriving Data From Database");
            }

        }

        [HttpGet]
        [Route("GetDataInGrid/{Year?}/{Month}/{Customer}/{Company}")]
        public async Task<ActionResult<BookingTable>> GetDataInGrid(int? year, int Month, string Customer, string Company)
        {
            try
            {
                var setYear = year == null ? System.DateTime.Now.Year : year;
                int Year = Convert.ToInt32(setYear);
                int id = 1;
                var result = await _bookingDashboardRepository.bookingTables(Year, Month, Customer, Company);
                var query = result.Where(res => res.Year == Year)
                                .GroupBy(res =>
                                            new { 
                                                res.Year,
                                                res.Month_Name,
                                                res.Company_Code,
                                                res.ID,
                                                res.Dealer_NonDealer,
                                                res.Order_Year,
                                                res.Order_Type,
                                                res.Order_Serial_No,
                                                res.Create_Date,
                                                res.Branch_Name,
                                                res.Customer_Code,
                                                res.Customer_Name,
                                                res.Order_Date
                                            }).
                                Select(res => new {
                                    Id = id++,
                                    Year= res.Key.Year,
                                    Month_Name=  res.Key.Month_Name,
                                    Company_Code = res.Key.Company_Code,
                                    IDs = res.Key.ID,
                                    Dealer_NonDealer = res.Key.Dealer_NonDealer,
                                    Order_Year = res.Key.Order_Year,
                                    Order_Type = res.Key.Order_Type,
                                    Order_Serial_No = res.Key.Order_Serial_No,
                                    Order_Date = res.Key.Order_Date.Value.ToString("dd/MM/yyyy"),
                                    Branch_Name = res.Key.Branch_Name,
                                    Customer_Code = res.Key.Customer_Code,
                                    Customer_Name = res.Key.Customer_Name,
                                    Order_Value = res.Sum(res => res.Order_Value)
                                }).OrderBy(res => res.IDs);
               
                return Ok(query);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error While Retriving Data From Database");
            }

        }

        [HttpGet]
        [Route("GetBookingBranchWise/{Year?}/{Month}/{Customer}/{Comapny}")]
        public async Task<ActionResult<BookingTable>> GetBookingBranchWise(int? year,int Month,string Customer,string Comapny)
        {
            var setYear = year == null ? System.DateTime.Now.Year : year;
            int Year = Convert.ToInt32(setYear);
            //var result = await _bookingDashboardRepository.bookingTables();
            var result1 = await _bookingDashboardRepository.GetBookingCompanyBranches(Year,Month,Customer, Comapny);
            //var query = result.Where(res => res.Domestic_Export == "Domestic" && res.Year == Year)
            //                  .GroupBy(res => new { res.Branch_Name })
            //                  .Select(res => new {
            //                      Branch_Name = res.Key.Branch_Name,
            //                      Order_Value = Math.Round( res.Sum( res=> (double)res.Order_Value)/100000,0)
            //                  });
            var query2 = result1.Where(res => res.Year == Year).GroupBy(res => new { res.Branch_Name,res.Serial_No })
                                .Select(res => new { res.Key.Branch_Name, 
                                Serial_No = res.Key.Serial_No,
                                    IV1 = res.Sum(r=> Math.Round(r.IV1,0)) , 
                                    IV2 = res.Sum(r => Math.Round(r.IV2, 0)) ,// Math.Round(res.IVU2, 0), 
                                    EIPL = res.Sum(r => Math.Round(r.EIPL, 0)), //Math.Round(res.EIPL, 0) ,
                                    PEG = res.Sum(r => Math.Round(r.IV1, 0)) + res.Sum(r => Math.Round(r.IV2, 0)) + res.Sum(r => Math.Round(r.EIPL, 0))})
                                .OrderBy(res => res.Serial_No);

            return Ok(query2);
        }

        [HttpGet]
        [Route("getBookingYear")]
        public async Task<ActionResult<BookingTable>> getBookingYear()
        {
            try
            {
                var result = await _bookingDashboardRepository.bookingTables(0,0,"","");
                return Ok(result.Select(res => res.Year).OrderBy(res => res.Value).Distinct());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error When Retriving Data From Server");
            }

        }
        [HttpGet]
        [Route("getBookingCompany")]
        public async Task<ActionResult<BookingTable>> getBookingCompany()
        {
            try
            {
                var result = await _bookingDashboardRepository.bookingTables(0, 0, "","");
                return Ok(result.OrderBy(res => res.ID).Select(res => res.Company_Code).Distinct());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error When Retriving Data From Server");
            }

        }
        [HttpGet]
        [Route("GetBookingChartDelayAnalyses")]
        public async Task<ActionResult<BookingChartDelayAnalysis>> GetBookingChartDelayAnalyses()
        {
            try
            {
                // var setYear = year == null ? System.DateTime.Now.Year : year;
                // int Year = Convert.ToInt32( setYear);
                var result = await _bookingDashboardRepository.GetBookingChartDelayAnalyses();
                int index = 1;
                var query = result.OrderBy(res => res.Year) //.ThenBy(res => res.Month)
                                                            //.Where(res => res.Year == Year)
                    .GroupBy(res => new { res.Year })
                    .Select(res => new
                    {
                        //Month_Name = res.Key.Month_Name,
                        //Year = res.Key.Year,
                        //Month = res.Key.Month,
                        Year = res.Key.Year, //.ToString().Substring(2, 2),
                        BFV = Math.Round(res.Sum(res => res.ButterFlyValve / 100000), 0),
                        BLV = Math.Round(res.Sum(res => res.BallValve / 100000), 0),
                        ACT = Math.Round(res.Sum(res => res.Actuator / 100000), 0),
                        GGC = Math.Round(res.Sum(res => res.GGCValve / 100000), 0),
                        PEG = Math.Round(res.Sum(res => res.ButterFlyValve / 100000), 0) + Math.Round(res.Sum(res => res.BallValve / 100000), 0) + Math.Round(res.Sum(res => res.Actuator / 100000), 0) + Math.Round(res.Sum(res => res.GGCValve / 100000), 0),
                        Id = index++
                    }); ;
                return Ok(query);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error When Rertiving data from database");

            }
        }
        [HttpGet]
        [Route("GetBookingYearDelayAnalyses/{Company}")]
        public async Task<ActionResult<BookingTable>> GetBookingYearDelayAnalyses(string Company)
        {
            try
            {
                var result = await _bookingDashboardRepository.bookingTables(0,0,"", Company);
                var query = result.GroupBy(res => new { res.Year }).Select(
                    res => new {
                        res.Key.Year,
                        Order_Value = Math.Round((double)res.Sum( res=>res.Order_Value/100000),0)
                    }
                    );
                return Ok(query);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error While getting Data from Server");
            }
        }
        [HttpGet]
        [Route("GetBookingMonthProductChart/{Year}")]
        public async Task<ActionResult<BookingTable>> GetBookingMonthProductChart(int Year)
        {
            try
            {
                var result = await _bookingDashboardRepository.bookingTables(Year,0,"","");
                var Query = result.Where(res => res.Year == Year)
                                  .GroupBy(res => new { res.Month_Name })
                                  .Select(res => new { Month_Name = res.Key.Month_Name });
                var Query1 = result.Select(res => new { res.Month, res.Month_Name }).OrderBy(res => res.Month).Distinct();
                //  retutn Ok(result.Select(res => res.Year).OrderBy(res => res.Value).Distinct());
                return Ok(Query1);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error When Data From Server");
            }

        }
        [HttpGet]
        [Route("GetBookingProductNameProductChart/{Year}")]
        public async Task<ActionResult<BookingTable>> GetBookingProductNameProductChart(int Year)
        {
            try
            {

                var result = await _bookingDashboardRepository.bookingTables(2023,0,"","");
                var Query = result.Where(res => res.Year >2021)
                                   //  .GroupBy(res => new { res.Product_Code, res.Product })
                                    
                                   .Select(res => new
                                   {
                                       //Product_Code = res.Key.Product_Code,
                                       //Product = res.Key.Product
                                       Product_Code = res.Product_Code,
                                       Product = res.Product
                                   }).Distinct(); 
                return Ok(Query);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error When Getting Data From Database");
            }

        }
        [HttpGet]
        [Route("GetBFVProductSpecGrid/{Product_Code}/{Year}/{Company}")]
        public async Task<ActionResult<tblBI_BFV_Order_TechDtl>> GetBFVProductSpec(int Product_Code, int Year,string Company)
        {

            try
            {
                var Book_result = await _bookingDashboardRepository.GetTblBooking_Tech_Dtls(Year,Product_Code,"", Company,0);
                int Id = 1;
                var query = Book_result.Where(res => res.Product_Code == Product_Code
                                       && res.Year == Year)
                       .GroupBy(res => new { res.Year, res.Company_Code,
                           res.Product,
                           res.Month, 
                           res.Month_Name, 
                           res.ProdGrp_MIS_Id, 
                           res.ProdGrp_MIS_Name ,
                           //res.Order_Serial_No,
                           //res.Customer_Code,
                           //res.Customer_Name,
                           //res.Major_No,
                           //res.Minor_No
                       })
                       .Select(res => new
                       {
                           Company_Code = res.Key.Company_Code, //
                           Year = res.Key.Year.ToString(),//
                           //Order_No = res.Key.Order_Serial_No,
                           //Customer_Code = res.Key.Customer_Code,
                           //Customer_Name = res.Key.Customer_Name,
                           //Major_No = res.Key.Major_No,
                           //Minor_No = res.Key.Minor_No,
                           Product = res.Key.Product,
                           Month = res.Key.Month,//
                           Month_Name = res.Key.Month_Name,//
                           ProdGrp_MIS_Id = res.Key.ProdGrp_MIS_Id,//
                           ProdGrp_MIS_Name = res.Key.ProdGrp_MIS_Name,//
                           Order_Value = Math.Round(res.Sum(res => res.Order_Value), 0),
                           Id = Id++
                       }).OrderByDescending(res => res.Year).OrderBy(res => res.Month).ThenBy(res => res.ProdGrp_MIS_Id);

                return Ok(query);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error when retriving data from server");
            }

        }

        [HttpGet]
        [Route("GetBFVProductSpecChart/{Year}/{Product_Code}/{Customer}/{Company}/{Month?}")]
        public async Task<ActionResult<tblBI_BFV_Order_TechDtl>> GetBFVProductSpecChart(int Year, int Product_Code,string Customer,string Company, int? Month)
        {
            try
            {
                var result = await _bookingDashboardRepository.GetTblBooking_Tech_Dtls(Year,Product_Code,Customer, Company, Month);

                var query =
                     result
                     //.Where(res => res.Product_Code == Product_Code && res.Year == Year)
                                  .GroupBy(res => new { res.Company_Code, res.Year, res.ProdGrp_MIS_Id, res.ProdGrp_MIS_Name })
                                  .Select(res => new
                                  {
                                      Company_Code = res.Key.Company_Code,
                                      Year = res.Key.Year,
                                      ProdGrp_MIS_Id = res.Key.ProdGrp_MIS_Id,
                                      ProdGrp_MIS_Name = res.Key.ProdGrp_MIS_Name,
                                      Order_Value = Math.Round(res.Sum(res => res.Order_Value) / 100000, 0)

                                  }).OrderBy(res => res.ProdGrp_MIS_Id);
                return Ok(query);

                //if (Month == null || Month==0)
                //{
                //    var query =
                //     result.Where(res => res.Product_Code == Product_Code && res.Year == Year)
                //                  .GroupBy(res => new { res.Company_Code, res.Year, res.ProdGrp_MIS_Id, res.ProdGrp_MIS_Name })
                //                  .Select(res => new
                //                  {
                //                      Company_Code = res.Key.Company_Code,
                //                      Year = res.Key.Year,
                //                      ProdGrp_MIS_Id = res.Key.ProdGrp_MIS_Id,
                //                      ProdGrp_MIS_Name = res.Key.ProdGrp_MIS_Name,
                //                      Order_Value = Math.Round(res.Sum(res => res.Order_Value) / 100000, 0)

                //                  }).OrderBy(res => res.ProdGrp_MIS_Id);
                //    return Ok(query);
                //}
                //else
                //{
                //    var query =
                //         result.Where(res => res.Product_Code == Product_Code && res.Year == Year && res.Month == Month)
                //                      .GroupBy(res => new { res.Company_Code, res.Year, res.ProdGrp_MIS_Id, res.ProdGrp_MIS_Name })
                //                      .Select(res => new
                //                      {
                //                          Company_Code = res.Key.Company_Code,
                //                          Year = res.Key.Year,
                //                          ProdGrp_MIS_Id = res.Key.ProdGrp_MIS_Id,
                //                          ProdGrp_MIS_Name = res.Key.ProdGrp_MIS_Name,
                //                          Order_Value = Math.Round(res.Sum(res => res.Order_Value) / 100000, 0)

                //                      }).OrderBy(res => res.ProdGrp_MIS_Id);
                //    return Ok(query);
                //}

                // return NotFound();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error When Getting Records From Server");

            }
        }

        [HttpGet]
        [Route("GetStoreProcedure")]
        public async Task<ActionResult<tblBooking_Tech_Dtl>> GetStoreProcedure()
        {
            try
            {
                var result = await _bookingDashboardRepository.GetTblBooking_Tech_DtlsSP();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try again later..");
            }
        }
        [HttpGet]
        [Route("GetPeriod/{Year}")]
        public async Task<ActionResult<tblBI_Period>> gettblBI_Periods(int Year) {
            var result = await _tblBI_PeriodRepository.gettblBI_Periods();
            if (Year == System.DateTime.Now.Year)
            {
                var query = result.Where(res => res.Year == Year && res.Module =="BOOKING")
                            .Select(res => new
                            {
                                FromDate = res.FromDate.ToString("dd/MM/yyyy"),
                                ToDate = System.DateTime.Now.ToString("dd/MM/yyyy")
                            });
                return Ok(query);
            }
            else
            {
                var query = result.Where(res => res.Year == Year && res.Module == "BOOKING")
                              .Select(res => new
                              {
                                  FromDate = res.FromDate.ToString("dd/MM/yyyy"),
                                  ToDate = res.ToDate.ToString("dd/MM/yyyy")
                              });
                return Ok(query);
            }
            
        }

        [HttpGet]
        [Route("BookingCustomerList/{Year}/{Company}/{Month}")]
        public async Task<ActionResult<vw_BookingCustomerList>> BookingCustomerList(int Year,string Company,int Month)
        {
            try
            {
                var result = await _bookingDashboardRepository.GetBookingCustomerList(Year, Company, Month);
                return Ok(result.OrderBy(res=>  res.PAN_No).ThenBy(res =>res.GSTIN));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try again later..");
                
            }
          
        }

    }
}
