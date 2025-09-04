using System.Collections.Generic;
using System.Threading.Tasks;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Services
{
    public interface IMessagingService
    {
        // TODO: Получить историю переписки по applicationId
        Task<IEnumerable<Message>> GetConversationAsync(int applicationId);

        // TODO: Отправить новое сообщение (для chat)
        Task SendMessageAsync(int applicationId, int senderUserId, string text);
    }
}
