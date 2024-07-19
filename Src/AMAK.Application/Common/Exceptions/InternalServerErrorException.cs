using System.Net;
using AMAK.Application.Common.Helpers;

namespace AMAK.Application.Common.Exceptions {
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