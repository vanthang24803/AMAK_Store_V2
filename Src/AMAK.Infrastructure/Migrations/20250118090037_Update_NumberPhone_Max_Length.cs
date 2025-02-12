using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_NumberPhone_Max_Length : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("0e146713-6306-4496-ab85-5b25d9a3c684"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("0e8b7cfb-6477-45e8-9bc4-4b7c2d320f8f"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("1efbf147-3bb0-43fd-91ba-31ab155734a9"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("237114b2-4c08-45c4-a107-83663dac33e3"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("6c5c2c56-f22e-489c-8db7-fddd8a3ab123"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("e6ee4852-5042-476a-926b-478c7fd6de95"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("f5936a9e-adb4-4e64-a2de-d673c6136506"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("08e17071-e08e-4081-ab44-1dd6c1cf8b87"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("19e283eb-7258-49f1-b504-d484dee6ff31"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("764f5d82-f6fb-45a3-a530-370c609002cb"));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("09278c49-af23-4d3c-aa1a-ae8b30b08100"), new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7413), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7414), null },
                    { new Guid("102b6d7b-fd24-4270-8eaa-af4f43e9c26d"), new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7416), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7416), null },
                    { new Guid("4bd32fb9-08ab-4595-8540-ce8a4fc96f29"), new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7422), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7423), null },
                    { new Guid("8ac09674-f4c6-49d2-a53e-4fec39e03012"), new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7424), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4o", new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7425), null },
                    { new Guid("a3cf4ee5-5633-41a3-9e2e-a75d80a7fa06"), new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7420), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7421), null },
                    { new Guid("a743c88c-c4d2-4896-bff5-0115154a1344"), new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7372), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7375), null },
                    { new Guid("c1181dd1-8914-4c22-8dd4-c2bf91ed7b2d"), new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7418), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7419), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("27917b96-ee35-455c-b7a0-2b82b205ae18"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7857), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7858) },
                    { new Guid("3da39e35-74ad-4506-97e2-43b11df97f1b"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7834), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7835) },
                    { new Guid("9202bb8f-1b98-4a7e-9456-a5a2e7efdd06"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7854), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2025, 1, 18, 9, 0, 37, 3, DateTimeKind.Utc).AddTicks(7855) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("09278c49-af23-4d3c-aa1a-ae8b30b08100"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("102b6d7b-fd24-4270-8eaa-af4f43e9c26d"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("4bd32fb9-08ab-4595-8540-ce8a4fc96f29"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("8ac09674-f4c6-49d2-a53e-4fec39e03012"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("a3cf4ee5-5633-41a3-9e2e-a75d80a7fa06"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("a743c88c-c4d2-4896-bff5-0115154a1344"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("c1181dd1-8914-4c22-8dd4-c2bf91ed7b2d"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("27917b96-ee35-455c-b7a0-2b82b205ae18"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("3da39e35-74ad-4506-97e2-43b11df97f1b"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("9202bb8f-1b98-4a7e-9456-a5a2e7efdd06"));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "character varying(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("0e146713-6306-4496-ab85-5b25d9a3c684"), new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1743), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4o", new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1743), null },
                    { new Guid("0e8b7cfb-6477-45e8-9bc4-4b7c2d320f8f"), new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1736), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1736), null },
                    { new Guid("1efbf147-3bb0-43fd-91ba-31ab155734a9"), new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1738), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1738), null },
                    { new Guid("237114b2-4c08-45c4-a107-83663dac33e3"), new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1733), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1734), null },
                    { new Guid("6c5c2c56-f22e-489c-8db7-fddd8a3ab123"), new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1694), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1697), null },
                    { new Guid("e6ee4852-5042-476a-926b-478c7fd6de95"), new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1741), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1742), null },
                    { new Guid("f5936a9e-adb4-4e64-a2de-d673c6136506"), new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1739), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1740), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("08e17071-e08e-4081-ab44-1dd6c1cf8b87"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1987), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1987) },
                    { new Guid("19e283eb-7258-49f1-b504-d484dee6ff31"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1983), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1983) },
                    { new Guid("764f5d82-f6fb-45a3-a530-370c609002cb"), "                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1989), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2024, 12, 14, 7, 25, 24, 31, DateTimeKind.Utc).AddTicks(1989) }
                });
        }
    }
}
