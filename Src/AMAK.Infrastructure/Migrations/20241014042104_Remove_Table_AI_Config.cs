using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Table_AI_Config : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIConfigs");

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
                table: "Configurations",
                columns: new[] { "Id", "CreateAt", "DeleteAt", "IsDeleted", "Key", "UpdateAt", "Value" },
                values: new object[,]
                {
                    { new Guid("106e4170-b4ba-4e3b-b4d5-f36b61e8964c"), new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4849), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Google", new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4874), null },
                    { new Guid("632f3772-5f6c-4f20-a3d4-34fe44f33ddc"), new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4918), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4", new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4918), null },
                    { new Guid("71c161ba-32f6-4e3b-8369-9ae6a04c28ca"), new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4912), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Email", new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4912), null },
                    { new Guid("7c2bffd8-e966-423a-9c7a-3d78da7a6873"), new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4916), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Gemini", new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4916), null },
                    { new Guid("df9cc5ad-1fdc-4199-890d-acc3b4b4b2ed"), new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4931), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "ChatGPT4o", new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4931), null },
                    { new Guid("ef6b85e9-181d-4e87-a50f-3ba24ffd1bfa"), new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4910), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Cloudinary", new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4910), null },
                    { new Guid("f997fbe4-9490-4614-a3a6-dad5eb905cea"), new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4914), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Momo", new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(4914), null }
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("13e695a3-19e6-4c61-8bb5-47ddb4b0e88c"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(5282), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(5283) },
                    { new Guid("1bd545b7-b098-4ed6-b8b6-97832af924a3"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.\r\n                        ", new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(5280), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(5280) },
                    { new Guid("c889edd6-d30f-44be-a9de-a2c9b8d4d517"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.\r\n                    ", new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(5277), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2024, 10, 14, 4, 21, 2, 865, DateTimeKind.Utc).AddTicks(5277) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("106e4170-b4ba-4e3b-b4d5-f36b61e8964c"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("632f3772-5f6c-4f20-a3d4-34fe44f33ddc"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("71c161ba-32f6-4e3b-8369-9ae6a04c28ca"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("7c2bffd8-e966-423a-9c7a-3d78da7a6873"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("df9cc5ad-1fdc-4199-890d-acc3b4b4b2ed"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("ef6b85e9-181d-4e87-a50f-3ba24ffd1bfa"));

            migrationBuilder.DeleteData(
                table: "Configurations",
                keyColumn: "Id",
                keyValue: new Guid("f997fbe4-9490-4614-a3a6-dad5eb905cea"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("13e695a3-19e6-4c61-8bb5-47ddb4b0e88c"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("1bd545b7-b098-4ed6-b8b6-97832af924a3"));

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: new Guid("c889edd6-d30f-44be-a9de-a2c9b8d4d517"));

            migrationBuilder.CreateTable(
                name: "AIConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Config = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIConfigs", x => x.Id);
                });

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
    }
}
