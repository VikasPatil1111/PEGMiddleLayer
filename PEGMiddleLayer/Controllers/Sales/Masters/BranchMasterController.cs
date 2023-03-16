using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEGMiddleLayer.Models.Sales.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Controllers.Sales.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchMasterController : ControllerBase
    {
        private readonly IBranchMasterRepository _branchMasterRepository;

        public BranchMasterController(IBranchMasterRepository branchMasterRepository)
        {
            _branchMasterRepository = branchMasterRepository;
        }

        [HttpGet]
        [Route("getBrachMasterDetails")]
        public async Task<ActionResult<Branch_Master>> getBrachMasterDetails()
        {
            try
            {
                var result = await _branchMasterRepository.GetBranch_Masters();
                return Ok(result.OrderBy(res=>res.Branch_Code));
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
