using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary
{
    public static class HelperExtensions
    {

        public static string ToFormatedDate(this DateTime date)
        {
            return date.ToString("dd-MMM-yyyy");
        }

    }
}
