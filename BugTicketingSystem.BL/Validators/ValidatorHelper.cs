using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL
{
   public static class ValidatorHelper
    {
        public static bool IsValidEnumValues<TEnum>(string enumValues) where TEnum : struct, Enum
        {
            var values = enumValues.Split(',', StringSplitOptions.RemoveEmptyEntries);

            return values.All(value =>
                Enum.TryParse(typeof(TEnum), value.Trim(), ignoreCase: true, out var parsedValue)
                && Enum.IsDefined(typeof(TEnum), parsedValue)
            );
        }
    }
}
