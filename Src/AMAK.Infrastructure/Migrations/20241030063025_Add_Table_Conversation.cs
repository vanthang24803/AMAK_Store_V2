using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Table_Conversation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("22ba562b-1d9d-4b67-80f3-5986c61c5431"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("2327dd27-a9c9-4123-926c-a45f45296fb7"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("3cd30808-db97-4c88-899a-59c69f54d4fd"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("b5fd6e59-76b1-468b-a322-6bfc7e6aec9d"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("c71f3751-c01f-4401-ad53-c3c34f347f10"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("d34c9741-1db8-4085-b0cb-89f3eeb3b80f"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("eff93d04-34eb-472d-86dd-62803ba76c6a"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("04a51e61-61e1-48af-b750-faff30c10d2b"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("e4224680-d2e9-4759-b20d-b80a2a67ac0d"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("e6984772-5817-41f9-891e-a3e478c52aa4"));

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    IsBotReply = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("0ea7bbb6-5e02-45c9-91a3-7fec61087ca3"), new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(5944), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(5945), null },
                    { new Guid("42fcc83a-7199-44f6-89a0-dd20b45ae456"), new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(5949), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(5950), null },
                    { new Guid("9415f65e-b456-4dbb-8cd9-5832ae7c882a"), new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(5946), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(5946), null },
                    { new Guid("9e3a6055-d429-4d95-9b5d-e66d9690234a"), new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(5942), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(5942), null },
                    { new Guid("a8613c26-e76d-4d3b-aec6-0e1a2302f73b"), new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(5841), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(5859), null },
                    { new Guid("cd664b87-0797-4475-bb80-b8d610fd8890"), new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(5948), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(5948), null },
                    { new Guid("e0164b6b-178e-4444-bbfa-8a2f328c77e9"), new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(6012), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4o", new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(6013), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("1b87223d-350a-464d-846a-73ce73f38fc1"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(6364), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(6364) },
                    { new Guid("31d1667e-a7a7-494f-a67d-9f6667ad5e2d"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(6355), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(6355) },
                    { new Guid("a1799286-8ad7-489f-b56f-0700fbe961ad"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(6359), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2024, 10, 30, 6, 30, 22, 651, DateTimeKind.Utc).AddTicks(6359) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_UserId",
                table: "Conversations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("0ea7bbb6-5e02-45c9-91a3-7fec61087ca3"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("42fcc83a-7199-44f6-89a0-dd20b45ae456"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("9415f65e-b456-4dbb-8cd9-5832ae7c882a"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("9e3a6055-d429-4d95-9b5d-e66d9690234a"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("a8613c26-e76d-4d3b-aec6-0e1a2302f73b"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("cd664b87-0797-4475-bb80-b8d610fd8890"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("e0164b6b-178e-4444-bbfa-8a2f328c77e9"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("1b87223d-350a-464d-846a-73ce73f38fc1"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("31d1667e-a7a7-494f-a67d-9f6667ad5e2d"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("a1799286-8ad7-489f-b56f-0700fbe961ad"));

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("22ba562b-1d9d-4b67-80f3-5986c61c5431"), new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9533), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9534), null },
                    { new Guid("2327dd27-a9c9-4123-926c-a45f45296fb7"), new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9535), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9536), null },
                    { new Guid("3cd30808-db97-4c88-899a-59c69f54d4fd"), new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9527), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9527), null },
                    { new Guid("b5fd6e59-76b1-468b-a322-6bfc7e6aec9d"), new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9529), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9530), null },
                    { new Guid("c71f3751-c01f-4401-ad53-c3c34f347f10"), new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9537), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4o", new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9538), null },
                    { new Guid("d34c9741-1db8-4085-b0cb-89f3eeb3b80f"), new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9531), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9532), null },
                    { new Guid("eff93d04-34eb-472d-86dd-62803ba76c6a"), new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9442), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2024, 10, 21, 6, 41, 1, 962, DateTimeKind.Utc).AddTicks(9445), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("04a51e61-61e1-48af-b750-faff30c10d2b"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.\r\n                        ", new DateTime(2024, 10, 21, 6, 41, 1, 963, DateTimeKind.Utc).AddTicks(8), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2024, 10, 21, 6, 41, 1, 963, DateTimeKind.Utc).AddTicks(9) },
                    { new Guid("e4224680-d2e9-4759-b20d-b80a2a67ac0d"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.\r\n                    ", new DateTime(2024, 10, 21, 6, 41, 1, 963, DateTimeKind.Utc).AddTicks(3), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2024, 10, 21, 6, 41, 1, 963, DateTimeKind.Utc).AddTicks(3) },
                    { new Guid("e6984772-5817-41f9-891e-a3e478c52aa4"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 10, 21, 6, 41, 1, 963, DateTimeKind.Utc).AddTicks(14), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2024, 10, 21, 6, 41, 1, 963, DateTimeKind.Utc).AddTicks(14) }
                });
        }
    }
}
