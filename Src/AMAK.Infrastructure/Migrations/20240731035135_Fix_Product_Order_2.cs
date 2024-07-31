using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Product_Order_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "ProductOrder",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductOrder",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "ProductOrder",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Sale",
                table: "ProductOrder",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductOrder");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductOrder");

            migrationBuilder.DropColumn(
                name: "Sale",
                table: "ProductOrder");

            migrationBuilder.AlterColumn<long>(
                name: "Quantity",
                table: "ProductOrder",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
