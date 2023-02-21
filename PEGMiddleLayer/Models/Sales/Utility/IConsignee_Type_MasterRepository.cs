using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Sales.Utility
{
   public interface IConsignee_Type_MasterRepository
    {
        Task<IEnumerable<Consignee_Type_Master>> GetConsignee_Type_Master();
    }
}
