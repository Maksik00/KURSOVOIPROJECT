using System;
using Microsoft.Maui.Controls;
using KURSOVOIproject.Services;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class ResponsesPage : ContentPage
    {
        private readonly IApplicationService _applicationService;
        private readonly ResponsesPageViewModel _viewModel;

        public ResponsesPage(
            IApplicationService applicationService,
            ResponsesPageViewModel viewModel)
        {
            InitializeComponent();
            _applicationService = applicationService;
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // TODO: вызвать _viewModel.LoadResponsesAsync() для загрузки данных
        }

        private async void OnMessageStudentClicked(object sender, EventArgs e)
        {
            // TODO: Получить выбранный ApplicationId из BindingContext элемента
            // и перейти на MessagingPage, передав студента/компанию
        }
    }
}
