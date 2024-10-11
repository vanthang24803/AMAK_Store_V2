using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMAK.Infrastructure.Migrations {
    /// <inheritdoc />
    public partial class Update_Config_Account_Template_Table_2 : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql("UPDATE \"AccountConfigs\" SET \"Language\" = 0 WHERE \"Language\" IS NULL");

            migrationBuilder.DropColumn(
                name: "IsNotification",
                table: "AccountConfigs");

            migrationBuilder.AlterColumn<int>(
                name: "Timezone",
                table: "AccountConfigs",
                type: "integer",
                nullable: false,
                defaultValue: 7,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.Sql("ALTER TABLE \"AccountConfigs\" ALTER COLUMN \"Language\" TYPE integer USING \"Language\"::integer;");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBan",
                table: "AccountConfigs",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<bool>(
                name: "IsActiveNotification",
                table: "AccountConfigs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "IsActiveNotification",
                table: "AccountConfigs");

            migrationBuilder.AlterColumn<string>(
                name: "Timezone",
                table: "AccountConfigs",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 7);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "AccountConfigs",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsBan",
                table: "AccountConfigs",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNotification",
                table: "AccountConfigs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
