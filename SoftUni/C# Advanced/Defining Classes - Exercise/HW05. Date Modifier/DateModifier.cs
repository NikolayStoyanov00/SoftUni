using System;
using System.Collections.Generic;
using System.Text;

namespace HW05._Date_Modifier
{
    public class DateModifier
    {
        public double CalculatesTheDifference(string startDate, string endDate)
        {
            string[] startDateSplitted = startDate.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] endDateSplitted = endDate.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int startYear = int.Parse(startDateSplitted[0]);
            int startMonth = int.Parse(startDateSplitted[1]);
            int startDay = int.Parse(startDateSplitted[2]);

            int endYear = int.Parse(endDateSplitted[0]);
            int endMonth = int.Parse(endDateSplitted[1]);
            int endDay = int.Parse(endDateSplitted[2]);
            DateTime startDateTime = new DateTime(startYear, startMonth, startDay);
            DateTime endDateTime = new DateTime(endYear, endMonth, endDay);

            return (endDateTime - startDateTime).TotalDays;
        }
    }
}
