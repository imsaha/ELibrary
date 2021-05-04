using System;
using System.Collections.Generic;

namespace ELibrary.Domain.Models
{
    public class OperationCountry : BaseModel
    {
        public OperationCountry()
        {
            Holidays = new HashSet<OperationCountryHoliday>();
            SupportedLanguages = new HashSet<OperationCountrySupportedLanguage>();
        }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Currency { get; set; }
        public double LateFees { get; set; }
        public DayOfWeek[] WeekDays { get; set; }
        public ICollection<OperationCountryHoliday> Holidays { get; }
        public ICollection<OperationCountrySupportedLanguage> SupportedLanguages { get; }
    }

    public class OperationCountryHoliday : BaseModel
    {
        public long OperationCountryId { get; set; }
        public OperationCountry OperationCountry { get; set; }
        public string OccationTitle { get; set; }
        public DateTime HolidayDate { get; set; }
    }


    public class OperationCountrySupportedLanguage
    {
        public long OperationCountryId { get; set; }
        public OperationCountry OperationCountry { get; set; }

        public long SupportedLanaguageId { get; set; }
        public SupportedLanguage SupportedLanguage { get; set; }
    }

    public class SupportedLanguage : BaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
