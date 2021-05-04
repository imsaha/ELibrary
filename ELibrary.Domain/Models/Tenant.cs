using System.Collections.Generic;

namespace ELibrary.Domain.Models
{
    public class Tenant : BaseModel
    {
        public Tenant()
        {
            Rents = new HashSet<Rent>();
        }
        public string Name { get; set; }
        public string MobileNumber { get; set; }

        public ICollection<Rent> Rents { get; }
    }
}
