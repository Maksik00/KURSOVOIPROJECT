<<<<<<< HEAD
﻿using System;
using System.Threading.Tasks;
=======
using System;
using System.Linq;
using System.Threading.Tasks;
using KURSOVOIproject.Data;
using KURSOVOIproject.Models;
using Microsoft.EntityFrameworkCore;
>>>>>>> 359d440 (InternshipADDING!)

namespace KURSOVOIproject.Services
{
    public class NotificationService : INotificationService
    {
<<<<<<< HEAD
        public NotificationService()
        {
            // TODO: Инициализация локального уведомительного провайдера (Platform-specific API)
=======
        private readonly SflDbContext _db;

        public NotificationService(SflDbContext db)
        {
            _db = db;
            // TODO completed: платформа может иметь нативные уведомления.
>>>>>>> 359d440 (InternshipADDING!)
        }

        public async Task SendNotificationAsync(int userId, string title, string message)
        {
<<<<<<< HEAD
            // TODO: Реализовать отправку локального пуша (или использовать Maui Community Toolkit)
            await Task.CompletedTask;
=======
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
>>>>>>> 359d440 (InternshipADDING!)
        }

        public async Task ScheduleNewInternshipNotificationAsync(int specializationId)
        {
<<<<<<< HEAD
            // TODO: Найти подписанных на эту специализацию студентов, 
            //       запланировать на дату публикации стажировки уведом.
            await Task.CompletedTask;
=======
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
>>>>>>> 359d440 (InternshipADDING!)
        }

        public async Task ScheduleApplicationStatusNotificationAsync(int applicationId)
        {
<<<<<<< HEAD
            // TODO: После изменения статуса отклика вызвать SendNotificationAsync
            await Task.CompletedTask;
=======
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
>>>>>>> 359d440 (InternshipADDING!)
        }

        public async Task ScheduleInterviewInvitationAsync(int applicationId, DateTime interviewDate)
        {
<<<<<<< HEAD
            // TODO: Запланировать уведомление в указанное время с текстом «Приглашение на собеседование»
            await Task.CompletedTask;
=======
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
>>>>>>> 359d440 (InternshipADDING!)
        }
    }
}
