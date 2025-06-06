using System.Threading.Tasks;

namespace KURSOVOIproject.Services
{
    public interface INotificationService
    {
        // TODO: отправить пуш-уведомление (локальное) пользователю
        Task SendNotificationAsync(int userId, string title, string message);

        // TODO: запланировать уведомление о новой стажировке (на дату публикации)
        Task ScheduleNewInternshipNotificationAsync(int specializationId);

        // TODO: запланировать уведомление об изменении статуса отклика
        Task ScheduleApplicationStatusNotificationAsync(int applicationId);

        // TODO: запланировать уведомление о приглашении на собеседование
        Task ScheduleInterviewInvitationAsync(int applicationId, System.DateTime interviewDate);
    }
}
