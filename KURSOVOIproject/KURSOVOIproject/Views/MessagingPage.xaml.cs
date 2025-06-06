using System;
using Microsoft.Maui.Controls;
using KURSOVOIproject.Services;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class MessagingPage : ContentPage
    {
        private readonly IMessagingService _messagingService;
        private readonly MessagingViewModel _viewModel;

        public MessagingPage(
            IMessagingService messagingService,
            MessagingViewModel viewModel)
        {
            InitializeComponent();
            _messagingService = messagingService;
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // TODO: вызвать _viewModel.LoadConversationAsync() для загрузки истории
        }

        private async void OnSendMessageClicked(object sender, EventArgs e)
        {
            string text = NewMessageEntry.Text?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(text))
                return;

            // TODO: вызвать await _viewModel.SendMessageAsync(text);
            NewMessageEntry.Text = "";
        }
    }
}
