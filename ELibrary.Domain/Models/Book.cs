using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.Models
{

    public class Book : BaseModel
    {
        public Book()
        {
            Rents = new HashSet<Rent>();
            Fees = new HashSet<CountryBookRentFee>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsProhibited { get; set; }

        public ICollection<CountryBookRentFee> Fees { get; }
        public ICollection<Rent> Rents { get; }
    }

    public class CountryBookRentFee
    {
        public long OperationCountryId { get; set; }
        public OperationCountry OperationCountry { get; set; }

        public long BookId { get; set; }
        public Book Book { get; set; }

        public double RentFee { get; set; }
    }
}
