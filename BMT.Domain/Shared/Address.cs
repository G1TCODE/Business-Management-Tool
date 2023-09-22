using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Domain.Shared
{
    public record Address
       (string State,
        string Country,
        string ZipCode,
        string Street,
        string City);
}
