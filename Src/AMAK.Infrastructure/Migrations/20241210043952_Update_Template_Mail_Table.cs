using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Template_Mail_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
