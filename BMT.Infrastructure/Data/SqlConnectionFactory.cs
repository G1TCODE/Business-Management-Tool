using BMT.Application.Abstractions.DataLink;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Options;
using BMT.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BMT.Infrastructure.Data
{
    internal sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _sqlConnection;

        public SqlConnectionFactory(string sqlConnectionString)
        {
            _sqlConnection = sqlConnectionString;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_sqlConnection);

            connection.Open();

            return connection;
        }
    }
}
