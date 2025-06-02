using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;         // для Preferences (если используете их)
using KURSOVOIproject.Services;       // ваш IStudentService
using KURSOVOIproject.Models;         // модель Student (если нужна)

namespace KURSOVOIproject.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly IStudentService _studentService;

        public LoginPage(IStudentService studentService)
        {
            InitializeComponent();
            _studentService = studentService;
        }

        private async void OnLoginButtonClicked(object sender, System.EventArgs e)
        {
            string phone = PhoneEntry.Text?.Trim();
            string password = PasswordEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(phone)
             || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Ошибка", "Заполните все поля", "OK");
                return;
            }

            var student = await _studentService.GetByPhoneAndPasswordAsync(phone, password);
            if (student == null)
            {
                await DisplayAlert("Ошибка", "Неверный телефон или пароль", "OK");
                return;
            }

            // Сохраняем ID студента (если нужно)
            Preferences.Default.Set("CurrentStudentId", student.Id);

            // Переходим в TabBar (SearchPage) абсолютным маршрутом:
            await Shell.Current.GoToAsync("//SearchPage");
        }

        private async void OnRegisterTapped(object sender, System.EventArgs e)
        {
            // Переход на RegistrationPage
            await Shell.Current.GoToAsync("RegistrationPage");
        }
    }
}
