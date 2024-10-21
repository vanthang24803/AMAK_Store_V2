using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Table_Blog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Thumbnail = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AuthorId",
                table: "Blogs",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

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
    }
}
