using Microsoft.Maui.Controls;

namespace KURSOVOIproject.Views
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        // -------------- Обработчики кнопок для студентов --------------
        private async void OnStudentRegisterClicked(object sender, EventArgs e)
        {
            // Навигируем на страницу регистрации студента
            await Shell.Current.GoToAsync("StudentRegistration");
        }

        private async void OnStudentLoginClicked(object sender, EventArgs e)
        {
            // Навигируем на страницу входа студента
            await Shell.Current.GoToAsync("StudentLogin");
        }

        // -------------- Обработчики кнопок для компаний --------------
        private async void OnCompanyRegisterClicked(object sender, EventArgs e)
        {
            // Навигируем на страницу регистрации компании
            await Shell.Current.GoToAsync("CompanyRegistration");
        }

        private async void OnCompanyLoginClicked(object sender, EventArgs e)
        {
            // Навигируем на страницу входа компании
            await Shell.Current.GoToAsync("CompanyLogin");
        }
    }
}
