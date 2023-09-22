using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMT.Application.Abstractions.DataLink
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
