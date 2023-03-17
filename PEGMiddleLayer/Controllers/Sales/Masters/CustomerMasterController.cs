using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEGMiddleLayer.Models.Sales.Masters;
using PEGMiddleLayer.Models.Sales.Utility;
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
        [Route("CustomerMasterList/{Search}")]
        public async Task<IActionResult> getCustomerMaster(string Search)
        {
            var customerList = await _customerMasterRepository.getCustomerMasters(Search);
            return Ok(customerList.OrderBy(s => s._Id).Take(100));
            //return Ok("Success");
        }

        [HttpPost]
        [Route("CreateCustomerMaster")]
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new Customer Master" + ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateCustomerMaster/{Id}")]
        public async Task<IActionResult> UpdateCustomerMaster([FromBody] CustomerMaster customerMaster,int Id)
        {
            try
            {
                if (customerMaster == null)
                    return BadRequest();

                var CreateCustomerMaster = await _customerMasterRepository.UpdateCustomerMaster(customerMaster,Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new Customer Master" + ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteCustomerMaster/{Id}")]
        public async Task<IActionResult> DeleteCustomerMaster(int Id)
        {
            try
            {
                var result = await _customerMasterRepository.DeleteCustomerMaster(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        
        }

        [HttpGet]
        [Route("getGSTPercentMaster")]
        public async Task<ActionResult<GST_Percent_Master>> getGSTPercentMaster()
        {
            try
            {
                var result = await _customerMasterRepository.GetGST_Percent_Masters();
                return Ok(result.OrderBy(res => res.GST_Percent));
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        [HttpGet]
        [Route("getStateMaster")]
        public async Task<ActionResult<State_Master>> getStateMaster()
        {
            try
            {
                var result = await _customerMasterRepository.GetState_Masters();
                return Ok(result.OrderBy(res => res.GST_State_Code));
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpGet]
        [Route("getCostCenter")]
        public async Task<ActionResult<Cost_Centre>> GetCost_Centres()
        {
            try
            {
                var result = await _customerMasterRepository.GetCost_Centres();
                return Ok(result.OrderBy(res => res.Cost_Centre_Code));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getCountryMaster")]
        public async Task<ActionResult<Country_Master>> getCountryMaster()
        {
            try
            {
                var result = await _customerMasterRepository.GetCountry_Masters();
                return Ok(result.OrderBy(res => res.Country_Code));

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
