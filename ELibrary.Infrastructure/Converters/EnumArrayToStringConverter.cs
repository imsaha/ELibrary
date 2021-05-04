using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastructure.Converters
{
    internal class EnumArrayToStringConverter<T> : ValueConverter<T[], string> where T : Enum
    {
        public EnumArrayToStringConverter(ConverterMappingHints mappingHints = null) : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        protected static Expression<Func<T[], string>> convertToProviderExpression = x => string.Join(",", x.Select(s => s.ToString()));
        protected static Expression<Func<string, T[]>> convertFromProviderExpression = v => v.Split(',', StringSplitOptions.None).Select(x => (T)Enum.Parse(typeof(T), x)).ToArray();
    }
}
