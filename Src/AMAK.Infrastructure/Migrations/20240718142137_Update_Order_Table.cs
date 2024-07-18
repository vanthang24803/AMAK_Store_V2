using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Order_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Introduction",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Sold",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Specifications",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Sale = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    IsActive = table.Column<double>(type: "double precision", nullable: false, defaultValue: 1.0),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Option_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Customer = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Address = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Payment = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Shipping = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Option_ProductId",
                table: "Option",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Option");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Introduction",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Sold",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Specifications",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);
        }
    }
}
