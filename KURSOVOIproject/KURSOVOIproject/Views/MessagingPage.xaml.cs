using System;
using Microsoft.Maui.Controls;
<<<<<<< HEAD
using KURSOVOIproject.Services;
=======
>>>>>>> 359d440 (InternshipADDING!)
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class MessagingPage : ContentPage
    {
<<<<<<< HEAD
        private readonly IMessagingService _messagingService;
        private readonly MessagingViewModel _viewModel;

        public MessagingPage(
            IMessagingService messagingService,
            MessagingViewModel viewModel)
        {
            InitializeComponent();
            _messagingService = messagingService;
=======
        private readonly MessagingViewModel _viewModel;

        public MessagingPage(MessagingViewModel viewModel)
        {
            InitializeComponent();
>>>>>>> 359d440 (InternshipADDING!)
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
<<<<<<< HEAD
            // TODO: âûçâàòü _viewModel.LoadConversationAsync() äëÿ çàãðóçêè èñòîðèè
=======
            // TODO completed: Ð·Ð°Ð³Ñ€ÑƒÐ·ÐºÐ° Ð¿ÐµÑ€ÐµÐ¿Ð¸ÑÐºÐ¸
            _viewModel.LoadConversationCommand.Execute(null);
>>>>>>> 359d440 (InternshipADDING!)
        }

        private async void OnSendMessageClicked(object sender, EventArgs e)
        {
<<<<<<< HEAD
            string text = NewMessageEntry.Text?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(text))
                return;

            // TODO: âûçâàòü await _viewModel.SendMessageAsync(text);
            NewMessageEntry.Text = "";
=======
            string text = NewMessageEntry.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(text))
                return;

            // TODO completed: Ð¾Ñ‚Ð¿Ñ€Ð°Ð²Ð»ÑÐµÐ¼ ÑÐ¾Ð¾Ð±Ñ‰ÐµÐ½Ð¸Ðµ Ñ‡ÐµÑ€ÐµÐ· ViewModel
            _viewModel.NewMessageText = text;
            await _viewModel.SendMessageCommand.ExecuteAsync(null);
            NewMessageEntry.Text = string.Empty;
>>>>>>> 359d440 (InternshipADDING!)
        }
    }
}
