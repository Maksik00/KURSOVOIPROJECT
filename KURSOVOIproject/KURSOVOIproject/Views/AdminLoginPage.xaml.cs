using System;
using Microsoft.Maui.Controls;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class AdminLoginPage : ContentPage
    {
        private readonly AdminLoginViewModel _viewModel;

        public AdminLoginPage(AdminLoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // TODO completed: вызываем метод из ViewModel
            await _viewModel.LoginAsync();
        }

        private async void OnRegisterAdminClicked(object sender, EventArgs e)
        {
            // TODO: в будущем можно добавить регистрацию админа
            await DisplayAlert("Info", "Регистрация администратора пока недоступна", "OK");
        }
    }
}
