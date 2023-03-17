using PEGMiddleLayer.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Data;
using PEGMiddleLayer.Models.Sales.Utility;

namespace PEGMiddleLayer.Models.Sales.Masters
{
    public class CustomerMasterRepository : ICustomerMasterRepository
    {
        // private readonly ApplicationDbContext _applicationDbContext;
        private readonly CompanyDbContext _applicationDbContext;
        public CustomerMasterRepository(CompanyDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<CustomerMaster>> getCustomerMasters(string Search)
        {
            IQueryable<CustomerMaster> query = _applicationDbContext.customerMasters;

            string Searching = "%" + Search + "%";

            if (!string.IsNullOrEmpty(Search) && Search != null && Search !="null")
            {
                //  query = query.Where(res => Searching.Contains(res.Customer_Name) ); //|| Search.Contains(res.Customer_Name)
                query = query.Where( res => EF.Functions.Like(res.Customer_Name.Trim(), Searching) || EF.Functions.Like(res.Customer_Code.Trim(), Searching)  ); //|| Search.Contains(res.Customer_Name)

            }
            return await query.ToListAsync();
           // return await _applicationDbContext.customerMasters.ToListAsync();
        }

        public async Task<CustomerMaster> AddCustomerMaster(CustomerMaster customerMaster)
        {
             
                var result = await _applicationDbContext.customerMasters.AddAsync(customerMaster);
                await _applicationDbContext.SaveChangesAsync();
                return result.Entity;             

        }
        public async Task<CustomerMaster> UpdateCustomerMaster(CustomerMaster customerMaster,int id)
        {
            customerMaster._Id = id;
            var result =   _applicationDbContext.customerMasters.Update(customerMaster);
            await _applicationDbContext.SaveChangesAsync();
            return  result.Entity;
        }
        public async Task<IEnumerable<GST_Percent_Master>> GetGST_Percent_Masters() 
        {
            var result = await _applicationDbContext.GST_Percent_Master.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<State_Master>> GetState_Masters()
        {
            var result = await _applicationDbContext.State_Master.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Cost_Centre>> GetCost_Centres()
        {
            var result = await _applicationDbContext.Cost_Centre.ToListAsync();

            return result;
        }
        public async Task<IEnumerable<Country_Master>> GetCountry_Masters()
        {
            var result = await _applicationDbContext.Country_Master.ToListAsync();
            return result;

        }
        public async Task<CustomerMaster> DeleteCustomerMaster(int Id)
        {

            var result = _applicationDbContext.customerMasters.FirstOrDefault(res => res._Id == Id);
            _applicationDbContext.customerMasters.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
            return result;

        }
        //public async Task<IEnumerable<Consignee_Type_Master>> GetConsignee_Type_Masters()
        //{
        //    return await _applicationDbContext.Consignee_Type_Master.ToListAsync();
        //}

    }
}
