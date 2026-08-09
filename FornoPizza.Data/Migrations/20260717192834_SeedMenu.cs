using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FornoPizza.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pizzas",
                columns: new[] { "Id", "ImageUrl", "Ingredients", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "https://images.unsplash.com/photo-1574071318508-1cdbab80d002?auto=format&fit=crop&w=800&q=80", "Томаты, моцарелла, базилик", true, "Маргарита", 490m },
                    { 2, "https://images.unsplash.com/photo-1628840042765-356cda07504e?auto=format&fit=crop&w=800&q=80", "Острая колбаса, сыр, томатный соус", true, "Пепперони", 590m },
                    { 3, "https://images.unsplash.com/photo-1593560708920-61dd98c46a4e?auto=format&fit=crop&w=800&q=80", "Шампиньоны, сливки, трюфельное масло", true, "Грибная", 620m },
                    { 4, "https://images.unsplash.com/photo-1548365328-9f68587ace8e?auto=format&fit=crop&w=800&q=80", "Моцарелла, горгонзола, пармезан, чеддер", true, "Четыре сыра", 650m }
                });

            migrationBuilder.InsertData(
                table: "Toppings",
                columns: new[] { "Id", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { 1, true, "Моцарелла", 70m },
                    { 2, true, "Бекон", 90m },
                    { 3, true, "Халапеньо", 50m },
                    { 4, true, "Оливки", 60m },
                    { 5, true, "Грибы", 70m },
                    { 6, true, "Соус барбекю", 40m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pizzas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Toppings",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
