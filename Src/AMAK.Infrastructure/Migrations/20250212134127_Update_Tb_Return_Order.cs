using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Tb_Return_Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("1be335ab-5ed5-495d-8be1-d24b63887732"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("7007b12b-0b6b-41f0-9839-064954580593"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("72a0fc31-4eb3-4660-829d-59db1b8b025f"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("739e0fb8-4cf4-4105-9491-99aa50dad733"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("7e9f2880-0376-4540-9f22-1cfbb0e1139f"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("aa268330-af15-4605-9050-ebddd8d55a40"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("b521bf83-6898-4859-b42e-cca0c875dc23"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("32477bea-d6e6-4b01-8df8-4260ab14b796"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("521d16a7-8901-4398-8298-712c0f63ef35"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("56e63cd9-4d4f-40b0-ac95-36a288a861de"));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "OrderDetail",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderReturnOrderId",
                table: "OrderDetail",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderReturns",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsAccepted = table.Column<bool>(type: "boolean", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConfirmAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SuccessAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderReturns", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderReturns_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("2216d586-b229-4921-b147-7a1c5c77c71c"), new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(568), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(571), null },
                    { new Guid("33774ce0-7ac9-42a4-acaa-1bcf58b4de62"), new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(621), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4o", new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(622), null },
                    { new Guid("60776ca2-c974-47fe-a5c3-d976b5168394"), new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(615), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(615), null },
                    { new Guid("6f21c9e8-5524-49f7-bb1d-9c23061e944d"), new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(617), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(618), null },
                    { new Guid("962b0326-38e3-4380-8678-b84e71de558d"), new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(619), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(620), null },
                    { new Guid("a6a7a7ef-da77-4f80-8575-4c9d09c27b41"), new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(610), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(610), null },
                    { new Guid("ab6f709b-7b75-43e0-90a8-17797bf47d12"), new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(613), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(613), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("646532a0-3732-4a09-a046-33deb79e2a3c"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(939), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(939) },
                    { new Guid("80ea8e23-82f9-4c55-801b-e0e6a5d70e67"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(931), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(932) },
                    { new Guid("beea1ea4-3eae-495b-8588-ba993393d88e"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(936), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2025, 2, 12, 13, 41, 26, 395, DateTimeKind.Utc).AddTicks(937) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderReturnOrderId",
                table: "OrderDetail",
                column: "OrderReturnOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrderReturns_OrderReturnOrderId",
                table: "OrderDetail",
                column: "OrderReturnOrderId",
                principalTable: "OrderReturns",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrderReturns_OrderReturnOrderId",
                table: "OrderDetail");

            migrationBuilder.DropTable(
                name: "OrderReturns");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_OrderReturnOrderId",
                table: "OrderDetail");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("2216d586-b229-4921-b147-7a1c5c77c71c"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("33774ce0-7ac9-42a4-acaa-1bcf58b4de62"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("60776ca2-c974-47fe-a5c3-d976b5168394"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("6f21c9e8-5524-49f7-bb1d-9c23061e944d"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("962b0326-38e3-4380-8678-b84e71de558d"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("a6a7a7ef-da77-4f80-8575-4c9d09c27b41"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("ab6f709b-7b75-43e0-90a8-17797bf47d12"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("646532a0-3732-4a09-a046-33deb79e2a3c"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("80ea8e23-82f9-4c55-801b-e0e6a5d70e67"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("beea1ea4-3eae-495b-8588-ba993393d88e"));

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "OrderReturnOrderId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Point",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("1be335ab-5ed5-495d-8be1-d24b63887732"), new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4235), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4236), null },
                    { new Guid("7007b12b-0b6b-41f0-9839-064954580593"), new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4238), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4238), null },
                    { new Guid("72a0fc31-4eb3-4660-829d-59db1b8b025f"), new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4260), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4o", new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4260), null },
                    { new Guid("739e0fb8-4cf4-4105-9491-99aa50dad733"), new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4258), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4258), null },
                    { new Guid("7e9f2880-0376-4540-9f22-1cfbb0e1139f"), new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4192), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4195), null },
                    { new Guid("aa268330-af15-4605-9050-ebddd8d55a40"), new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4256), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4256), null },
                    { new Guid("b521bf83-6898-4859-b42e-cca0c875dc23"), new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4254), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4254), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("32477bea-d6e6-4b01-8df8-4260ab14b796"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4643), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4643) },
                    { new Guid("521d16a7-8901-4398-8298-712c0f63ef35"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4640), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4641) },
                    { new Guid("56e63cd9-4d4f-40b0-ac95-36a288a861de"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4635), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2025, 2, 12, 6, 14, 15, 939, DateTimeKind.Utc).AddTicks(4635) }
                });
        }
    }
}
