using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashSaleProducts_Products_ProductId",
                table: "FlashSaleProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlashSaleProducts",
                table: "FlashSaleProducts");

            migrationBuilder.DropIndex(
                name: "IX_FlashSaleProducts_ProductId",
                table: "FlashSaleProducts");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlashSaleProducts",
                table: "FlashSaleProducts",
                columns: new[] { "FlashSaleId", "OptionId" });

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

            migrationBuilder.CreateIndex(
                name: "IX_FlashSaleProducts_OptionId",
                table: "FlashSaleProducts",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashSaleProducts_Options_OptionId",
                table: "FlashSaleProducts",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashSaleProducts_Options_OptionId",
                table: "FlashSaleProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlashSaleProducts",
                table: "FlashSaleProducts");

            migrationBuilder.DropIndex(
                name: "IX_FlashSaleProducts_OptionId",
                table: "FlashSaleProducts");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlashSaleProducts",
                table: "FlashSaleProducts",
                columns: new[] { "FlashSaleId", "ProductId" });

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

            migrationBuilder.CreateIndex(
                name: "IX_FlashSaleProducts_ProductId",
                table: "FlashSaleProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashSaleProducts_Products_ProductId",
                table: "FlashSaleProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
