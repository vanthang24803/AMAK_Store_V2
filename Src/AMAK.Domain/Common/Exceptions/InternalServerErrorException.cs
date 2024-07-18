using System.Net;
using AMAK.Domain.Common.Helpers;

namespace AMAK.Domain.Common.Exceptions {
    public class InternalServerErrorException(string message = "Internal Server Error") : Exception(message) {
        public ApiError ToApiError() {
            return new ApiError {
                Status = (int)HttpStatusCode.InternalServerError,
                Message = Message,
                Timestamp = DateTime.Now
            };
        }
    }
}