using PEGMiddleLayer.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Data;

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
        public async Task<IEnumerable<CustomerMaster>> getCustomerMasters()
        {
            return await _applicationDbContext.customerMasters.ToListAsync();
        }

        public async Task<CustomerMaster> AddCustomerMaster(CustomerMaster customerMaster)
        {
             
                var result = await _applicationDbContext.customerMasters.AddAsync(customerMaster);
                await _applicationDbContext.SaveChangesAsync();
                return result.Entity;             

        }
    }
}
