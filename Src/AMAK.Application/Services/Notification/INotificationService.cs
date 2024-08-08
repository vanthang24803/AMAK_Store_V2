using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Notification.Dtos;

namespace AMAK.Application.Services.Notification {
    public interface INotificationService {
        Task<Response<List<NotificationResponse>>> GetAllNotificationForAccount(ClaimsPrincipal user);
        Task<Response<string>> SeenAllNotification(ClaimsPrincipal user);
        Task<Response<string>> OpenAllNotification(ClaimsPrincipal user);
        Task<Response<NotificationResponse>> CreateNotification(CreateNotificationForAccountRequest request);
        Task<Response<string>> CreateGlobalNotification(CreateGlobalNotificationRequest request);
        Task<Response<string>> DeleteNotificationForAccount(ClaimsPrincipal user, Guid id);
    }
}