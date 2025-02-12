using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Seed_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
