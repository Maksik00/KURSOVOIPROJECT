using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KURSOVOIproject.Services;
using KURSOVOIproject.Views;
using Microsoft.Maui.Controls;

namespace KURSOVOIproject.ViewModels
{
    public partial class AdminLoginViewModel : ObservableObject
    {
        private readonly IAdminService _adminService;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isBusy;

        public AsyncRelayCommand LoginCommand { get; }

        public AdminLoginViewModel(IAdminService adminService)
        {
            _adminService = adminService;
            LoginCommand = new AsyncRelayCommand(LoginAsync);
        }

        public async Task LoginAsync()
        {
            if (IsBusy) return;
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Заполните все поля", "OK");
                return;
            }

            try
            {
                IsBusy = true;
                // TODO: проверить логин/пароль администратора (в идеале в базе будет таблица Admins)
                bool isValid = (Email == "admin@example.com" && Password == "admin"); // заглушка

                if (!isValid)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "Неверный логин или пароль", "OK");
                    return;
                }

                // Если успешно, то переходим в AdminPage
                await Shell.Current.GoToAsync("//Admin");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
