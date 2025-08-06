using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppDB.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Combos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "id", "Description", "Name", "createdById", "createdByName", "createdDate", "updatedById", "updatedByName", "updatedDate" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, "Đã giao", new Guid("11111111-1111-1111-1111-111111111111"), "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), null, "Chưa giao", new Guid("11111111-1111-1111-1111-111111111111"), "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), null, "Đang giao", new Guid("11111111-1111-1111-1111-111111111111"), "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Combos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
