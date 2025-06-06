using System;
using Microsoft.Maui.Controls;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class AllInternshipsPage : ContentPage
    {
        private readonly AllInternshipsPageViewModel _viewModel;
        public AllInternshipsPage(AllInternshipsPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadInternshipsCommand.ExecuteAsync(null);
        }
    }
}
