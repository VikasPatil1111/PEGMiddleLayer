using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEGMiddleLayer.Models.Dashboard.AccountReceivable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReceivableController : ControllerBase
    {
        private readonly IAccountReceivableRepository _accountReceivableRepository;
        public AccountReceivableController(IAccountReceivableRepository accountReceivableRepository)
        {
            _accountReceivableRepository = accountReceivableRepository;
        }

        [HttpGet]
        [Route("ComWiseAccountRec/{Company}/{CustomerCode}/{DueFilter}/{OverDueFilter}")]
        public async Task<ActionResult<tblMIS_AccRec_Details>> ComWiseAccountRec(string Company,string CustomerCode, int DueFilter ,int OverDueFilter)
        {
            try
            {
                var result = await _accountReceivableRepository.TblMIS_AccRec_Details(Company, CustomerCode, DueFilter,OverDueFilter);

                var query = result.GroupBy(res => new { res.Company_Code,res.ID })
                    .Select(res=> new { 
                        IDs = res.Key.ID,
                        CompanyCode = res.Key.Company_Code,
                        Receivable = Math.Round((double)res.Sum(res=> res.Receivable),0),
                        Due_Total = Math.Round((double)res.Sum(res => res.Cdue_OS0_30), 0)+ Math.Round((double)res.Sum(res => res.Cdue_OS31_60), 0)+Math.Round((double)res.Sum(res => res.Cdue_OS61_90), 0)+ Math.Round((double)res.Sum(res => res.Cdue_OSAbv90), 0),
                        Over_Due_Total = Math.Round((double)res.Sum(res => res.COd_OS0_30), 0) + Math.Round((double)res.Sum(res => res.COd_OS31_60), 0) + Math.Round((double)res.Sum(res => res.COd_OS61_90), 0) + Math.Round((double)res.Sum(res => res.COd_OSAbv90), 0)

                    }).OrderBy(res=> res.IDs);
                var queryPEG = result.GroupBy(res => true)
                    .Select(res => new {
                        IDs = 4,
                        CompanyCode = Company == "null" ? "PEG" : "Total",
                        Receivable = Math.Round((double)res.Sum(res => res.Receivable), 0),
                        Due_Total = Math.Round((double)res.Sum(res => res.Cdue_OS0_30), 0) + Math.Round((double)res.Sum(res => res.Cdue_OS31_60), 0) + Math.Round((double)res.Sum(res => res.Cdue_OS61_90), 0) + Math.Round((double)res.Sum(res => res.Cdue_OSAbv90), 0),
                        Over_Due_Total = Math.Round((double)res.Sum(res => res.COd_OS0_30), 0) + Math.Round((double)res.Sum(res => res.COd_OS31_60), 0) + Math.Round((double)res.Sum(res => res.COd_OS61_90), 0) + Math.Round((double)res.Sum(res => res.COd_OSAbv90), 0)

                    });

                return Ok(query.Union(queryPEG));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later");
            }
           
        }

        [HttpGet]
        [Route("BranchWiseDetails/{Company}/{CustomerCode}/{DueFilter}/{OverDueFilter}")]
        public async Task<ActionResult<tblMIS_AccRec_Details>> BranchWiseDetails(string Company, string CustomerCode, int DueFilter, int OverDueFilter)
        {
            try
            {
                var result = await _accountReceivableRepository.TblMIS_AccRec_Details(Company, CustomerCode, DueFilter, OverDueFilter);
                int id = 0;
                var query = result.GroupBy(res => new { res.Branch_Name, res.SrNo })
                    .Select(res => new
                    {
                        Id = id++,
                        //IDs = res.Key.ID,
                       // Company_Code = res.Key.Company_Code,
                        Branch_Name = res.Key.Branch_Name,
                        SrNo = (int) res.Key.SrNo,
                        Total_Receivable = Math.Round((double)res.Sum(res => res.Receivable), 0),
                        Not_Due_Amount = Math.Round((double)res.Sum(res => res.Cdue_OS0_30), 0) + Math.Round((double)res.Sum(res => res.Cdue_OS31_60), 0) + Math.Round((double)res.Sum(res => res.Cdue_OS61_90), 0) + Math.Round((double)res.Sum(res => res.Cdue_OSAbv90), 0),
                        Cdue_OS0_30 = Math.Round((double)res.Sum(res => res.Cdue_OS0_30), 0),
                        Cdue_OS31_60 = Math.Round((double)res.Sum(res => res.Cdue_OS31_60), 0),
                        Cdue_OS61_90 = Math.Round((double)res.Sum(res => res.Cdue_OS61_90), 0),
                        Cdue_OSAbv90 = Math.Round((double)res.Sum(res => res.Cdue_OSAbv90), 0),
                        Over_Due_Amount = Math.Round((double)res.Sum(res => res.COver_Due), 0),
                        COd_OS0_30 = Math.Round((double)res.Sum(res => res.COd_OS0_30), 0),
                        COd_OS31_60 = Math.Round((double)res.Sum(res => res.COd_OS31_60), 0),
                        COd_OS61_90 = Math.Round((double)res.Sum(res => res.COd_OS61_90), 0),
                        COd_OSAbv90 = Math.Round((double)res.Sum(res => res.COd_OSAbv90), 0),
                    }).OrderBy(res=> res.SrNo);
                var queryTotal = result.GroupBy(res => true)
                   .Select(res => new
                   {
                       Id = id++,
                        //IDs = res.Key.ID,
                        // Company_Code = res.Key.Company_Code,
                        Branch_Name = "Total:-" ,
                       SrNo = 0,
                       Total_Receivable = Math.Round((double)res.Sum(res => res.Receivable), 0),
                       Not_Due_Amount = Math.Round((double)res.Sum(res => res.Cdue_OS0_30), 0) + Math.Round((double)res.Sum(res => res.Cdue_OS31_60), 0) + Math.Round((double)res.Sum(res => res.Cdue_OS61_90), 0) + Math.Round((double)res.Sum(res => res.Cdue_OSAbv90), 0),
                       Cdue_OS0_30 = Math.Round((double)res.Sum(res => res.Cdue_OS0_30), 0),
                       Cdue_OS31_60 = Math.Round((double)res.Sum(res => res.Cdue_OS31_60), 0),
                       Cdue_OS61_90 = Math.Round((double)res.Sum(res => res.Cdue_OS61_90), 0),
                       Cdue_OSAbv90 = Math.Round((double)res.Sum(res => res.Cdue_OSAbv90), 0),
                       Over_Due_Amount = Math.Round((double)res.Sum(res => res.COver_Due), 0),
                       COd_OS0_30 = Math.Round((double)res.Sum(res => res.COd_OS0_30), 0),
                       COd_OS31_60 = Math.Round((double)res.Sum(res => res.COd_OS31_60), 0),
                       COd_OS61_90 = Math.Round((double)res.Sum(res => res.COd_OS61_90), 0),
                       COd_OSAbv90 = Math.Round((double)res.Sum(res => res.COd_OSAbv90), 0),
                   });

                return Ok(query.Union(queryTotal));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try again later...");
            }

        }

        [HttpGet]
        [Route("BranchWiseDetail/{Company}/{CustomerCode}/{DueFilter}/{OverDueFilter}")]
        public async Task<ActionResult<tblMIS_AccRec_Details>> BranchWiseDetail(string Company, string CustomerCode, int DueFilter, int OverDueFilter)
        {
            try
            {
                var result = await _accountReceivableRepository.TblMIS_AccRec_Details(Company, CustomerCode,DueFilter, OverDueFilter);
                int id = 0;
                var query = result.GroupBy(res => new { res.Branch_Name, res.SrNo })
                    .Select(res => new
                    {
                        Id = id++,
                        //IDs = res.Key.ID,
                        // Company_Code = res.Key.Company_Code,
                        Branch_Name = res.Key.Branch_Name,
                        SrNo = (int)res.Key.SrNo,
                        Total_Receivable = Math.Round((double)res.Sum(res => res.Receivable), 0),
                        Not_Due_Amount = Math.Round((double)res.Sum(res => res.Cdue_OS0_30), 0) + Math.Round((double)res.Sum(res => res.Cdue_OS31_60), 0) + Math.Round((double)res.Sum(res => res.Cdue_OS61_90), 0) + Math.Round((double)res.Sum(res => res.Cdue_OSAbv90), 0),
                        Cdue_OS0_30 = Math.Round((double)res.Sum(res => res.Cdue_OS0_30), 0),
                        Cdue_OS31_60 = Math.Round((double)res.Sum(res => res.Cdue_OS31_60), 0),
                        Cdue_OS61_90 = Math.Round((double)res.Sum(res => res.Cdue_OS61_90), 0),
                        Cdue_OSAbv90 = Math.Round((double)res.Sum(res => res.Cdue_OSAbv90), 0),
                        Over_Due_Amount = Math.Round((double)res.Sum(res => res.COver_Due), 0),
                        COd_OS0_30 = Math.Round((double)res.Sum(res => res.COd_OS0_30), 0),
                        COd_OS31_60 = Math.Round((double)res.Sum(res => res.COd_OS31_60), 0),
                        COd_OS61_90 = Math.Round((double)res.Sum(res => res.COd_OS61_90), 0),
                        COd_OSAbv90 = Math.Round((double)res.Sum(res => res.COd_OSAbv90), 0),
                    }).OrderBy(res => res.SrNo);
               

                return Ok(query);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please try again later...");
            }

        }
        [HttpGet]
        [Route("BranchDrilledDown/{Company}/{Branch}/{CustomerCode}/{DueFilter}/{OverDueFilter}")]
        public async Task<ActionResult<tblMIS_AccRec_Details>> BranchDrilledDown(string Company,string Branch, string CustomerCode, int DueFilter, int OverDueFilter)
        {
            try
            {
                var result = await _accountReceivableRepository.TblMIS_AccRec_Details(Company, Branch,CustomerCode, DueFilter, OverDueFilter);
                int Ids = 1;
                var query = result.Select(
                    res => new {
                        Id = Ids++,
                        res.Company_Code,
                     res.Branch_Name      ,
                        res.Customer_Code      ,
                        res.Customer_Name      ,
                        res.Order_Serial_No      ,
                        res.Document_Name      ,
                         res.Document_Date,     
                        res.Lorry_Date      ,
                        res.Payment_Term      ,
                        res.Payment_Days      ,
                        res.Payment_Due_Date      ,
                        res.Document_Amount      ,
                        res.Adjusted_Amount      ,
                        res.Receivable      ,
                        res.Cdue_OS0_30      ,
                        res.Cdue_OS31_60      ,
                        res.Cdue_OS61_90      ,
                        res.Cdue_OSAbv90      ,
                        res.COver_Due      ,
                        res.COd_OS0_30      ,
                        res.COd_OS31_60      ,
                        res.COd_OS61_90      ,
                        res.COd_OSAbv90
                    }
                    );
                return Ok(query);
            }
            catch (Exception ex)
            {

                throw;
            }
           


        }

        [HttpGet]
        [Route("CustomerWiseList/{Company}/{CustomerCode}/{DueFilter}/{OverDueFilter}")]
        public async Task<ActionResult<tblMIS_AccRec_Details>> CustomerWiseList(string Company,  string CustomerCode, int DueFilter, int OverDueFilter)
        {
            try
            {
                var result = await _accountReceivableRepository.TblMIS_AccRec_Details(Company, "", CustomerCode, DueFilter, OverDueFilter);
                int Ids = 1;
                var query = result.GroupBy(res => new {
                    res.Company_Code,
                    res.Customer_Code,
                    res.Customer_Name,
                    res.GSTIN,
                    res.PAN_No,
                }) .Select(
                    res => new {
                        Id = Ids++,
                        Company_Code =   res.Key.Company_Code,
                        Customer_Code= res.Key.Customer_Code,
                        Customer_Name=  res.Key.Customer_Name,
                        GSTIN=  res.Key.GSTIN,
                        PAN_No=  res.Key.PAN_No,
                        Receivable = res.Sum(res=> res.Receivable),
                        Not_Due_Amount = Math.Round((double)res.Sum(res => res.Cdue_OS0_30), 0) + Math.Round((double)res.Sum(res => res.Cdue_OS31_60), 0) + Math.Round((double)res.Sum(res => res.Cdue_OS61_90), 0) + Math.Round((double)res.Sum(res => res.Cdue_OSAbv90), 0),
                        Cdue_OS0_30 = res.Sum(res => res.Cdue_OS0_30),
                        Cdue_OS31_60=  res.Sum(res => res.Cdue_OS31_60),
                        Cdue_OS61_90= res.Sum(res => res.Cdue_OS61_90),
                        Cdue_OSAbv90= res.Sum(res => res.Cdue_OSAbv90),
                        Over_Due_Amount = res.Sum(res => res.COver_Due),
                        COd_OS0_30= res.Sum(res => res.COd_OS0_30),
                        COd_OS31_60=  res.Sum(res => res.COd_OS31_60),
                        COd_OS61_90= res.Sum(res => res.COd_OS61_90),
                        COd_OSAbv90 =  res.Sum(res => res.COd_OSAbv90)
                    }
                    ).Where(res => res.Receivable>0).OrderBy(res => res.GSTIN);
                return Ok(query);
            }
            catch (Exception ex)
            {

                throw;
            }



        }

        [HttpGet]
        [Route("CompanyList")]
        public async Task<ActionResult<tblMIS_AccRec_Details>> CompanyList()
        {
            try
            {
                var result = await _accountReceivableRepository.TblMIS_AccRec_Details("","",0,0);

                return Ok(result.OrderBy(res => res.ID).Select(res => res.Company_Code).Distinct());
               // return Ok(query);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later");
            }
        }
        [HttpGet]
        [Route("CustomerList/{Companycode}")]
        public async Task<ActionResult<tblMIS_AccRec_Details>> CustomerList(string Companycode)
        {
            try {
                var result = await _accountReceivableRepository.TblMIS_AccRec_Details(Companycode,"",0,0);
                var query = result.Select(res => new
                {
                    Customer_Code = res.Customer_Code,
                    Customer_Name = res.Customer_Name,
                    GSTIN = res.GSTIN,
                    PANNO = res.PAN_No
                }).Distinct().OrderBy(res => res.PANNO).ThenBy(res => res.Customer_Code);
                return Ok(query);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }
    }
}
