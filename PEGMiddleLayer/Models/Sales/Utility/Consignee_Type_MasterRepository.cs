using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Sales.Utility
{
    public class Consignee_Type_MasterRepository : IConsignee_Type_MasterRepository
    {
        private readonly CompanyDbContext _companyDbContext;
        public Consignee_Type_MasterRepository(CompanyDbContext companyDbContext )
        {
            _companyDbContext = companyDbContext;
        }
        public async Task<IEnumerable<Consignee_Type_Master>> GetConsignee_Type_Master()
        {
            //var consigneeList = await _companyDbContext.consignee_Type_Masters.ToListAsync();
            return await _companyDbContext.consignee_Type_Master.ToListAsync();
        }
    }
}
