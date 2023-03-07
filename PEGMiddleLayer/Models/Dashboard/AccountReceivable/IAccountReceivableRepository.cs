using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard.AccountReceivable
{
   public interface IAccountReceivableRepository
    {
        public  Task<IEnumerable<tblMIS_AccRec_Details>> TblMIS_AccRec_Details(string Company,string CustomerCode,int DueFilter, int OverDueFilter);
        public Task<IEnumerable<tblMIS_AccRec_Details>> TblMIS_AccRec_Details(string Company,string Branch,string CustomerCode, int DueFilter, int OverDueFilter);
        public Task<IEnumerable<tblBranch_User>> tblBranch_Users(string UserID);
    }
}
