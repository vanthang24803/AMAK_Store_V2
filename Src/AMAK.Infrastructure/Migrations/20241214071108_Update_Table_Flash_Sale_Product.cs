using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Table_Flash_Sale_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "OptionId",
                table: "FlashSaleProducts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("40989fab-e5ba-4375-8d99-fb0b0ea46de5"), new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(422), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4o", new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(422), null },
                    { new Guid("79570505-a9eb-4d25-b361-84efe3eaee85"), new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(420), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(420), null },
                    { new Guid("9c7bac6b-381a-4baa-aada-2c0cf5d7645e"), new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(400), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(400), null },
                    { new Guid("ac5d05aa-e9ca-4b96-a0b1-f4ca4dfd8369"), new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(404), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(404), null },
                    { new Guid("c46cd73a-ff03-497a-b8be-87abed19e695"), new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(365), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(367), null },
                    { new Guid("c9f4bbd2-df30-409a-9dd8-e456fb195ed3"), new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(402), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(403), null },
                    { new Guid("de1e2f69-6d8d-4a9d-b765-5bb2ba869ef1"), new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(406), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(406), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("4a21655e-36de-4519-a477-4928ea17e2b5"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(600), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(600) },
                    { new Guid("badc4fa8-de83-4dba-9a9d-75b372aa8d56"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(604), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(605) },
                    { new Guid("ebbd790b-8f6a-415c-b536-26878aef42c2"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(607), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2024, 12, 14, 7, 11, 7, 666, DateTimeKind.Utc).AddTicks(607) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("40989fab-e5ba-4375-8d99-fb0b0ea46de5"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("79570505-a9eb-4d25-b361-84efe3eaee85"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("9c7bac6b-381a-4baa-aada-2c0cf5d7645e"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("ac5d05aa-e9ca-4b96-a0b1-f4ca4dfd8369"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("c46cd73a-ff03-497a-b8be-87abed19e695"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("c9f4bbd2-df30-409a-9dd8-e456fb195ed3"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("de1e2f69-6d8d-4a9d-b765-5bb2ba869ef1"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("4a21655e-36de-4519-a477-4928ea17e2b5"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("badc4fa8-de83-4dba-9a9d-75b372aa8d56"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("ebbd790b-8f6a-415c-b536-26878aef42c2"));

            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "FlashSaleProducts");

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
    }
}
