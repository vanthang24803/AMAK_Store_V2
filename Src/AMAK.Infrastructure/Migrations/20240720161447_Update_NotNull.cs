using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_NotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f0226c3-79f2-4b86-a633-e03d24fce3d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3812289-dc8c-4e4c-a51a-f255d0cdbab4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f202be9c-fa4e-42da-b7c1-a86689a7f3fd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5f0226c3-79f2-4b86-a633-e03d24fce3d1", null, "CUSTOMER", null },
                    { "a3812289-dc8c-4e4c-a51a-f255d0cdbab4", null, "MANAGER", null },
                    { "f202be9c-fa4e-42da-b7c1-a86689a7f3fd", null, "ADMIN", null }
                });
        }
    }
}
