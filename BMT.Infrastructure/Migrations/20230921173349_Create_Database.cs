using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "managers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    manager_scope = table.Column<int>(type: "int", nullable: false),
                    store_or_warehouse_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_managers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name_ThisName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice_Value = table.Column<int>(type: "int", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shoppingcarts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreOrWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShoppingCartTotal_Value = table.Column<int>(type: "int", nullable: true),
                    ShoppingCartMaxValue = table.Column<int>(type: "int", nullable: false),
                    TheShoppingCart_Capacity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingcarts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    ItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price_Value = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoreId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_items_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_items_stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "stores",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_items_stores_StoreId1",
                        column: x => x.StoreId1,
                        principalTable: "stores",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_or_warehouse_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    manager_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order_status = table.Column<int>(type: "int", nullable: false),
                    date_order_placed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total_cost = table.Column<int>(type: "int", nullable: false),
                    order_type = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "stores",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_items_ProductId",
                table: "items",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_items_StoreId",
                table: "items",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_items_StoreId1",
                table: "items",
                column: "StoreId1");

            migrationBuilder.CreateIndex(
                name: "IX_orders_StoreId",
                table: "orders",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "managers");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "shoppingcarts");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "stores");
        }
    }
}
