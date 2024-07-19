using System.Net;
using AMAK.Application.Common.Helpers;

namespace AMAK.Application.Common.Exceptions
{
   public class ForbiddenException(string message = "Forbidden") : Exception(message) {
        public ApiError ToApiError() {
            return new ApiError {
                Status = (int)HttpStatusCode.Forbidden,
                Message = Message,
                Timestamp = DateTime.Now
            };
        }
    }
}