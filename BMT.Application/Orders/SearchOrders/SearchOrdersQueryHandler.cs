using BMT.Application.Abstractions.DataLink;
using BMT.Application.Abstractions.Messaging;
using BMT.Domain.Abstractions;
using BMT.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BMT.Application.Orders.SearchOrders
{
    internal sealed class SearchOrdersQueryHandler 
        : IQueryHandler<SearchOrdersQuery, IReadOnlyList<OrderResponse>>
    {
        private static readonly int[] ActiveOrderStatuses =
        {
            (int)OrderStatus.Approved, // 1
            (int)OrderStatus.Shipped, // 2
            (int)OrderStatus.DeliveredToCentralWarehouse, // 3
            (int)OrderStatus.EnRouteToStore, // 4
        };

        private readonly ISqlConnectionFactory _dbConnectionFactory;

        public SearchOrdersQueryHandler(ISqlConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<OrderResponse>>> Handle(
            SearchOrdersQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = """
                SELECT
                    id AS Id,
                    store_or_warehouse_id AS StoreOrWarehouseID,
                    manager_id AS ManagerId,
                    order_status AS OrderStatus,
                    date_order_placed AS DataOrderPlaced,
                    total_cost AS TotalCost,
                    order_type AS OrderType
                FROM orders
                WHERE order_status IN (1, 2, 3, 4)
                ORDER BY total_cost               
                """;

            var order = await connection.QueryAsync<OrderResponse>(
                sql,
                new
                {
                    request.managerId
                });

            return order.ToList();
        }
    }
}

//WHERE order_status IN (Approved, Shipped, DeliveredToCentralWarehouse, EnRouteToStore)
