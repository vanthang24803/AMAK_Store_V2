using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Notfication_Table_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageUsers_AspNetUsers_UsersId",
                table: "MessageUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageUsers_Notifications_NotificationsId",
                table: "MessageUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageUsers",
                table: "MessageUsers");

            migrationBuilder.DropIndex(
                name: "IX_MessageUsers_UsersId",
                table: "MessageUsers");

            migrationBuilder.DropColumn(
                name: "NotificationsId",
                table: "MessageUsers");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "MessageUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageUsers",
                table: "MessageUsers",
                columns: new[] { "UserId", "NonfictionId" });

            migrationBuilder.CreateIndex(
                name: "IX_MessageUsers_NonfictionId",
                table: "MessageUsers",
                column: "NonfictionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageUsers_AspNetUsers_UserId",
                table: "MessageUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageUsers_Notifications_NonfictionId",
                table: "MessageUsers",
                column: "NonfictionId",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageUsers_AspNetUsers_UserId",
                table: "MessageUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageUsers_Notifications_NonfictionId",
                table: "MessageUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageUsers",
                table: "MessageUsers");

            migrationBuilder.DropIndex(
                name: "IX_MessageUsers_NonfictionId",
                table: "MessageUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "NotificationsId",
                table: "MessageUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "MessageUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageUsers",
                table: "MessageUsers",
                columns: new[] { "NotificationsId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_MessageUsers_UsersId",
                table: "MessageUsers",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageUsers_AspNetUsers_UsersId",
                table: "MessageUsers",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageUsers_Notifications_NotificationsId",
                table: "MessageUsers",
                column: "NotificationsId",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
