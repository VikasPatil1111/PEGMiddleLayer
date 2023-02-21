using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PEGMiddleLayer.Models.Dashboard.MISReport;
using Microsoft.AspNetCore.Authorization;
using PEGMiddleLayer.Models.CompanySelection;
using PEGMiddleLayer.Models.Common;

namespace PEGMiddleLayer.Controllers.Dashboard
{
    [Authorize(Roles = "SuperAdmin,BacklogDashboard,BookingDashboard,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MISReportController : ControllerBase
    {
        private readonly IMISReportRepository _mISReportRepository;
        private readonly ICompanyMasterRepository _companyRepositoryMaster;
        public MISReportController(IMISReportRepository MISReportRepository, ICompanyMasterRepository companyMasterRepository)
        {
            _mISReportRepository = MISReportRepository;
            _companyRepositoryMaster = companyMasterRepository;
        }

      //  [Authorize]
        [HttpGet]
        [Route("GetMISReprotDaily")]
        public async Task<ActionResult<USP_BIRepo_PEG_MIS_SS>> GetMISReprotDaily()
        {
            
            try
            {
                var result = await _mISReportRepository.GetMISReprotDaily();
                //var companyUser = await _companyRepositoryMaster.tblCompanyUsers();
                //var result1 = (from dataComany in result
                //               join userCompany in companyUser
                //               on dataComany..Trim() equals userCompany.CompanyCode.Trim()
                //               where userCompany.User_ID == setDatabase.UserId && userCompany.Allow == true
                //               orderby dataComany.Company_Code
                //               select new
                //               {
                //                   dataComany.Company_Code
                //               }
                //                 );

                return Ok(result.Where(res=> res.SrNo >0));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error");
            }
        }
    }
}
