using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMAK.Infrastructure.Migrations {
    /// <inheritdoc />
    public partial class Update_Cart_Table_v3 : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql("ALTER TABLE \"CartDetails\" ALTER COLUMN \"OptionId\" TYPE uuid USING \"OptionId\"::uuid;");

            migrationBuilder.DropColumn(
                name: "OptionName",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "Sale",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "CartDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "OptionId",
                table: "CartDetails",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_OptionId",
                table: "CartDetails",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Options_OptionId",
                table: "CartDetails",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Options_OptionId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_OptionId",
                table: "CartDetails");

            migrationBuilder.AlterColumn<string>(
                name: "OptionId",
                table: "CartDetails",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "OptionName",
                table: "CartDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "CartDetails",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "CartDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "CartDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sale",
                table: "CartDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "CartDetails",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
