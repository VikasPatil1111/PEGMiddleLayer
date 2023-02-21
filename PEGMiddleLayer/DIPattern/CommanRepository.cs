using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.DIPattern
{
    public class CommanRepository : ICommanRepository
    {
        public int CalculateFinancialMonthDifference(DateTime FromDate, DateTime ToDate, int Year)
        {
            int setMonth = 0;

            if (Year <= System.DateTime.Now.Year - 2)
            {
                setMonth = 12;
            }
            else if (Year == System.DateTime.Now.Year)
            {
                setMonth = ((Year - FromDate.Year) * 12) + System.DateTime.Now.Month - FromDate.Month+1;
            }
            else if (Year == System.DateTime.Now.Year - 1)
            {
                setMonth = ((Year - FromDate.Year) * 12) + System.DateTime.Now.Month - FromDate.Month+1 + 12;
            }
            return setMonth;
        }

        public string getMonthName(int Month)
        {
            string MonthName = "";

            
            switch (Month)
            {
                case 1:
                    MonthName = "Jan";
                    break;
                case 2:
                    MonthName = "Feb";
                    break;
                case 3:
                    MonthName = "Mar";
                    break;
                case 4:
                    MonthName = "Apr";
                    break;
                case 5:
                    MonthName = "May";
                    break;
                case 6:
                    MonthName = "Jun";
                    break;
                case 7:
                    MonthName = "Jul";
                    break;
                case 8:
                    MonthName = "Aug";
                    break;
                case 9:
                    MonthName = "Sep";
                    break;
                case 10:
                    MonthName = "Oct";
                    break;
                case 11:
                    MonthName = "Nov";
                    break;
                
                case 12:
                    MonthName = "Dec";
                    break;
                default: 
                    MonthName = "Error";
                    break;
            }

            return MonthName;
              
        }
    }
}
