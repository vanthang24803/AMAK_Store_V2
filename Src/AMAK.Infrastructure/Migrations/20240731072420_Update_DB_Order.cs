using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_DB_Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumberPhone",
                table: "Orders",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberPhone",
                table: "Orders");
        }
    }
}
