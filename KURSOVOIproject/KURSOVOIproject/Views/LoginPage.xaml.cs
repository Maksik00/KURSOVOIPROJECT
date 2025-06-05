using Microsoft.Maui.Controls;
using KURSOVOIproject.Services;
using KURSOVOIproject.Models;
using Microsoft.Maui.Storage;

namespace KURSOVOIproject.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly IStudentService _studentService;

        // Если у вас есть ViewModel, можно принимать его здесь:
        // public LoginPage(LoginViewModel vm) { ... BindingContext = vm; }

        public LoginPage(IStudentService studentService)
        {
            InitializeComponent();
            _studentService = studentService;
        }

        private async void OnLoginButtonClicked(object sender, System.EventArgs e)
        {
            string phone = PhoneEntry.Text?.Trim() ?? "";
            string password = PasswordEntry.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Ошибка", "Заполните все поля", "OK");
                return;
            }

            // Попытка найти студента по телефону и паролю
            Student student = null;
            try
            {
                student = await _studentService.GetByPhoneAndPasswordAsync(phone, password);
            }
            catch
            {
                // Если в базе несколько одинаковых записей или другая ошибка
            }

            if (student == null)
            {
                await DisplayAlert("Ошибка", "Неверный телефон или пароль", "OK");
                return;
            }

            // Сохраняем ID студента и флаг авторизации
            Preferences.Default.Set("CurrentStudentId", student.Id);
            Preferences.Default.Set("IsStudentLoggedIn", true);

            // Переходим на SearchPage (экран поиска)
            await Shell.Current.GoToAsync("//Search");
        }

        private async void OnRegisterTapped(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("//Registration");
        }
    }
}
