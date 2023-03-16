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
    public class ConsigneeTypeMasterController : ControllerBase
    {
        private readonly IConsignee_Type_MasterRepository _consignee_Type_MasterRepository;

        public ConsigneeTypeMasterController(IConsignee_Type_MasterRepository consignee_Type_MasterRepository)
        {
            _consignee_Type_MasterRepository = consignee_Type_MasterRepository;
        }

        [HttpGet]
        [Route("getConsignee_Type_Master")]
        public async Task<IActionResult> getConsignee_Type_Master() {
            try
            {
                var list = await _consignee_Type_MasterRepository.GetConsignee_Type_Master();
                return Ok(list.OrderBy(x => x.Consignee_Type).Select(res => new { res.Consignee_Type, res.Consignee_Desc }).Distinct());
            }
            catch (Exception e)
            {
                return StatusCode(401,"Error");
            }
        }
    }
}
