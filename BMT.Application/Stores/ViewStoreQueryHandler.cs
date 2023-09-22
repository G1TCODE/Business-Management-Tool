using BMT.Application.Abstractions.DataLink;
using BMT.Application.Abstractions.Messaging;
using BMT.Application.Manager.SearchManagers;
using BMT.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using BMT.Domain.Orders;
using BMT.Application.Orders.SearchOrders;

namespace BMT.Application.Stores
{
    public sealed class ViewStoresQueryHandler
        : IQueryHandler<ViewStoresQuery, IReadOnlyList<StoreResponse>>
    {
        //private static readonly int[] ActiveOrderStatuses =
        //{
        //    (int)OrderStatus.Approved, // 1
        //    (int)OrderStatus.Shipped, // 2
        //    (int)OrderStatus.DeliveredToCentralWarehouse, // 3
        //    (int)OrderStatus.EnRouteToStore, // 4
        //};

        private readonly ISqlConnectionFactory _dbConnectionFactory;

        public ViewStoresQueryHandler(ISqlConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Result<IReadOnlyList<StoreResponse>>> Handle(
            ViewStoresQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = """
            SELECT
                id AS Id,
                name AS Name,
                ManagerId AS ManagerID,
                address_country AS Country,
                address_street AS Street,
                address_city AS City,
                address_state AS State,
                address_zipcode AS ZipCode
            FROM stores
            """;

            var store = await connection
                .QueryAsync<StoreResponse, StoreAddressResponse, StoreResponse>(
                    sql,
                    (store, address) =>
                    {
                        store.Address = address;

                        return store;
                    },
                    new
                    {
                        request.managerId
                    },
                    splitOn: "Country");

            return store.ToList();
        }
    }
}