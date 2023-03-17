using PEGMiddleLayer.Models.Sales.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Sales.Masters
{
  public  interface ICustomerMasterRepository
    {
        Task<IEnumerable<CustomerMaster>> getCustomerMasters(string Search);
        Task<CustomerMaster> AddCustomerMaster(CustomerMaster customerMaster);
        Task<CustomerMaster> UpdateCustomerMaster(CustomerMaster customerMaster,int id);
        Task<CustomerMaster> DeleteCustomerMaster(int Id);
        //  Task<IEnumerable<Consignee_Type_Master>> GetConsignee_Type_Masters();
        Task<IEnumerable<GST_Percent_Master>> GetGST_Percent_Masters();

        Task<IEnumerable<State_Master>> GetState_Masters();

        Task<IEnumerable<Cost_Centre>> GetCost_Centres();
        Task<IEnumerable<Country_Master>> GetCountry_Masters();
    }
}
