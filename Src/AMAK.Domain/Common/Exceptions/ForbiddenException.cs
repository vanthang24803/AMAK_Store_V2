using System.Net;
using AMAK.Domain.Common.Helpers;

namespace AMAK.Domain.Common.Exceptions
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