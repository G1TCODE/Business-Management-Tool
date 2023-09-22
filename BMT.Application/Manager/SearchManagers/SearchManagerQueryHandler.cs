using BMT.Application.Abstractions.Messaging;
using BMT.Application.Orders.SearchOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using BMT.Domain.Abstractions;
using BMT.Application.Abstractions.DataLink;

namespace BMT.Application.Manager.SearchManagers
{
    public sealed class SearchManagersQueryHandler
        : IQueryHandler<SearchManagersQuery, IReadOnlyList<ManagerResponse>>
    {
        private readonly ISqlConnectionFactory _dbConnectionFactory;

        public SearchManagersQueryHandler(ISqlConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Result<IReadOnlyList<ManagerResponse>>> Handle(
            SearchManagersQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = """
                SELECT
                    id AS Id,
                    name AS Name,
                    level AS Level,
                    password AS Password,
                    email AS Email,
                    manager_scope AS ManagerScope,
                    store_or_warehouse_id AS StoreOrWarehouseId
                FROM managers
                ORDER BY level DESC
                """;

            var managers = await connection.QueryAsync<ManagerResponse>(
                sql,
                new
                {
                    request.managerId
                });

            return managers.ToList();
        }
    }
}
