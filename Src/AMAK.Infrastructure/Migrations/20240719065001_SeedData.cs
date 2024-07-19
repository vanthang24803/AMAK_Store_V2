using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5199c1b9-a251-4a75-b895-ca45478e34c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a83fe9da-b518-4ee3-ac75-78bddc6d5059");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eec921cc-be30-4491-9426-f8e014f4a225");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "063695b7-fab4-4ffe-a82c-4dc7f3a137b9", "b966621e-bba6-4a61-b246-e35075c612d5", "Manager", "manager" },
                    { "2d625ae2-6ce8-4268-8aba-52b945a96ecf", "ce1ab039-d7e4-488d-b640-fc6137b3f27a", "Customer", "customer" },
                    { "cef4d931-76df-4102-913f-d8aa7decc66c", "c14070bc-c209-43b1-bf2f-cae89ae070e0", "Admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "063695b7-fab4-4ffe-a82c-4dc7f3a137b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d625ae2-6ce8-4268-8aba-52b945a96ecf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cef4d931-76df-4102-913f-d8aa7decc66c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5199c1b9-a251-4a75-b895-ca45478e34c7", null, "Manager", null },
                    { "a83fe9da-b518-4ee3-ac75-78bddc6d5059", null, "Admin", null },
                    { "eec921cc-be30-4491-9426-f8e014f4a225", null, "Customer", null }
                });
        }
    }
}
