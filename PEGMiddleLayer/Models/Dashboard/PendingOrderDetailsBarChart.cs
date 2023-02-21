using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Models.Dashboard
{
    public class PendingOrderDetailsBarChart
    {

        public string MonthName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double ButterFlyValve { get; set; }
        public double Actuator { get; set; }
        public double GGCValve { get; set; }
        public double BallValve { get; set; }
        //public int Month { get; set; }
        //public double Order_Value { get; set; }
        //public int Order_Qty { get; set; }
        public string Company_Code { get; set; }
        //public string Order_Type { get; set; }
        //public string ProductName { get; set; }

    }
}
