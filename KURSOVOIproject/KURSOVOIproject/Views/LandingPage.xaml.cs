using Microsoft.Maui.Controls;

namespace KURSOVOIproject.Views
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterButtonClicked(object sender, System.EventArgs e)
        {
            // Переход на RegistrationPage (RegistrationPage уже зарегистрирован в Shell)
            await Shell.Current.GoToAsync("RegistrationPage");
        }

        private async void OnLoginButtonClicked(object sender, System.EventArgs e)
        {
            // Переход на LoginPage
            await Shell.Current.GoToAsync("LoginPage");
        }
    }
}
