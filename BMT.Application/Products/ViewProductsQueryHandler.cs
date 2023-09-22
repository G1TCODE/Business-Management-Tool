using BMT.Application.Abstractions.DataLink;
using BMT.Application.Abstractions.Messaging;
using BMT.Application.Stores;
using BMT.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using BMT.Application.Orders.SearchOrders;

namespace BMT.Application.Products
{
    public sealed class ViewProductsQueryHandler
        : IQueryHandler<ViewProductsQuery, IReadOnlyList<ProductResponse>>
    {
        private readonly ISqlConnectionFactory _dbConnectionFactory;

        public ViewProductsQueryHandler(ISqlConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<Result<IReadOnlyList<ProductResponse>>> Handle(
            ViewProductsQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = """
            SELECT
                id AS Id,
                Name_ThisName AS Name,
                Category AS Category,
                UnitPrice_Value AS UnitPrice_Value,
                InStock AS InStock,
                Rating AS Rating
            FROM products
            """;
            var product = await connection.QueryAsync<ProductResponse>(
                sql,
                new
                {
                    request.managerId
                });

            return product.ToList();
        }
    }
}




//var store = await connection
//    .QueryAsync<StoreResponse, StoreAddressResponse, StoreResponse>(
//        sql,
//        (store, address) =>
//        {
//            store.AddressResponse = address;

//            return store;
//        },
//        new
//        {
//            request.managerId
//        },
//        splitOn: "Country");

//return store.ToList();
