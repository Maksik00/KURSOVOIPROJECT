using System;
using System.Linq;
using System.Threading.Tasks;
using KURSOVOIproject.Data;
using KURSOVOIproject.Models;
using Microsoft.EntityFrameworkCore;

namespace KURSOVOIproject.Services
{
    public class NotificationService : INotificationService
    {
        private readonly SflDbContext _db;

        public NotificationService(SflDbContext db)
        {
            _db = db;
            // TODO completed: платформа может иметь нативные уведомления.
        }

        public async Task SendNotificationAsync(int userId, string title, string message)
        {
            // TODO completed: сохраняем уведомление как отправленное
            var notification = new Notification
            {
                RecipientUserId = userId,
                Title = title,
                Message = message,
                ScheduledAt = DateTime.UtcNow,
                IsSent = true
            };

            _db.Notifications.Add(notification);
            await _db.SaveChangesAsync();
        }

        public async Task ScheduleNewInternshipNotificationAsync(int specializationId)
        {
            // TODO completed: уведомляем всех студентов указанной специализации
            var studentIds = await _db.Students
                .Where(s => s.IdSpecialization == specializationId)
                .Select(s => s.Id)
                .ToListAsync();

            foreach (var id in studentIds)
            {
                _db.Notifications.Add(new Notification
                {
                    RecipientUserId = id,
                    Title = "Новая стажировка",
                    Message = "Добавлена новая стажировка по вашей специализации",
                    ScheduledAt = DateTime.UtcNow,
                    IsSent = false
                });
            }

            await _db.SaveChangesAsync();
        }

        public async Task ScheduleApplicationStatusNotificationAsync(int applicationId)
        {
            // TODO completed: после изменения статуса информируем студента
            var app = await _db.Applications
                .Include(a => a.Student)
                .Include(a => a.Internship)
                .FirstOrDefaultAsync(a => a.Id == applicationId);
            if (app == null)
                return;

            _db.Notifications.Add(new Notification
            {
                RecipientUserId = app.IdStudent,
                Title = "Статус заявки обновлен",
                Message = $"Ваша заявка на '{app.Internship.Title}' обновлена",
                ScheduledAt = DateTime.UtcNow,
                IsSent = false
            });
            await _db.SaveChangesAsync();
        }

        public async Task ScheduleInterviewInvitationAsync(int applicationId, DateTime interviewDate)
        {
            // TODO completed: уведомление о приглашении на собеседование
            var app = await _db.Applications
                .Include(a => a.Student)
                .Include(a => a.Internship)
                .FirstOrDefaultAsync(a => a.Id == applicationId);
            if (app == null)
                return;

            _db.Notifications.Add(new Notification
            {
                RecipientUserId = app.IdStudent,
                Title = "Приглашение на собеседование",
                Message = $"Собеседование по '{app.Internship.Title}' назначено на {interviewDate:dd.MM.yyyy HH:mm}",
                ScheduledAt = interviewDate,
                IsSent = false
            });
            await _db.SaveChangesAsync();
        }
    }
}
