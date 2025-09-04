using System;
using Microsoft.Maui.Controls;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class InternshipDetailPage : ContentPage
    {
        private readonly InternshipDetailViewModel _viewModel;

        public InternshipDetailPage(InternshipDetailViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // TODO completed: загрузка деталей
            _viewModel.LoadDetailsCommand.Execute(null);
        }

        private async void OnApplyClicked(object sender, EventArgs e)
        {
            // TODO completed: отклик или отзыв
            if (!_viewModel.IsApplied)
                await _viewModel.ApplyCommand.ExecuteAsync(null);
            else
                await _viewModel.RevokeCommand.ExecuteAsync(null);
        }
    }
}
