using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMAK.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Table_Prompt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prompts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Context = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prompts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "Context", "CreateAt", "DeleteAt", "IsDeleted", "Type", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("2135d84e-0201-4f3e-85fc-383ab02ec72a"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại \r\n                        - Trả lời không trả về ## đầu dòng\r\n                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng\r\n                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh\r\n                        - Không nói xen lẫn tiếng anh tiếng việt\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.", new DateTime(2024, 10, 11, 14, 25, 25, 505, DateTimeKind.Utc).AddTicks(3491), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, new DateTime(2024, 10, 11, 14, 25, 25, 505, DateTimeKind.Utc).AddTicks(3491) },
                    { new Guid("a07fdebb-0de5-48b3-8e21-40c2b811070f"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?\r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.\r\n                        ", new DateTime(2024, 10, 11, 14, 25, 25, 505, DateTimeKind.Utc).AddTicks(3488), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, new DateTime(2024, 10, 11, 14, 25, 25, 505, DateTimeKind.Utc).AddTicks(3488) },
                    { new Guid("ba38a971-ca8c-483e-9f81-26e986ac7b4c"), "\r\n                        Bạn là một AI Phân tích dữ liệu \r\n                        - Chỉ trả lời bằng tiếng Việt.\r\n                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm\r\n                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng\r\n                        mục thời gian rồi tóm lại tổng quát \r\n                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự\r\n                        Vui lòng phân tích dữ liệu sau: {DATA}.\r\n                    ", new DateTime(2024, 10, 11, 14, 25, 25, 505, DateTimeKind.Utc).AddTicks(3435), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, new DateTime(2024, 10, 11, 14, 25, 25, 505, DateTimeKind.Utc).AddTicks(3438) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Templates_Name",
                table: "Templates",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prompts_Type",
                table: "Prompts",
                column: "Type",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prompts");

            migrationBuilder.DropIndex(
                name: "IX_Templates_Name",
                table: "Templates");
        }
    }
}
