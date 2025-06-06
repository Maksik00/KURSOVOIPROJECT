using System;
using Microsoft.Maui.Controls;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class MessagingPage : ContentPage
    {
        private readonly MessagingViewModel _viewModel;

        public MessagingPage(MessagingViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // TODO completed: загрузка переписки
            _viewModel.LoadConversationCommand.Execute(null);
        }

        private async void OnSendMessageClicked(object sender, EventArgs e)
        {
            string text = NewMessageEntry.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(text))
                return;

            // TODO completed: отправляем сообщение через ViewModel
            _viewModel.NewMessageText = text;
            await _viewModel.SendMessageCommand.ExecuteAsync(null);
            NewMessageEntry.Text = string.Empty;
        }
    }
}
