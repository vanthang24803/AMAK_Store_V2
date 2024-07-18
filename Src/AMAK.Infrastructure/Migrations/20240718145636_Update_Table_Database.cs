using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Table_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductVouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVoucher",
                table: "ProductVoucher");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "ProductVoucher");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "ProductVoucher");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "ProductVoucher");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "ProductVoucher");

            migrationBuilder.DropColumn(
                name: "IsExpire",
                table: "ProductVoucher");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductVoucher");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductVoucher");

            migrationBuilder.DropColumn(
                name: "ShelfLife",
                table: "ProductVoucher");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "ProductVoucher");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductVoucher",
                newName: "VoucherId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductVoucher",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVoucher",
                table: "ProductVoucher",
                columns: new[] { "ProductId", "VoucherId" });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsExpire = table.Column<bool>(type: "boolean", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    Discount = table.Column<int>(type: "integer", nullable: false),
                    ShelfLife = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVoucher_VoucherId",
                table: "ProductVoucher",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVoucher_Products_ProductId",
                table: "ProductVoucher",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVoucher_Vouchers_VoucherId",
                table: "ProductVoucher",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVoucher_Products_ProductId",
                table: "ProductVoucher");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVoucher_Vouchers_VoucherId",
                table: "ProductVoucher");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVoucher",
                table: "ProductVoucher");

            migrationBuilder.DropIndex(
                name: "IX_ProductVoucher_VoucherId",
                table: "ProductVoucher");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductVoucher");

            migrationBuilder.RenameColumn(
                name: "VoucherId",
                table: "ProductVoucher",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ProductVoucher",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "ProductVoucher",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "ProductVoucher",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "ProductVoucher",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsExpire",
                table: "ProductVoucher",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductVoucher",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductVoucher",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShelfLife",
                table: "ProductVoucher",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "ProductVoucher",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVoucher",
                table: "ProductVoucher",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductVouchers",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    VoucherId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVouchers", x => new { x.ProductId, x.VoucherId });
                    table.ForeignKey(
                        name: "FK_ProductVouchers_ProductVoucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "ProductVoucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVouchers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVouchers_VoucherId",
                table: "ProductVouchers",
                column: "VoucherId");
        }
    }
}
