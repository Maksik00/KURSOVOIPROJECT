using System;
using System.Threading.Tasks;

namespace KURSOVOIproject.Services
{
    public class NotificationService : INotificationService
    {
        public NotificationService()
        {
            // TODO: Инициализация локального уведомительного провайдера (Platform-specific API)
        }

        public async Task SendNotificationAsync(int userId, string title, string message)
        {
            // TODO: Реализовать отправку локального пуша (или использовать Maui Community Toolkit)
            await Task.CompletedTask;
        }

        public async Task ScheduleNewInternshipNotificationAsync(int specializationId)
        {
            // TODO: Найти подписанных на эту специализацию студентов, 
            //       запланировать на дату публикации стажировки уведом.
            await Task.CompletedTask;
        }

        public async Task ScheduleApplicationStatusNotificationAsync(int applicationId)
        {
            // TODO: После изменения статуса отклика вызвать SendNotificationAsync
            await Task.CompletedTask;
        }

        public async Task ScheduleInterviewInvitationAsync(int applicationId, DateTime interviewDate)
        {
            // TODO: Запланировать уведомление в указанное время с текстом «Приглашение на собеседование»
            await Task.CompletedTask;
        }
    }
}
