using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FornoPizza.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstPromo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "DiscountValue", "IsActive" },
                values: new object[] { 1, "WELCOME10", 100m, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
