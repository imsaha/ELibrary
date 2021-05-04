using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Application.BookManagement.Dtos
{
    public class BookMiniDto
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public string Currency { get; set; }
        public double Fees { get; set; }

        public string Text => $"{Title} ({Currency} {Fees})";
    }
}
