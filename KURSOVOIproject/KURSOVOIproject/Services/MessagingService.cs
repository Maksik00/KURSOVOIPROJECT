using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KURSOVOIproject.Data;
using KURSOVOIproject.Models;
using Microsoft.EntityFrameworkCore;

namespace KURSOVOIproject.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly SflDbContext _dbContext;

        public MessagingService(SflDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Message>> GetConversationAsync(int applicationId)
        {
            // TODO: Вернуть все сообщения, где ApplicationId == applicationId, отсортированные по дате
            return await _dbContext.Messages
                .Where(m => m.ApplicationId == applicationId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task SendMessageAsync(int applicationId, int senderUserId, string text)
        {
            // TODO: Добавить запись в таблицу Messages
            var msg = new Message
            {
                ApplicationId = applicationId,
                SenderUserId = senderUserId,
                Text = text,
                Timestamp = System.DateTime.UtcNow
            };
            _dbContext.Messages.Add(msg);
            await _dbContext.SaveChangesAsync();
        }
    }
}
