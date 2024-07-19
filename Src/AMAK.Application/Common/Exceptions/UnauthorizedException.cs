using System.Net;
using AMAK.Application.Common.Helpers;

namespace AMAK.Application.Common.Exceptions {
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