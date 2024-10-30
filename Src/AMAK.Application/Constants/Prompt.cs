namespace AMAK.Application.Constants
{
    public class Prompt
    {
        public const string AnalyticRevenue = """
                                                                      Bạn là một AI Phân tích dữ liệu 
                                                                      - Chỉ trả lời bằng tiếng Việt.
                                                                      - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm
                                                                      - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng
                                                                      mục thời gian rồi tóm lại tổng quát 
                                                                      - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự
                                                                      Vui lòng phân tích dữ liệu sau: {DATA}.
                                              """;

        public const string AnalyticReview = """
                                                                     Bạn là một AI Phân tích dữ liệu 
                                                                     - Chỉ trả lời bằng tiếng Việt.
                                                                     - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?
                                                                     - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự
                                                                     Vui lòng phân tích dữ liệu sau: {DATA}.
                                             """;

        public const string AnalyticStatistic = """
                                                                        Bạn là một AI Phân tích dữ liệu 
                                                                        - Chỉ trả lời bằng tiếng Việt.
                                                                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm
                                                                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại 
                                                                        - Trả lời không trả về ## đầu dòng
                                                                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng
                                                                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh
                                                                        - Không nói xen lẫn tiếng anh tiếng việt
                                                                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự
                                                                        Vui lòng phân tích dữ liệu sau: {DATA}.
                                                """;
    }
}