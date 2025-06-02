using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;  // <-- обязательно
using CommunityToolkit.Mvvm.Input;           // <-- обязательно
using KURSOVOIproject.Services;              // <-- для IStudentService
using Microsoft.Maui.Controls;               // <-- для DisplayAlert, Shell
using Microsoft.Maui.Storage;                 // <-- для Preferences

namespace KURSOVOIproject.ViewModels
{
    // Наследуемся от ObservableObject, чтобы иметь INotifyPropertyChanged
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IStudentService _studentService;

        // Конструктор получает IStudentService через DI (MauiProgram.cs)
        public LoginViewModel(IStudentService studentService)
        {
            _studentService = studentService;
            LoginCommand = new AsyncRelayCommand(LoginAsync);
        }

        // ===========================
        // === ПОЛЯ И СВОЙСТВА ===
        // ===========================

        // Атрибут [ObservableProperty] автоматически генерирует:
        //   private string _telNumber;
        //   public string TelNumber { get; set; /*+ OnPropertyChanged */ }
        [ObservableProperty]
        private string telNumber;

        //   private string _password;
        //   public string Password { get; set; /*+ OnPropertyChanged */ }
        [ObservableProperty]
        private string password;

        //   private bool _isBusy;
        //   public bool IsBusy { get; set; /*+ OnPropertyChanged */ }
        [ObservableProperty]
        private bool isBusy;

        // Доступное свойство для кнопки, когда не загружаем
        public bool IsNotBusy => !IsBusy;

        // ===========================
        // ====== КОМАНДЫ ===========
        // ===========================

        // Эта команда будет привязана к кнопке "Войти"
        public IAsyncRelayCommand LoginCommand { get; }

        // ===========================
        // ====== МЕТОДЫ ===========
        // ===========================

        private async Task LoginAsync()
        {
            if (IsBusy)
                return;

            // Проверяем, что поля не пустые
            if (string.IsNullOrWhiteSpace(TelNumber)
             || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage
                      .DisplayAlert("Ошибка", "Заполните все поля", "OK");
                return;
            }

            try
            {
                // Включаем индикатор
                IsBusy = true;
                OnPropertyChanged(nameof(IsNotBusy));

                // Ищем студента по телефону и паролю
                var student = await _studentService
                                      .GetByPhoneAndPasswordAsync(TelNumber, Password);
                if (student == null)
                {
                    await Application.Current.MainPage
                          .DisplayAlert("Ошибка", "Неверный телефон или пароль", "OK");
                    return;
                }

                // Сохраняем Id текущего студента в Preferences (если нужно)
                Preferences.Default.Set("CurrentStudentId", student.Id);

                // Переходим на маршрут "//SearchPage"
                // (Заранее должен быть объявлен SearchPage в AppShell.xaml)
                await Shell.Current.GoToAsync("//SearchPage");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                      .DisplayAlert("Ошибка", ex.Message, "OK");
            }
            finally
            {
                // Выключаем индикатор
                IsBusy = false;
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }
    }
}
