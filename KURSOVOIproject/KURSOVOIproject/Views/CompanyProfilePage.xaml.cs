using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using KURSOVOIproject.ViewModels;

namespace KURSOVOIproject.Views
{
    public partial class CompanyProfilePage : ContentPage
    {
        public CompanyProfilePage(CompanyProfileViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private async void OnEditProfileClicked(object sender, System.EventArgs e)
        {
            // TODO: Ваша логика для редактирования профиля компании
            await DisplayAlert("Редактировать", "Здесь можно отредактировать профиль компании.", "OK");
        }

        private async void OnCreateInternshipClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("CreateInternship");
        }

        private async void OnLogoutClicked(object sender, System.EventArgs e)
        {
            // Очистим ID компании и флаг залогинен ли
            Preferences.Default.Remove("CurrentCompanyId");
            Preferences.Default.Set("IsCompanyLoggedIn", false);

            // Перейдём обратно на главный экран (Landing)
            await Shell.Current.GoToAsync("Landing");
        }
    }
}
