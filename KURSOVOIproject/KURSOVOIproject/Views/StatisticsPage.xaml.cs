using System;
using Microsoft.Maui.Controls;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class StatisticsPage : ContentPage
    {
        private readonly StatisticsPageViewModel _viewModel;

        public StatisticsPage(StatisticsPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // TODO: вызвать загрузку статистики _viewModel.LoadStatisticsCommand.Execute(null);
        }
    }
}
