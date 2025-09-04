using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KURSOVOIproject.Models;
using KURSOVOIproject.Services;

namespace KURSOVOIproject.ViewModels
{
    public partial class MessagingViewModel : ObservableObject
    {
        private readonly IMessagingService _messagingService;

        public int ApplicationId { get; set; }
        public int CurrentUserId { get; set; }

        public ObservableCollection<Message> Messages { get; }
            = new ObservableCollection<Message>();

        [ObservableProperty]
        private string newMessageText;

        public IAsyncRelayCommand LoadConversationCommand { get; }
        public IAsyncRelayCommand SendMessageCommand { get; }

        public MessagingViewModel(IMessagingService messagingService)
        {
            _messagingService = messagingService;
            LoadConversationCommand = new AsyncRelayCommand(LoadAsync);
            SendMessageCommand = new AsyncRelayCommand(SendAsync);
        }

        private async Task LoadAsync()
        {
            // TODO completed: загружаем историю переписки через сервис
            Messages.Clear();
            var history = await _messagingService.GetConversationAsync(ApplicationId);
            foreach (var msg in history)
                Messages.Add(msg);
        }

        private async Task SendAsync()
        {
            if (string.IsNullOrWhiteSpace(NewMessageText)) return;

            // TODO completed: отправляем сообщение и обновляем чат
            await _messagingService.SendMessageAsync(ApplicationId, CurrentUserId, NewMessageText);
            NewMessageText = string.Empty;
            await LoadAsync();
        }
    }
}
