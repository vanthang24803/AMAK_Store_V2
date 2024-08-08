using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_DB_Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Addresses",
                newName: "AddressName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Addresses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Addresses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberPhone",
                table: "Addresses",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "NumberPhone",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "AddressName",
                table: "Addresses",
                newName: "Name");
        }
    }
}
