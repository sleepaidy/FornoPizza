using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FornoPizza.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Pizzas_PizzaId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderToppings_Orders_OrderId",
                table: "OrderToppings");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PizzaId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Dough",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PizzaNameInOrder",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PizzaPriceInOrder",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderToppings",
                newName: "OrderItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderToppings_OrderId",
                table: "OrderToppings",
                newName: "IX_OrderToppings_OrderItemId");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PizzaId = table.Column<int>(type: "int", nullable: false),
                    PizzaNameInOrder = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PizzaPriceInOrder = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Dough = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    LinePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_PizzaId",
                table: "OrderItems",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderToppings_OrderItems_OrderItemId",
                table: "OrderToppings",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderToppings_OrderItems_OrderItemId",
                table: "OrderToppings");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                table: "OrderToppings",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderToppings_OrderItemId",
                table: "OrderToppings",
                newName: "IX_OrderToppings_OrderId");

            migrationBuilder.AddColumn<int>(
                name: "Dough",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PizzaId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PizzaNameInOrder",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PizzaPriceInOrder",
                table: "Orders",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PizzaId",
                table: "Orders",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Pizzas_PizzaId",
                table: "Orders",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderToppings_Orders_OrderId",
                table: "OrderToppings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
