using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Application.RentManagement.Dtos
{
    public class ActiveRentDto
    {
        public long Id { get; set; }
        public string BookTitle { get; set; }
        public string TenantName { get; set; }
        public string TenantMobile { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ScheduledReturnDate { get; set; }
    }
}
