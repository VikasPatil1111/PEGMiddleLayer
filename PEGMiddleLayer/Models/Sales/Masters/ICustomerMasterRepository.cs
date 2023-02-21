using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Sales.Masters
{
  public  interface ICustomerMasterRepository
    {
        Task<IEnumerable<CustomerMaster>> getCustomerMasters();
        Task<CustomerMaster> AddCustomerMaster(CustomerMaster customerMaster);
    }
}
