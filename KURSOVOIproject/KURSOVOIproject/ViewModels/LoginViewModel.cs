using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;  // <-- обязательно
using CommunityToolkit.Mvvm.Input;           // <-- обязательно
using KURSOVOIproject.Services;              // <-- для IStudentService
using Microsoft.Maui.Controls;               // <-- для DisplayAlert, Shell
using Microsoft.Maui.Storage;                // <-- для Preferences
using KURSOVOIproject.Models;

namespace KURSOVOIproject.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IStudentService _studentService;

        public LoginViewModel(IStudentService studentService)
        {
            _studentService = studentService;
            LoginCommand = new AsyncRelayCommand(LoginAsync);
        }

        // ===========================
        // === ПОЛЯ И СВОЙСТВА ===
        // ===========================

        [ObservableProperty]
        private string telNumber;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isBusy;

        public bool IsNotBusy => !IsBusy;

        // ===========================
        // ====== КОМАНДЫ ===========
        // ===========================

        public IAsyncRelayCommand LoginCommand { get; }

        // ===========================
        // ====== МЕТОДЫ ===========
        // ===========================

        private async Task LoginAsync()
        {
            if (IsBusy)
                return;

            if (string.IsNullOrWhiteSpace(TelNumber)
                || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage
                      .DisplayAlert("Ошибка", "Заполните все поля", "OK");
                return;
            }

            try
            {
                IsBusy = true;
                OnPropertyChanged(nameof(IsNotBusy));

                var student = await _studentService
                                      .GetByPhoneAndPasswordAsync(TelNumber, Password);
                if (student == null)
                {
                    await Application.Current.MainPage
                          .DisplayAlert("Ошибка", "Неверный телефон или пароль", "OK");
                    return;
                }

                // Сохраняем Id и статус авторизации
                Preferences.Default.Set("CurrentStudentId", student.Id);
                Preferences.Default.Set("IsStudentLoggedIn", true);

                // Навигация на Search (обратите внимание: в AppShell маршрут называется "Search")
                await Shell.Current.GoToAsync("//Search");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                      .DisplayAlert("Ошибка", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }
    }
}
