using System;
using System.Linq;

namespace ELibrary.Domain.Models
{
    public class Rent : BaseModel
    {
        public long BookId { get; set; }
        public Book Book { get; set; }

        public long TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public long OperationCountryId { get; set; }
        public OperationCountry OperationCountry { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime ScheduledReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }

        public static double GetLateFeeCalculate(DateTime startDate, DateTime endDate, OperationCountry operationCountry)
        {
            var businessDaysCount = getBusinessDaysCount(startDate, endDate, operationCountry);
            return operationCountry.LateFees * businessDaysCount;
        }


        private static int getBusinessDaysCount(DateTime startDate, DateTime endDate, OperationCountry operationCountry)
        {
            if (endDate < startDate)
                throw new ArgumentOutOfRangeException(nameof(startDate));

            var weekDays = operationCountry.WeekDays;
            var holidays = operationCountry.Holidays.Select(s => s.HolidayDate).ToArray();

            var allDates = Enumerable.Range(0, 1 + (endDate - startDate).Days)
                .Select(count => startDate.AddDays(count)).ToArray();

            var countOfBusinesDays = allDates
                .Where(x => !weekDays.Contains(x.DayOfWeek) && !holidays.Contains(x))
                .Count();

            return countOfBusinesDays;
        }
    }
}
