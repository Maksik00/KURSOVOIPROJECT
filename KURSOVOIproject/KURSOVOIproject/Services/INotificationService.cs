using System.Threading.Tasks;

namespace KURSOVOIproject.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(int userId, string title, string message);

        Task ScheduleNewInternshipNotificationAsync(int specializationId);

        Task ScheduleApplicationStatusNotificationAsync(int applicationId);

        Task ScheduleInterviewInvitationAsync(int applicationId, System.DateTime interviewDate);
    }
}
