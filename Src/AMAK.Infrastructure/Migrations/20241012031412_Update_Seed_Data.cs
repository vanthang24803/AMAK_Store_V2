using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Seed_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AIConfigs",
                keyColumn: "Id",
                keyValue: new Guid("4fb73695-d2b7-4152-aab0-c81155c3991e"));

            migrationBuilder.DeleteData(
                table: "AIConfigs",
                keyColumn: "Id",
                keyValue: new Guid("b5a1ed43-17a7-4580-b8b7-7d251dcb94d8"));

            migrationBuilder.DeleteData(
                table: "AIConfigs",
                keyColumn: "Id",
                keyValue: new Guid("c2cd0452-a1eb-4801-81ca-9cbb10e2040d"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("4d0c4791-4aef-4202-b333-ce367182ff8a"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("b1afae70-5cd3-4219-9b9c-2b27ca0874ce"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("2363702b-7753-48c6-b1a2-527257932718"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("27eaca64-5736-486d-9817-04f59c42a387"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("69e32de6-6c21-4962-88bc-a67736954a7f"));

            migrationBuilder.InsertData(
                table: "AIConfigs",
                columns: new[] { "Id", "Config", "CreateAt", "DeleteAt", "IsDeleted", "Name", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("daa93784-b19b-43fb-aa15-b1b33fff0083"), null, new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6598), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT3_5", new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6598) },
                    { new Guid("ece9aadc-ad94-4e2b-bddd-ed96fd7d4a12"), null, new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6584), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6585) },
                    { new Guid("feeab585-a7bf-4f3a-b357-26befcf95507"), null, new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6595), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6596) }
                });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("232805ad-3e88-4e89-b293-b42d41550c53"), new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6256), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6257), null },
                    { new Guid("cf643c37-8861-4a24-b818-c9933b87d28b"), new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6258), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6258), null },
                    { new Guid("d73f72d3-41e2-4ca2-8f29-8c03a415151a"), new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6155), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6158), null },
                    { new Guid("fb4aecdf-ecc2-4c27-b6ee-be38389a9d93"), new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6254), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6254), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("ccf4138a-18af-4974-b32b-b534b6979444"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.\r\n                        ", new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6635), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6636) },
                    { new Guid("d79650f5-4d9d-44dc-9e4c-741160310b9b"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6686), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6686) },
                    { new Guid("e607214a-d5ff-4222-b6ad-2e79cdd2ea78"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.\r\n                    ", new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6631), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2024, 10, 12, 3, 14, 11, 135, DateTimeKind.Utc).AddTicks(6632) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AIConfigs",
                keyColumn: "Id",
                keyValue: new Guid("daa93784-b19b-43fb-aa15-b1b33fff0083"));

            migrationBuilder.DeleteData(
                table: "AIConfigs",
                keyColumn: "Id",
                keyValue: new Guid("ece9aadc-ad94-4e2b-bddd-ed96fd7d4a12"));

            migrationBuilder.DeleteData(
                table: "AIConfigs",
                keyColumn: "Id",
                keyValue: new Guid("feeab585-a7bf-4f3a-b357-26befcf95507"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("232805ad-3e88-4e89-b293-b42d41550c53"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("cf643c37-8861-4a24-b818-c9933b87d28b"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("d73f72d3-41e2-4ca2-8f29-8c03a415151a"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("fb4aecdf-ecc2-4c27-b6ee-be38389a9d93"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("ccf4138a-18af-4974-b32b-b534b6979444"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("d79650f5-4d9d-44dc-9e4c-741160310b9b"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("e607214a-d5ff-4222-b6ad-2e79cdd2ea78"));

            migrationBuilder.InsertData(
                table: "AIConfigs",
                columns: new[] { "Id", "Config", "CreateAt", "DeleteAt", "IsDeleted", "Name", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("4fb73695-d2b7-4152-aab0-c81155c3991e"), null, new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(6301), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT3_5", new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(6301) },
                    { new Guid("b5a1ed43-17a7-4580-b8b7-7d251dcb94d8"), null, new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(6271), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(6272) },
                    { new Guid("c2cd0452-a1eb-4801-81ca-9cbb10e2040d"), null, new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(6284), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(6284) }
                });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("4d0c4791-4aef-4202-b333-ce367182ff8a"), new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(5855), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(5856), null },
                    { new Guid("b1afae70-5cd3-4219-9b9c-2b27ca0874ce"), new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(5815), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(5819), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("2363702b-7753-48c6-b1a2-527257932718"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(6345), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(6345) },
                    { new Guid("27eaca64-5736-486d-9817-04f59c42a387"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.\r\n                        ", new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(6343), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(6343) },
                    { new Guid("69e32de6-6c21-4962-88bc-a67736954a7f"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.\r\n                    ", new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(6336), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2024, 10, 12, 2, 55, 25, 876, DateTimeKind.Utc).AddTicks(6337) }
                });
        }
    }
}
