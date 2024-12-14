using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Table_Flash_Sale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("25887a1b-79fe-4e39-9e4e-f1639af7cdef"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("2a7bc9e1-3f4a-4dfb-87c8-58f1cadf5be1"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("741f0aa9-ac83-4bc0-b6a6-db764f69eadc"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("82160df7-b12a-418d-88d3-6584e575c1b4"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("cf70a206-3204-449f-81f7-8fc645841154"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("de331ac5-2840-433f-a35f-97e3759a3b8a"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("fcf74e5a-75f1-41bb-ba13-6f5efdd36f93"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("783385a3-5ff6-47cd-b52b-a3bf648223de"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("8510451b-771d-445f-890f-d6bd70e2a0b2"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("9825d352-e829-4aaa-8334-f1cbe6a14f1c"));

            migrationBuilder.CreateTable(
                name: "FlashSales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StartAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlashSaleProducts",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    FlashSaleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashSaleProducts", x => new { x.FlashSaleId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_FlashSaleProducts_FlashSales_FlashSaleId",
                        column: x => x.FlashSaleId,
                        principalTable: "FlashSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlashSaleProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("2e4c7ac4-2dfc-4772-afc6-58772959f440"), new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3434), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3435), null },
                    { new Guid("87d4457d-2add-4796-9837-ad242719254b"), new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3430), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3431), null },
                    { new Guid("8c3008d7-74aa-4290-b842-6e12002dbc52"), new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3436), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4o", new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3436), null },
                    { new Guid("a43b9c0c-6b86-41a2-a3f2-d4cfd8f123b7"), new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3432), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3433), null },
                    { new Guid("cd476411-4043-4eba-8739-c02ca0062a67"), new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3411), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3411), null },
                    { new Guid("d2387129-bafc-41b4-a4b7-75425b5b6344"), new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3372), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3376), null },
                    { new Guid("d5d57ef8-bd73-4a70-b4ec-38d67254afd7"), new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3428), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3429), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("032063c0-bd0b-4ff6-82b1-ce6d98821e53"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3687), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3688) },
                    { new Guid("4a7a6bf6-00af-4901-9407-cce1fd23d529"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3680), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3681) },
                    { new Guid("6eea3936-0579-4c57-bc4e-08574315075c"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3685), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2024, 12, 14, 2, 38, 28, 440, DateTimeKind.Utc).AddTicks(3685) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlashSaleProducts_ProductId",
                table: "FlashSaleProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlashSaleProducts");

            migrationBuilder.DropTable(
                name: "FlashSales");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("2e4c7ac4-2dfc-4772-afc6-58772959f440"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("87d4457d-2add-4796-9837-ad242719254b"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("8c3008d7-74aa-4290-b842-6e12002dbc52"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("a43b9c0c-6b86-41a2-a3f2-d4cfd8f123b7"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("cd476411-4043-4eba-8739-c02ca0062a67"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("d2387129-bafc-41b4-a4b7-75425b5b6344"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("d5d57ef8-bd73-4a70-b4ec-38d67254afd7"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("032063c0-bd0b-4ff6-82b1-ce6d98821e53"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("4a7a6bf6-00af-4901-9407-cce1fd23d529"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("6eea3936-0579-4c57-bc4e-08574315075c"));

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("25887a1b-79fe-4e39-9e4e-f1639af7cdef"), new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9698), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9698), null },
                    { new Guid("2a7bc9e1-3f4a-4dfb-87c8-58f1cadf5be1"), new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9677), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9677), null },
                    { new Guid("741f0aa9-ac83-4bc0-b6a6-db764f69eadc"), new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9675), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9675), null },
                    { new Guid("82160df7-b12a-418d-88d3-6584e575c1b4"), new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9632), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9635), null },
                    { new Guid("cf70a206-3204-449f-81f7-8fc645841154"), new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9696), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9696), null },
                    { new Guid("de331ac5-2840-433f-a35f-97e3759a3b8a"), new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9672), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9673), null },
                    { new Guid("fcf74e5a-75f1-41bb-ba13-6f5efdd36f93"), new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9699), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4o", new DateTime(2024, 12, 10, 4, 39, 52, 87, DateTimeKind.Utc).AddTicks(9700), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("783385a3-5ff6-47cd-b52b-a3bf648223de"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 10, 4, 39, 52, 88, DateTimeKind.Utc).AddTicks(136), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2024, 12, 10, 4, 39, 52, 88, DateTimeKind.Utc).AddTicks(136) },
                    { new Guid("8510451b-771d-445f-890f-d6bd70e2a0b2"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 10, 4, 39, 52, 88, DateTimeKind.Utc).AddTicks(126), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2024, 12, 10, 4, 39, 52, 88, DateTimeKind.Utc).AddTicks(127) },
                    { new Guid("9825d352-e829-4aaa-8334-f1cbe6a14f1c"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 10, 4, 39, 52, 88, DateTimeKind.Utc).AddTicks(134), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2024, 12, 10, 4, 39, 52, 88, DateTimeKind.Utc).AddTicks(134) }
                });
        }
    }
}
