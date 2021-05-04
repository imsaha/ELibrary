using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELibrary
{
    public static class CultureHelper
    {
        public static string CurrentCultureCode
        {
            get
            {
                var cultureName = Thread.CurrentThread.CurrentUICulture.Name;
                return cultureName;
            }
        }

        public static bool IsRightToLeft
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft;
            }
        }
    }
}
