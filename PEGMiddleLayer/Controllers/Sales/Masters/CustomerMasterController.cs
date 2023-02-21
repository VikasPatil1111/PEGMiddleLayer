using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEGMiddleLayer.Models.Sales.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Controllers.Sales.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerMasterController : ControllerBase
    {

        private readonly ICustomerMasterRepository _customerMasterRepository;
        public CustomerMasterController(ICustomerMasterRepository customerMasterRepository)
        {
            this._customerMasterRepository = customerMasterRepository;
        }

        [HttpGet]
        [Route("CustomerMasterList")]
        public async Task<IActionResult> getCustomerMaster()
        {
             var customerList = await _customerMasterRepository.getCustomerMasters();
             return Ok(customerList.OrderBy(s => s.Customer_Code).Take(100));
             //return Ok("Success");
        }

        [HttpPost]
        [Route("CustomerMasterList")]
        public async Task<IActionResult> CreateCustomerMaster([FromBody] CustomerMaster customerMaster)
        {
            try
            {
                if (customerMaster == null)
                    return BadRequest();

                var CreateCustomerMaster = await _customerMasterRepository.AddCustomerMaster(customerMaster);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new Customer Master"+ex.Message);
            }
        }
    }
}
