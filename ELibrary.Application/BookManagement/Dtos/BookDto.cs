using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Application.BookManagement.Dtos
{
    public class BookDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsProhibited { get; set; }
        public double RentFee { get; set; }
        public long Id { get; set; }
    }
}
