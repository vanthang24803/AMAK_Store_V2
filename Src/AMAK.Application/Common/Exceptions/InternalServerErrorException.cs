namespace AMAK.Application.Common.Exceptions {
    public abstract class InternalServerErrorException(string message = "Internal Server Error") : Exception(message) {
    }
}