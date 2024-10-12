using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations {
    /// <inheritdoc />
    public partial class Update_Table_Configuration_2 : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DeleteData(
                table: "AIConfigs",
                keyColumn: "Id",
                keyValue: new Guid("27f3fc6f-33ef-40d9-921c-be75df421009"));

            migrationBuilder.DeleteData(
                table: "AIConfigs",
                keyColumn: "Id",
                keyValue: new Guid("474cedf2-e877-4531-be18-0211e9d54689"));

            migrationBuilder.DeleteData(
                table: "AIConfigs",
                keyColumn: "Id",
                keyValue: new Guid("8f96bc3b-e97d-4fd0-9d1a-23765997e671"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("1c005953-10c8-4a0c-880a-4e529739e71d"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("22459ebc-d120-4c73-a854-51659035b902"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("2ba9a32d-0cfd-4edd-ab77-80a14e082073"));

            migrationBuilder.AlterColumn<JsonElement>(
                name: "Value",
                table: "Configurations",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Configurations",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
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

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Configurations",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(JsonElement),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Configurations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "AIConfigs",
                columns: new[] { "Id", "Config", "CreateAt", "DeleteAt", "IsDeleted", "Name", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("27f3fc6f-33ef-40d9-921c-be75df421009"), null, new DateTime(2024, 10, 11, 15, 10, 38, 302, DateTimeKind.Utc).AddTicks(5099), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2024, 10, 11, 15, 10, 38, 302, DateTimeKind.Utc).AddTicks(5101) },
                    { new Guid("474cedf2-e877-4531-be18-0211e9d54689"), null, new DateTime(2024, 10, 11, 15, 10, 38, 302, DateTimeKind.Utc).AddTicks(5149), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT3_5", new DateTime(2024, 10, 11, 15, 10, 38, 302, DateTimeKind.Utc).AddTicks(5149) },
                    { new Guid("8f96bc3b-e97d-4fd0-9d1a-23765997e671"), null, new DateTime(2024, 10, 11, 15, 10, 38, 302, DateTimeKind.Utc).AddTicks(5146), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2024, 10, 11, 15, 10, 38, 302, DateTimeKind.Utc).AddTicks(5146) }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("1c005953-10c8-4a0c-880a-4e529739e71d"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.\r\n                        ", new DateTime(2024, 10, 11, 15, 10, 38, 302, DateTimeKind.Utc).AddTicks(5458), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2024, 10, 11, 15, 10, 38, 302, DateTimeKind.Utc).AddTicks(5458) },
                    { new Guid("22459ebc-d120-4c73-a854-51659035b902"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 10, 11, 15, 10, 38, 302, DateTimeKind.Utc).AddTicks(5460), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2024, 10, 11, 15, 10, 38, 302, DateTimeKind.Utc).AddTicks(5460) },
                    { new Guid("2ba9a32d-0cfd-4edd-ab77-80a14e082073"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.\r\n                    ", new DateTime(2024, 10, 11, 15, 10, 38, 302, DateTimeKind.Utc).AddTicks(5454), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2024, 10, 11, 15, 10, 38, 302, DateTimeKind.Utc).AddTicks(5454) }
                });
        }
    }
}
