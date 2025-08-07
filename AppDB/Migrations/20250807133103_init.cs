using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppDB.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Combos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComboName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboDetails",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComboID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_ComboDetails_Combos_ComboID",
                        column: x => x.ComboID,
                        principalTable: "Combos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComboID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Combos_ComboID",
                        column: x => x.ComboID,
                        principalTable: "Combos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "StatusOrders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusOrders", x => x.id);
                    table.ForeignKey(
                        name: "FK_StatusOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatusOrders_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "id", "RoleName", "createdById", "createdByName", "createdDate", "updatedById", "updatedByName", "updatedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Admin", new Guid("11111111-1111-1111-1111-111111111111"), "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Staff", new Guid("11111111-1111-1111-1111-111111111111"), "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Customer", new Guid("11111111-1111-1111-1111-111111111111"), "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "id", "Description", "Name", "createdById", "createdByName", "createdDate", "updatedById", "updatedByName", "updatedDate" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, "Đã giao", new Guid("11111111-1111-1111-1111-111111111111"), "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), null, "Chưa giao", new Guid("11111111-1111-1111-1111-111111111111"), "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), null, "Đang giao", new Guid("11111111-1111-1111-1111-111111111111"), "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "Address", "Email", "FullName", "Password", "Phone", "RoleId", "Username", "createdById", "createdByName", "createdDate", "updatedById", "updatedByName", "updatedDate" },
                values: new object[,]
                {
                    { new Guid("642da3c9-5c2a-4d5d-b03b-f2118baa61fe"), "Admin Address", "admin@example.com", "Administrator", "73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=", "0123456789", new Guid("11111111-1111-1111-1111-111111111111"), "admin", new Guid("642da3c9-5c2a-4d5d-b03b-f2118baa61fe"), "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null },
                    { new Guid("b3c8cdf2-9b76-4a19-b008-10f70fb3467f"), "Staff Address", "staff@example.com", "Staff", "73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=", "0123256789", new Guid("11111111-1111-1111-1111-111111111111"), "staff", new Guid("642da3c9-5c2a-4d5d-b03b-f2118baa61fe"), "System", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboDetails_ComboID",
                table: "ComboDetails",
                column: "ComboID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboDetails_ProductID",
                table: "ComboDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ComboID",
                table: "OrderDetails",
                column: "ComboID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductID",
                table: "OrderDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StaffID",
                table: "Orders",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusOrders_OrderId",
                table: "StatusOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusOrders_StatusId",
                table: "StatusOrders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComboDetails");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "StatusOrders");

            migrationBuilder.DropTable(
                name: "Combos");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
