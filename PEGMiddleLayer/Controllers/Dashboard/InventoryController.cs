using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEGMiddleLayer.Models.Dashboard.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet]
        [Route("getInventoryCompany")]
        public async Task<ActionResult<tblTotal_Inventory_Details>> getInventoryCompany()
        {
            try
            {
                var result = await _inventoryRepository.tblTotal_Inventory_Details("");
                var query = result.OrderBy(res => res.ID).Select(res => res.Company_Code).Distinct();
                return Ok(query);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }

        [HttpGet]
        [Route("GetInventotyTypeDetails/{Companycode}")]
        public async Task<ActionResult<tblTotal_Inventory_Details>> GetInventotyTypeDetails(string Companycode)
        {
            try
            {
                // var result = await _inventoryRepository.vwInventotyTypeDetails();
                var result = await _inventoryRepository.tblTotal_Inventory_Details(Companycode);

                var query = result.OrderBy(res => res.ID)
                    .GroupBy(res => new { res.Company_Code })
                    .Select(res =>
                    new
                    {
                        Company_Code = res.Key.Company_Code,
                        //MISInventory_Type = res.Key.MISInventory_Type,
                        RAW_STORE = Math.Round( res.Sum(res => (double) res.Alloc_Value) / 100000, 0),
                        WIP_STORE = Math.Round( (res.Sum(res => (double) res.Free_Value)+res.Sum(res=> (double) res.Obsolete_Value)) / 100000, 0),
                        Total = Math.Round(res.Sum(res => (double)res.Alloc_Value) / 100000, 0)+ Math.Round((res.Sum(res => (double)res.Free_Value) + res.Sum(res => (double)res.Obsolete_Value)) / 100000, 0)
                    }
                  ).Where(res => res.Total>0);

                var queryPEG = result.OrderBy(res => res.ID)
                   .GroupBy(res => true)
                   .Select(res =>
                   new
                   {
                       Company_Code = "PEG",
                        //MISInventory_Type = res.Key.MISInventory_Type,
                        RAW_STORE = Math.Round(res.Sum(res => (double)res.Alloc_Value) / 100000, 0),
                       WIP_STORE = Math.Round((res.Sum(res => (double)res.Free_Value) + res.Sum(res => (double)res.Obsolete_Value)) / 100000, 0),
                       Total = Math.Round(res.Sum(res => (double)res.Alloc_Value) / 100000, 0) + Math.Round((res.Sum(res => (double)res.Free_Value) + res.Sum(res => (double)res.Obsolete_Value)) / 100000, 0)
                       // Total = Math.Round((res.Sum(res => res.RAW_STORE) + res.Sum(res => res.WIP_STORE)) /  100000, 0)
                   }
                 ).Where(res => res.Total > 0);

                return Ok(query.Union(queryPEG));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }
        [HttpGet]
        [Route("getAgingDetails/{Companycode}")]
        public async Task<ActionResult<vwInventoryAgingSummary>> getAgingDetails(string Companycode)
        {
            try
            {
                int index = 0;
                var result = await _inventoryRepository.vwInventoryAgingSummaries(Companycode);
                var query = result
                    //.OrderBy(res => res.Sr)
                    .GroupBy(res => new { res.Inventory_Type,res.SrNo })
                    .Select(res => new {
                        Id = index++,
                       // IDs = res.Key.ID,
                      //  Company_Code = res.Key.Company_Code,
                        Inventory_Type = res.Key.Inventory_Type,
                        SrNo = (int) res.Key.SrNo,
                        Age_0_30 = Math.Round( res.Sum(res => res.Age_0_30) /100000,0),

                        Age_31_60 = Math.Round(res.Sum(res => res.Age_31_60) / 100000, 0),
                        Age_61_90 = Math.Round(res.Sum(res => res.Age_61_90) / 100000, 0),
                        Age_91_180 = Math.Round(res.Sum(res => res.Age_91_180) / 100000, 0),
                        Age_Above_180 = Math.Round(res.Sum(res => res.Age_Above_180) / 100000, 0),
                        Total = Math.Round((res.Sum(res => res.Age_0_30)  + res.Sum(res => res.Age_31_60) + res.Sum(res => res.Age_61_90) + res.Sum(res => res.Age_91_180) + res.Sum(res => res.Age_Above_180))/100000,0)

                    }).OrderBy(res => res.SrNo);

                var queryPEG = result.GroupBy(res => true)
                   .Select(res => new {
                       Id = index++,
                        // IDs = res.Key.ID,
                        //  Company_Code = res.Key.Company_Code,
                       Inventory_Type = "Total",
                       SrNo =0,
                       Age_0_30 = Math.Round(res.Sum(res => res.Age_0_30) / 100000, 0),

                       Age_31_60 = Math.Round(res.Sum(res => res.Age_31_60) / 100000, 0),
                       Age_61_90 = Math.Round(res.Sum(res => res.Age_61_90) / 100000, 0),
                       Age_91_180 = Math.Round(res.Sum(res => res.Age_91_180) / 100000, 0),
                       Age_Above_180 = Math.Round(res.Sum(res => res.Age_Above_180) / 100000, 0),
                       Total = Math.Round((res.Sum(res => res.Age_0_30) + res.Sum(res => res.Age_31_60) + res.Sum(res => res.Age_61_90) + res.Sum(res => res.Age_91_180) + res.Sum(res => res.Age_Above_180)) / 100000, 0)

                   });

                return Ok(query.Union(queryPEG));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later.."); 
            }
        }

        [HttpGet]
        [Route("getAllocatedAgingDetails/{Companycode}")]
        public async Task<ActionResult<vwInventoryAgingSummary>> getAllocatedAgingDetails(string Companycode)
        {
            try
            {
                int index = 0;
                var result = await _inventoryRepository.vwInventoryAgingSummaryAllocated(Companycode);
                var query = result
                    //.OrderBy(res => res.Sr)
                    .GroupBy(res => new { res.Inventory_Type,res.SrNo })
                    .Select(res => new {
                        Id = index++,
                        // IDs = res.Key.ID,
                        //  Company_Code = res.Key.Company_Code,
                        Inventory_Type = res.Key.Inventory_Type,
                        SrNo = (int) res.Key.SrNo,
                        Age_0_30 = Math.Round(res.Sum(res => res.Age_0_30) / 100000, 0),

                        Age_31_60 = Math.Round(res.Sum(res => res.Age_31_60) / 100000, 0),
                        Age_61_90 = Math.Round(res.Sum(res => res.Age_61_90) / 100000, 0),
                        Age_91_180 = Math.Round(res.Sum(res => res.Age_91_180) / 100000, 0),
                        Age_Above_180 = Math.Round(res.Sum(res => res.Age_Above_180) / 100000, 0),
                        Total = Math.Round((res.Sum(res => res.Age_0_30) + res.Sum(res => res.Age_31_60) + res.Sum(res => res.Age_61_90) + res.Sum(res => res.Age_91_180) + res.Sum(res => res.Age_Above_180)) / 100000, 0)

                    }).Where(res => res.Total >0).OrderBy(res => res.SrNo);

                var queryPEG = result.GroupBy(res => true)
                   .Select(res => new {
                       Id = index++,
                       // IDs = res.Key.ID,
                       //  Company_Code = res.Key.Company_Code,
                       Inventory_Type = "Total",
                       SrNo =0,
                       Age_0_30 = Math.Round(res.Sum(res => res.Age_0_30) / 100000, 0),

                       Age_31_60 = Math.Round(res.Sum(res => res.Age_31_60) / 100000, 0),
                       Age_61_90 = Math.Round(res.Sum(res => res.Age_61_90) / 100000, 0),
                       Age_91_180 = Math.Round(res.Sum(res => res.Age_91_180) / 100000, 0),
                       Age_Above_180 = Math.Round(res.Sum(res => res.Age_Above_180) / 100000, 0),
                       Total = Math.Round((res.Sum(res => res.Age_0_30) + res.Sum(res => res.Age_31_60) + res.Sum(res => res.Age_61_90) + res.Sum(res => res.Age_91_180) + res.Sum(res => res.Age_Above_180)) / 100000, 0)

                   }).Where(res => res.Total > 0); ;

                return Ok(query.Union(queryPEG));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }

        [HttpGet]
        [Route("getUnAllocatedAgingDetails/{Companycode}")]
        public async Task<ActionResult<vwInventoryAgingSummary>> getUnAllocatedAgingDetails(string Companycode)
        {
            try
            {
                int index = 0;
                var result = await _inventoryRepository.vwInventoryAgingSummaryUnAllocated(Companycode);
                var query = result
                    //.OrderBy(res => res.Sr)
                    .GroupBy(res => new { res.Inventory_Type,res.SrNo })
                    .Select(res => new {
                        Id = index++,
                        // IDs = res.Key.ID,
                        //  Company_Code = res.Key.Company_Code,
                        Inventory_Type = res.Key.Inventory_Type,
                        SrNo = (int)res.Key.SrNo,
                        Age_0_30 = Math.Round(res.Sum(res => res.Age_0_30) / 100000, 0),

                        Age_31_60 = Math.Round(res.Sum(res => res.Age_31_60) / 100000, 0),
                        Age_61_90 = Math.Round(res.Sum(res => res.Age_61_90) / 100000, 0),
                        Age_91_180 = Math.Round(res.Sum(res => res.Age_91_180) / 100000, 0),
                        Age_Above_180 = Math.Round(res.Sum(res => res.Age_Above_180) / 100000, 0),
                        Total = Math.Round((res.Sum(res => res.Age_0_30) + res.Sum(res => res.Age_31_60) + res.Sum(res => res.Age_61_90) + res.Sum(res => res.Age_91_180) + res.Sum(res => res.Age_Above_180)) / 100000, 0)

                    }).Where(res => res.Total > 0).OrderBy(res => res.SrNo) ;

                var queryPEG = result.GroupBy(res => true)
                   .Select(res => new {
                       Id = index++,
                       // IDs = res.Key.ID,
                       //  Company_Code = res.Key.Company_Code,
                       Inventory_Type = "Total",
                       SrNo = 10,
                       Age_0_30 = Math.Round(res.Sum(res => res.Age_0_30) / 100000, 0),

                       Age_31_60 = Math.Round(res.Sum(res => res.Age_31_60) / 100000, 0),
                       Age_61_90 = Math.Round(res.Sum(res => res.Age_61_90) / 100000, 0),
                       Age_91_180 = Math.Round(res.Sum(res => res.Age_91_180) / 100000, 0),
                       Age_Above_180 = Math.Round(res.Sum(res => res.Age_Above_180) / 100000, 0),
                       Total = Math.Round((res.Sum(res => res.Age_0_30) + res.Sum(res => res.Age_31_60) + res.Sum(res => res.Age_61_90) + res.Sum(res => res.Age_91_180) + res.Sum(res => res.Age_Above_180)) / 100000, 0)

                   }).Where(res => res.Total > 0); 

                return Ok(query.Union(queryPEG));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error Please Try Later..");
            }
        }
    }
}
