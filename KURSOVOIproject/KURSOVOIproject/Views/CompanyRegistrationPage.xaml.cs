using Microsoft.Maui.Controls;
using KURSOVOIproject.Services;
using Microsoft.Maui.Storage;
using KURSOVOIproject.Models;

namespace KURSOVOIproject.Views
{
    public partial class CompanyRegistrationPage : ContentPage
    {
        private readonly ICompanyService _companyService;

        public CompanyRegistrationPage(ICompanyService companyService)
        {
            InitializeComponent();
            _companyService = companyService;
        }

        private async void OnRegisterButtonClicked(object sender, System.EventArgs e)
        {
            string name = NameEntry.Text?.Trim() ?? "";
            string city = CityEntry.Text?.Trim() ?? "";
            string street = StreetEntry.Text?.Trim() ?? "";
            string building = BuildingEntry.Text?.Trim() ?? "";
            string password = PasswordEntry.Text?.Trim() ?? "";

            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(city) ||
                string.IsNullOrEmpty(street) ||
                string.IsNullOrEmpty(building) ||
                string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Ошибка", "Заполните все поля.", "ОК");
                return;
            }

            // Создаём новый объект Company
            var newCompany = new Company
            {
                Name = name,
                City = city,
                Street = street,
                Building = building,
                Password = password
            };

            // Сохраняем в базу через сервис
            await _companyService.CreateAsync(newCompany);

            // Сохраняем в Preferences
            Preferences.Default.Set("CurrentCompanyId", newCompany.Id);
            Preferences.Default.Set("IsCompanyLoggedIn", true);

            // Переходим на CompanyProfilePage
            await Shell.Current.GoToAsync("///CompanyProfile");
        }

        private async void OnAlreadyHaveAccountClicked(object sender, System.EventArgs e)
        {
            // Если у компании уже есть аккаунт
            await Shell.Current.GoToAsync("CompanyLogin");
        }
    }
}
