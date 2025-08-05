using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDB.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusOrders_Status_Statusid",
                table: "StatusOrders");

            migrationBuilder.DropColumn(
                name: "StatuId",
                table: "StatusOrders");

            migrationBuilder.RenameColumn(
                name: "Statusid",
                table: "StatusOrders",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_StatusOrders_Statusid",
                table: "StatusOrders",
                newName: "IX_StatusOrders_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusOrders_Status_StatusId",
                table: "StatusOrders",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusOrders_Status_StatusId",
                table: "StatusOrders");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "StatusOrders",
                newName: "Statusid");

            migrationBuilder.RenameIndex(
                name: "IX_StatusOrders_StatusId",
                table: "StatusOrders",
                newName: "IX_StatusOrders_Statusid");

            migrationBuilder.AddColumn<Guid>(
                name: "StatuId",
                table: "StatusOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_StatusOrders_Status_Statusid",
                table: "StatusOrders",
                column: "Statusid",
                principalTable: "Status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
