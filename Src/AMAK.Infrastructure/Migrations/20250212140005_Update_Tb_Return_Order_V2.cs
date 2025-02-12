using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Tb_Return_Order_V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrderReturns_OrderReturnOrderId",
                table: "OrderDetail");

            migrationBuilder.DropTable(
                name: "OrderReturns");

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

            migrationBuilder.RenameColumn(
                name: "OrderReturnOrderId",
                table: "OrderDetail",
                newName: "OrderRefundOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_OrderReturnOrderId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_OrderRefundOrderId");

            migrationBuilder.AddColumn<bool>(
                name: "IsRefund",
                table: "OrderDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "OrderRefunds",
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
                    table.PrimaryKey("PK_OrderRefunds", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderRefunds_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefundTimelines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundTimelines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefundTimelines_OrderRefunds_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderRefunds",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefundTimelines_Orders_OrderId",
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
                    { new Guid("009e00df-c197-4b63-9916-b34ff4ac83e7"), new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3422), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3422), null },
                    { new Guid("28b4cbd6-6812-477c-87a2-3bac6a2bca06"), new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3435), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3435), null },
                    { new Guid("47bbd07d-ea44-43f7-8ba1-82b6c4440270"), new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3420), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3420), null },
                    { new Guid("85ea000b-f90f-4484-9f45-d4398d915cd3"), new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3415), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3415), null },
                    { new Guid("b63299e5-b7aa-445a-aae5-3017d45e1325"), new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3370), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3374), null },
                    { new Guid("cf5469b3-c6fb-4ab8-8792-85da00d52600"), new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3418), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3418), null },
                    { new Guid("d4d084f1-c918-4f66-bdda-d7ad0dfc7122"), new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3437), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4o", new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3438), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("46ce63f7-cbd3-4dfd-b2d5-b5088c8c3e16"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3898), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3898) },
                    { new Guid("826b4327-ced0-4ea1-8634-fb4d006bde28"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3900), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3901) },
                    { new Guid("98ca924e-62e6-4510-abc2-c274c2368ee5"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3892), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2025, 2, 12, 14, 0, 5, 24, DateTimeKind.Utc).AddTicks(3893) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefundTimelines_OrderId",
                table: "RefundTimelines",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrderRefunds_OrderRefundOrderId",
                table: "OrderDetail",
                column: "OrderRefundOrderId",
                principalTable: "OrderRefunds",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_OrderRefunds_OrderRefundOrderId",
                table: "OrderDetail");

            migrationBuilder.DropTable(
                name: "RefundTimelines");

            migrationBuilder.DropTable(
                name: "OrderRefunds");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("009e00df-c197-4b63-9916-b34ff4ac83e7"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("28b4cbd6-6812-477c-87a2-3bac6a2bca06"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("47bbd07d-ea44-43f7-8ba1-82b6c4440270"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("85ea000b-f90f-4484-9f45-d4398d915cd3"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("b63299e5-b7aa-445a-aae5-3017d45e1325"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("cf5469b3-c6fb-4ab8-8792-85da00d52600"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("d4d084f1-c918-4f66-bdda-d7ad0dfc7122"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("46ce63f7-cbd3-4dfd-b2d5-b5088c8c3e16"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("826b4327-ced0-4ea1-8634-fb4d006bde28"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("98ca924e-62e6-4510-abc2-c274c2368ee5"));

            migrationBuilder.DropColumn(
                name: "IsRefund",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "OrderRefundOrderId",
                table: "OrderDetail",
                newName: "OrderReturnOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_OrderRefundOrderId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_OrderReturnOrderId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "OrderDetail",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OrderReturns",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfirmAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsAccepted = table.Column<bool>(type: "boolean", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrderReturns_OrderReturnOrderId",
                table: "OrderDetail",
                column: "OrderReturnOrderId",
                principalTable: "OrderReturns",
                principalColumn: "OrderId");
        }
    }
}
