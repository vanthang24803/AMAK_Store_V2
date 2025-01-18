using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Option_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<bool>(
                name: "IsFlashSale",
                table: "Options",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("2bb75b75-b06a-4427-b2fd-2683be8ade9d"), new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5136), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5137), null },
                    { new Guid("3586390c-0448-47c7-acca-0813c684f74b"), new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5034), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5038), null },
                    { new Guid("56cc2184-381d-4ba5-b8ed-ed1da45a64ee"), new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5143), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5144), null },
                    { new Guid("90c43072-6b2f-4dd1-81a6-16da9d5193e2"), new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5139), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5139), null },
                    { new Guid("bfc63eba-08a5-4fac-a1e6-55ca15be4523"), new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5146), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4o", new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5146), null },
                    { new Guid("c31e7b11-469a-4c72-8d6c-afcb987eb371"), new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5141), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5142), null },
                    { new Guid("c7dd8da1-fde3-4ef9-999b-87f777783075"), new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5134), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5134), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("7d10b3aa-5c54-469a-b40c-28f42b381f87"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5412), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5412) },
                    { new Guid("e118b698-45dd-4e04-b9d6-1aed82729bcd"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5401), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5402) },
                    { new Guid("fd0aff9c-5035-4a68-9016-8460b6a17328"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5405), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2024, 12, 14, 6, 9, 6, 833, DateTimeKind.Utc).AddTicks(5406) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("2bb75b75-b06a-4427-b2fd-2683be8ade9d"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("3586390c-0448-47c7-acca-0813c684f74b"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("56cc2184-381d-4ba5-b8ed-ed1da45a64ee"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("90c43072-6b2f-4dd1-81a6-16da9d5193e2"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("bfc63eba-08a5-4fac-a1e6-55ca15be4523"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("c31e7b11-469a-4c72-8d6c-afcb987eb371"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("c7dd8da1-fde3-4ef9-999b-87f777783075"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("7d10b3aa-5c54-469a-b40c-28f42b381f87"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("e118b698-45dd-4e04-b9d6-1aed82729bcd"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("fd0aff9c-5035-4a68-9016-8460b6a17328"));

            migrationBuilder.DropColumn(
                name: "IsFlashSale",
                table: "Options");

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
        }
    }
}
