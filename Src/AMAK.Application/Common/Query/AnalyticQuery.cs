using System.Globalization;

namespace AMAK.Application.Common.Query {
    public class AnalyticQuery : BaseQuery {
        private string? _startAt;
        private string? _endAt;

        public string? Status { get; set; } = null;

        public string StartAt {
            get => _startAt ?? new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).ToString("d/M/yyyy");
            set {
                if (DateTime.TryParseExact(value, "d/M/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out DateTime parsedDate)) {
                    if (parsedDate > DateTime.UtcNow) {
                        _startAt = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).ToString("d/M/yyyy");
                    } else {
                        _startAt = value;
                    }
                } else {
                    _startAt = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).ToString("d/M/yyyy");
                }
            }
        }

        public string EndAt {
            get => _endAt ?? new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month)).ToString("d/M/yyyy");
            set => _endAt = value;
        }
    }
}
