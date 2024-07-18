using System.Net;
using AMAK.Domain.Common.Helpers;

namespace AMAK.Domain.Common.Exceptions {
    public class UnauthorizedException(string message = "Unauthorized") : Exception(message) {
        public ApiError ToApiError() {
            return new ApiError {
                Status = (int)HttpStatusCode.Unauthorized,
                Message = Message,
                Timestamp = DateTime.Now
            };
        }
    }
}