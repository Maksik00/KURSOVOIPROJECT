using System;
using Microsoft.Maui.Controls;
using KURSOVOIproject.Services;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class InternshipDetailPage : ContentPage
    {
        private readonly IApplicationService _applicationService;
        private readonly InternshipDetailViewModel _viewModel;

        public InternshipDetailPage(
            IApplicationService applicationService,
            InternshipDetailViewModel viewModel)
        {
            InitializeComponent();
            _applicationService = applicationService;
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // TODO: вызвать _viewModel.LoadDetailsAsync() (грузим детали стажировки по Id)
        }

        private async void OnApplyClicked(object sender, EventArgs e)
        {
            // TODO: если пользователь ещё не откликался, вызвать 
            // await _viewModel.ApplyToInternshipAsync(); 
            // иначе вызвать 
            // await _viewModel.RevokeApplicationAsync();
        }
    }
}
