using Microsoft.Maui.Controls;
using KURSOVOIproject.Services;
using Microsoft.Maui.Storage;

namespace KURSOVOIproject.Views
{
    public partial class CompanyLoginPage : ContentPage
    {
        private readonly ICompanyService _companyService;

        public CompanyLoginPage(ICompanyService companyService)
        {
            InitializeComponent();
            _companyService = companyService;
        }

        private async void OnLoginButtonClicked(object sender, System.EventArgs e)
        {
            string name = NameEntry.Text?.Trim() ?? "";
            string password = PasswordEntry.Text?.Trim() ?? "";

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Ошибка", "Заполните все поля", "ОК");
                return;
            }

            // Ищем компанию по имени и паролю
            var company = await _companyService.GetByNameAndPasswordAsync(name, password);
            if (company == null)
            {
                await DisplayAlert("Ошибка", "Неверное имя компании или пароль.", "ОК");
                return;
            }

            // Сохраняем Id компании в Preferences
            Preferences.Default.Set("CurrentCompanyId", company.Id);
            Preferences.Default.Set("IsCompanyLoggedIn", true);

            // Переходим на CompanyProfilePage
            await Shell.Current.GoToAsync("CompanyProfile");
        }

        private async void OnRegisterTapped(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("CompanyRegistration");
        }
    }
}
