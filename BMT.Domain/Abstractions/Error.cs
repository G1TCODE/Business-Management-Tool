using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Domain.Abstractions
{
    public record Error(string ErrorLaw, string ErrorName)
    {
        public static Error NoError = new (string.Empty, string.Empty);

        public static Error Null = new("Error.Null",
            "Error Null: No value for this error was specified.");
    }
}
