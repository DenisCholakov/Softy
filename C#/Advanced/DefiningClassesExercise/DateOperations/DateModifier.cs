using System;
using System.Collections.Generic;
using System.Text;

namespace DateOperations
{
    public class DateModifier
    {
        public static int GetDayDifference(string startDateString, string endDateString)
        {
            DateTime startDate = DateTime.Parse(startDateString);
            DateTime endDate = DateTime.Parse(endDateString);

            return (int)Math.Abs((startDate - endDate).TotalDays);
        }
    }
}
