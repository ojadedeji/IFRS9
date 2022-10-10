using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.Common.Utils
{
    public static class Helpers
    {
        public static long? GetNullableValue(long? value)
        {
            return value != 0 ? value : null;
        }

        public static long? GetZeroIfNull(long? value)
        {
            return value == null ? 0 : value;
        }

        public static string ConvertIntToString(int value, string numberOfZeros)
        {
            return value.ToString(numberOfZeros);
        }
    }
}
